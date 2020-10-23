using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnVelocity;
    public Transform topT;
    public Transform standardT;
    public Transform standardTPivot;

    public GameObject pfTarget;
    public GameObject food;

    bool top;

    bool A;
    bool D;

    Ray rc;
    RaycastHit rch;

    int timer;
    Vector3 targetPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            top = !top;
        }

        
        if(!top)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                A = true;
            }
            
            if (Input.GetKeyUp(KeyCode.A))
            {
                A = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                D = true;
            }

            if(Input.GetKeyUp(KeyCode.D))
            {
                D = false;
            }      
        }

        if (Input.GetMouseButtonDown(0))
        {
            rc = new Ray(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), transform.forward);
            Debug.DrawRay(rc.origin, rc.direction, Color.red);
            if (Physics.Raycast(rc.origin, rc.direction, out rch, 100))
            {
                Debug.Log("Ray hit");
                targetPos = rch.point;
                Instantiate(food, targetPos + (Vector3.up * 11f), pfTarget.transform.rotation);
            }
        }

        if(HuishManager.me.dead)
        {
            top = true;
        }
    }

    void FixedUpdate()
    {
        if(A)
        {
            standardTPivot.Rotate(0, turnVelocity, 0);
        }

        if (D)
        {
            standardTPivot.Rotate(0, -turnVelocity, 0);
        }

        if (top)
        {
            transform.position = Vector3.Lerp(transform.position, topT.position, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, topT.rotation, 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, standardT.position, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, standardT.rotation, 0.1f);
        }

        if(pfTarget.transform.position != targetPos)
        {
            timer++;
            if(timer > 75)
            {
                pfTarget.transform.position = targetPos;
                timer = 0;
            }
        }
    }
}
