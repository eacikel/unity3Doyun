using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string turnAxisName = "Horizontal";
    string moveAxisName = "Vertical";

    float moveAxis;
    float turnAxis;

    float moveSpeed = 7;
    float turnSpeed = 220;

    Rigidbody rb;
    
    void Start() {

        rb = GetComponent<Rigidbody>();
    }
    
    void Update() {

        moveAxis = Input.GetAxis(moveAxisName);
        turnAxis = Input.GetAxis(turnAxisName);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
      
    }

    private void FixedUpdate() {

        Quaternion rotation = Quaternion.Euler(0, turnAxis * turnSpeed * Time.deltaTime, 0);
        rb.MovePosition(transform.position + transform.forward * moveAxis * moveSpeed * Time.deltaTime);
        rb.MoveRotation(transform.rotation * rotation);
    }

    private void Shoot() {

        GetComponent<ShootBehaviour>().Shoot();
    }
}
