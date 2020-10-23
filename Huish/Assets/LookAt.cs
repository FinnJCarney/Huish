using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject targetObj;
    public bool onlyY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(targetObj.transform);

        if(onlyY)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

    }
}
