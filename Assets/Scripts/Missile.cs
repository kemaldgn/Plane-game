using System;
using UnityEngine;
using System.Collections;


    public class Missile : MonoBehaviour {
        [Header("REFERENCES")] 
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private PlaneMovement planeMovement;
        //[SerializeField] private GameObject _explosionPrefab;

        [Header("MOVEMENT")] 
        [SerializeField] private float _speed = 15;
        [SerializeField] private float _rotateSpeed = 95;

        [Header("PREDICTION")] 
        [SerializeField] private float _maxDistancePredict = 100;
        [SerializeField] private float _minDistancePredict = 5;
        [SerializeField] private float _maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")] 
        [SerializeField] private float _deviationAmount = 50;
        [SerializeField] private float _deviationSpeed = 2;

        public Transform[] spawnPoints;
        public GameObject enemyPrefab;

        private void FixedUpdate() {
            _rb.velocity = transform.forward * _speed;

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, planeMovement.transform.position));

            PredictPlaneMovement(leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }

        private void PredictPlaneMovement(float leadTimePercentage) {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

            _standardPrediction = planeMovement.rb.position + (planeMovement.rb.velocity) * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage) {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

            _deviatedPrediction = _standardPrediction + predictionOffset;
        }

        private void RotateRocket() {
            var heading = _deviatedPrediction - transform.position;

            var rotation = Quaternion.LookRotation(heading);
            
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision) {
            //if(_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
   
            Destroy(collision.gameObject);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _standardPrediction);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
        }

        private IEnumerator DestroyRocketAfter10Seconds(GameObject Missile){

            yield return (new WaitForSeconds(10));

            Destroy(Missile);
        }

        public IEnumerator speedBoost()
        {
            int randSpawnPoint = UnityEngine.Random.Range(0,spawnPoints.Length);
            
            yield return new WaitForSeconds(5f);

            Instantiate(gameObject, spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }
