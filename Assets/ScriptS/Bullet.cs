using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    private void Start() {

        Destroy(gameObject, 3f);
    }
    
    private void OnCollisionEnter(Collision collision){

       HealthBehaviour healthb =  collision.gameObject.GetComponent<HealthBehaviour>();

        if (healthb)

            healthb.TakeDamage(20);
       
        Destroy(gameObject, 0.5f);
    }
}
