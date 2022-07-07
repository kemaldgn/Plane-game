using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedBooster : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player"){
            PlaneMovement.isBoosted = true;
        }
        Destroy(gameObject);
    }
}
