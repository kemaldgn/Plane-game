using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float directionSpeed;

    [SerializeField] public Rigidbody rb;


    [SerializeField] public Vector3 velocity;
    

    private float horizontalInput, verticalInput;

    

    public static bool isBoosted= false;

    private float direction;
    private float upDownMovement;
    private float upDownMovementBool;
    private float turnMovement;
    private float turnMovementBool;
    private bool corotineisDone = true;

    public int score;
    void Update()
    {
        UnityEngine.Debug.Log(rb.velocity);
        MovePlane();
        UnityEngine.Debug.Log(isBoosted);
        if (corotineisDone == true){
            MyInput();
            TurnPlane();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            
            if(horizontalInput >= 0){
                StartCoroutine(RotateObj(1f,-1));
            }

            else{
                StartCoroutine(RotateObj(1f, 1));
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            StartCoroutine(speedBoost());
        }
        if(isBoosted){
            StartCoroutine(speedBoost());
            isBoosted = false;
        }

        if (corotineisDone == true){
            MyInput();
            TurnPlane();
        }
        StartCoroutine(gainScore());
    }

    void FixedUpdate(){
        velocity = new Vector3();
        MovePlane();
        
    }

    void TurnPlane(){
        if(horizontalInput != 0 ){
            
        }
            direction += horizontalInput * directionSpeed * Time.deltaTime;
            upDownMovement = Mathf.Lerp(0,30,Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
            turnMovement = Mathf.Lerp(0, 25, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);
            transform.localRotation = Quaternion.Euler(Vector3.up * direction + Vector3.right * upDownMovement + Vector3.forward * turnMovement);
    }

    void MyInput(){
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
    }

    void MovePlane(){
        
        //transform.position += transform.forward * forwardSpeed * Time.fixedDeltaTime;
        rb.velocity = transform.forward * forwardSpeed ;

        
        
    }

    IEnumerator RotateObj(float duration,int direction)
    {
        corotineisDone = false;
        Quaternion startRot = transform.rotation;
        float t = 0.0f;

        while (t <= duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f * direction, Vector3.forward); 
            yield return null;
        }
        corotineisDone = true;
        
    }

        public IEnumerator speedBoost()
        {
            float speed = forwardSpeed;
            forwardSpeed = forwardSpeed * 1.5f;
            yield return new WaitForSeconds(1f);

            forwardSpeed = speed;
        }
        public IEnumerator gainScore()
        {
            
            yield return new WaitForSeconds(1f);

            score += 1;
        }

    
}
