using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchTrig : MonoBehaviour
{
    public MeshRenderer mr;
    public MoveTexture mt;
    public Light tvLight;
    public TVLight tvLightscr;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            mr.enabled = true;
            mt.active = true;
            tvLight.enabled = true;
            tvLightscr.active = true;
        }    
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            mr.enabled = false;
            mt.active = false;
            tvLight.enabled = false;
            tvLightscr.active = false;
        }
    }
}
