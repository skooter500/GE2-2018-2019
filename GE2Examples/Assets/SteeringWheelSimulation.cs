using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Testing out the steering wheel code
// Press space to simulate grabbing the wheel and use arrow keys to 
// move the hands
public class SteeringWheelSimulation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool held = false;

    public Transform hand;

    public Vector3 oldGrabPoint;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(oldGrabPoint, 0.1f);
    }

    public float angle;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!held)
            {
                oldGrabPoint = CalculateGrabPoint();
                held = true;
            }
            else
            {
                Vector3 grabPoint = CalculateGrabPoint();

                // Calculate the angle
                Vector3 from = grabPoint - transform.position;
                Vector3 to = oldGrabPoint - transform.position;
                angle = Vector3.Angle(from, to);

                // Calculate the direction, positive or negative
                Vector3 up1 = Vector3.Cross(from, to); // This will be an up or down vector
                float dot = Vector3.Dot(transform.up, up1);
                if (dot > 0)
                {
                    angle = -angle;
                }
                oldGrabPoint = grabPoint;
                transform.Rotate(0, angle, 0);
            }
        }
        else
        {
            held = false;
        }

        float speed = 5;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hand.transform.Translate(-Time.deltaTime * speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            hand.transform.Translate(Time.deltaTime * speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            hand.transform.Translate(0, 0, Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            hand.transform.Translate(0, 0, - Time.deltaTime * speed);
        }
    }

    public Vector3 CalculateGrabPoint()
    {
        Plane plane = new Plane(transform.up, transform.position);
        Ray ray = new Ray(hand.position, transform.up);
        float distance;
        plane.Raycast(ray, out distance);
        return hand.position + transform.up * distance;
    }
}
