using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float mass = 1;
    public Vector3 target;
    public float maxSpeed = 5;
    public GameObject targetGameObject;
    public float slowingDistance = 10;

    public bool SeekEnabled = false;
    public bool ArriveEnabled = false;

    public GameObject fleeTarget;
    public float fleeDistance = 10;
    public bool FleeEnabled = false;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, force * 5);
        Gizmos.DrawRay(transform.position, acceleration * 5);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }



    public Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }

    public Vector3 Flee(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) < fleeDistance)
        {
            Vector3 desired = target - transform.position;
            desired.Normalize();
            desired *= maxSpeed;
            return velocity - desired;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;

        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / dist);
        return desired - velocity;
    }

    Vector3 CalculateForces()
    {
        Vector3 force = Vector3.zero;
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
        
        force = Vector3.zero;
        if (SeekEnabled)
        {
            force += Seek(target);
        }
        if (ArriveEnabled)
        {
            force += Arrive(target);
        }
        if (FleeEnabled)
        {
            force += Flee(fleeTarget.transform.position);
        }
        return force;
    }

    // Update is called once per frame
    void Update()
    {
        force = CalculateForces();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity;
        }
    }
}
