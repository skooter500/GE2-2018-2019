using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JitterWander : SteeringBehaviour
{
    public float distance = 15.0f;
    public float radius = 10;
    public float jitter = 100;

    Vector3 target;
    Vector3 worldTarget;

    public void OnDrawGizmos()
    {
        Vector3 localCP = Vector3.forward * distance;
        Vector3 worldCP = transform.TransformPoint(localCP);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, worldCP);
        Gizmos.DrawWireSphere(worldCP, radius);

        Vector3 localTarget = (Vector3.forward * distance) + target;
        worldTarget = transform.TransformPoint(localTarget);

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(worldTarget, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, worldTarget);

    }

    public override Vector3 Calculate()
    {
        Vector3 disp = jitter * Random.insideUnitSphere * Time.deltaTime;
        target += disp;
        target.Normalize();
        target *= radius;

        Vector3 localTarget = (Vector3.forward * distance) + target;

        worldTarget = transform.TransformPoint(localTarget);
        return worldTarget - transform.position;

    }

    // Start is called before the first frame update
    void Start()
    {
        target = Random.insideUnitSphere * radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
