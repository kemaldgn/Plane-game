using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float lerpValue;
    [SerializeField] private GameObject targetObject;
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,targetObject.transform.position - offSet,lerpValue);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
