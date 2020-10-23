using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVLight : MonoBehaviour
{
    public Color pink;
    public Color blue;
    Light light;
    int timer;
    public bool active;

    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            timer++;
            light.color = Color.Lerp(pink, blue, Mathf.Abs(Mathf.Sin(timer * 0.1f)));
        }
    }
}
