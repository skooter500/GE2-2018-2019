using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerSteering: SteeringBehaviour
{
    public float playerForce = 100;        
    public override Vector3 Calculate()
    {
        Vector3 force = Vector3.zero;

        Vector3 projectedRight = transform.right;
        projectedRight.y = 0;
        projectedRight.Normalize();

        force += Input.GetAxis("Vertical") * transform.forward * playerForce;
        force += Input.GetAxis("Horizontal") * projectedRight * playerForce * 0.2f;
        return force;
    }
    
}