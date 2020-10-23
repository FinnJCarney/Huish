using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodParticleSystem : MonoBehaviour
{
    ParticleSystem ps;
    int timer;

    AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        aS = GetComponent<AudioSource>();
        aS.pitch = Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if(timer > 300)
        {
            Destroy(this.gameObject);
        }
    }
}
