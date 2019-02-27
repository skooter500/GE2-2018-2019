using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringTest : MonoBehaviour
{
	
	public bool held = false;
	
	public Transform Hand;
	
    private Vector3 oldGrabPoint;
    private Vector3 handPos;	
	
    // Update is called once per frame
    void Update()
    {
        handPos = Hand.position;        
    }

    public void OnTriggerExit(Collider other)
    {
        held = false;
    }

    public void OnDrawGizmos()
    {
        Vector3 gp = oldGrabPoint;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gp, 1);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(handPos, 1);        
    }

    public Vector3 CalculateGrabPoint()
    {
        Plane plane = new Plane(transform.up, transform.position);
        Ray ray = new Ray(handPos, transform.up);
        plane.Raycast(ray, out float distance);
        return handPos + transform.up * distance;
    }

    private bool CheckButton()
    {
        // Replace with button handling code
        return true;
    }
    
    public void OnTriggerStay(Collider target)
	{
        if(target.tag == "Hand") //when hands enter the steering wheel collider
		{
            // Replace with button handling code!!
            if (CheckButton())
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
                    float angle = Vector3.Angle(from, to);

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
		}
    }//end onTriggerStay()



}//end main()
