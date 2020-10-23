using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    public GameObject coverQuad;
    public Sprite[] logoSpr;
    SpriteRenderer sr;

    public Vector3 quadTarget;
    Vector3 ogPos;

    bool active;

    int timer;

    public ParticleSystem shower;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogPos = transform.localPosition;
        active = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            coverQuad.transform.localPosition = Vector3.Lerp(coverQuad.transform.localPosition, quadTarget, 0.005f);
            if (Vector3.Distance(coverQuad.transform.localPosition, quadTarget) < 0.2f)
            {
                sr.sprite = logoSpr[1];
                timer++;
            }

            if(timer > 150)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, ogPos + Vector3.up * 2, 0.05f);
                shower.Play();;
            }

            if(Vector3.Distance(transform.localPosition, ogPos + Vector3.up * 2) < 0.2f)
            {
                Destroy(coverQuad);
                timer = 0;
                active = false;
            }
        }

        if(HuishManager.me.dead)
        {
            timer++;
            if (timer > 180)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, ogPos, 0.05f);
                sr.sprite = logoSpr[2];
            }
        }
    }
}
