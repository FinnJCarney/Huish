using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public bool active;
    Vector3 ogPos;
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            timer++;
            transform.position = ogPos + (Vector3.right * Mathf.Sin(timer * 0.2f));
        }
    }
}
