using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class HuishManager : MonoBehaviour
{
    public static HuishManager me;

    public GameObject pfTarget;

    NavMeshAgent nMA;

    public bool dead;

    public float waterContent;
    float waterContentMax;

    public SpriteRenderer fishSprite;
    public Color blue;
    Vector3 fishOGPos;
    public Vector3 fishCouchPos;

    CapsuleCollider cc;
    bool inShower;
    bool onCouch;

    public AudioSource couchAS;
    public AudioSource computerAS;

    int sprTimer;
    public SpriteRenderer Bodsr;
    public Sprite standSprite;
    public Sprite[] walkSprites;
    public int walkSpr;
    public Sprite couchSprite;

    public Transform cage;
    int waitTimer;
    int waitTimerLimit;
    public int waitTimerMin;
    public int waitTimerMax;

    AudioSource aS;
    public AudioClip[] glubs;
    public AudioClip deathGlub;
    int glubTimer;
    int glubLimit;
    public int glubLimitMin;
    public int glubLimitMax;

    int deathTimer;


    // Start is called before the first frame update
    void Awake()
    {
        me = this;
        nMA = GetComponent<NavMeshAgent>();
        waterContentMax = waterContent;
        cc = GetComponent<CapsuleCollider>();
        fishOGPos = fishSprite.gameObject.transform.localPosition;
        aS = GetComponent<AudioSource>();
    }

    void Update()
    {
        fishSprite.color = Color.Lerp(Color.white, blue, ((waterContentMax - waterContent) / waterContentMax));

        if (Input.GetMouseButtonDown(0))
        {
            waitTimer = 0;
            waitTimerLimit = Random.Range(waitTimerMin, waitTimerMax);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if(onCouch)
        {
            Bodsr.sprite = couchSprite;
        }

        if (dead)
        {
            Bodsr.gameObject.transform.localEulerAngles = new Vector3(90f, Bodsr.gameObject.transform.localEulerAngles.y, Bodsr.gameObject.transform.localEulerAngles.z);
            fishSprite.gameObject.transform.localEulerAngles = new Vector3(90f, fishSprite.gameObject.transform.localEulerAngles.y, fishSprite.gameObject.transform.localEulerAngles.z);
        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            if (Vector3.Distance(transform.position, nMA.destination) > 1.6f)
            {
                WalkingToTarget();
            }
            else
            {
                WaitForTarget();
            }

            if (!inShower)
            {
                waterContent--;
            }
            else if (waterContent < waterContentMax)
            {
                waterContent += 5;
            }

            if(waterContent <= 0)
            {
                dead = true;
            }

            glubTimer++;
            if(glubTimer > glubLimit)
            {
                aS.clip = glubs[Random.Range(0, glubs.Length - 1)];
                aS.pitch = Random.Range(0.8f, 1.2f);
                aS.Play();
                glubLimit = Random.Range(glubLimitMin, glubLimitMax);
                glubTimer = 0;
            }

            nMA.destination = pfTarget.transform.position;
        }
        else
        {
            if(deathTimer == 0)
            {
                aS.clip = deathGlub;
                aS.pitch = 1f;
                aS.Play();
                deathTimer = 1;
            }
        }
    }
    

    void WalkingToTarget()
    {
        sprTimer++;
        if (sprTimer > 15)
        {
            if (Bodsr.sprite == standSprite)
            {
                if (walkSpr == 0)
                {
                    walkSpr = 1;
                }
                else
                {
                    walkSpr = 0;
                }
                Bodsr.sprite = walkSprites[walkSpr];
            }
            else
            {
                Bodsr.sprite = standSprite;
            }
            sprTimer = 0;
        }
    }

    void WaitForTarget()
    {
        waitTimer++;
        if(waitTimer > waitTimerLimit)
        {
            pfTarget.transform.position = cage.position + (Random.insideUnitSphere * 6f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        waitTimer = 0;
        if(col.gameObject.tag == "Shower")
        {
            inShower = true;
        }

        if (col.gameObject.tag == "Couch")
        {
            onCouch = true;
            fishSprite.gameObject.transform.localPosition = fishCouchPos;
            couchAS.Play();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Shower")
        {
            inShower = false;
        }

        if (col.gameObject.tag == "Couch")
        {
            onCouch = false;
            fishSprite.gameObject.transform.localPosition = fishOGPos;
            couchAS.Pause();
        }
    }
}
