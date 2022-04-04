using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BalloonController : MonoBehaviour
{

    Rigidbody2D rb;
    AudioSource audioSource;

    public Transform boyancyPosition;
    public Animator animator;
    public Controller gameController;

    bool floating = true;
    bool popping = false;
    bool slipStream = false;
    bool seagullBoop = false;
    int dangerousLayers;
    const float SLIP_STREAM_DURATION = 0.5f;
    float slipStreamTime = 0f;

    public List<AudioClip> boopSounds;
    public List<AudioClip> popSounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dangerousLayers = LayerMask.GetMask("Spiky", "Ground");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Balloon"), false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Action")) {
            seagullBoop = true;
        }*/
    }

    void FixedUpdate()
    {
        if (floating) {
            rb.AddForceAtPosition(Physics2D.gravity * -0.99f, boyancyPosition.position, ForceMode2D.Force);
        }
        if (popping) {
            rb.angularDrag = 1000f;
            rb.SetRotation(0f);
        }
        if (floating && slipStream && slipStreamTime > 0f) {
            float forceMultiplier = 4f - 2 * transform.position.y;
            forceMultiplier = Mathf.Clamp(forceMultiplier, 0f, 4f);
            rb.AddForceAtPosition((Vector3.right + Vector3.up * 1.25f) * forceMultiplier, boyancyPosition.position, ForceMode2D.Force);
            slipStreamTime -= Time.fixedDeltaTime;
            if (slipStreamTime <= 0f) {
            slipStream = false;
            }
        }
        if (floating && seagullBoop) {
            seagullBoop = false;
            float forceMultiplier = 2f - 1 * transform.position.y;
            forceMultiplier = Mathf.Clamp(forceMultiplier, 0f, 4f);
            rb.AddForceAtPosition((Vector3.left + Vector3.up * 1.1f) * forceMultiplier, boyancyPosition.position, ForceMode2D.Impulse);
            audioSource.PlayOneShot(boopSounds[Random.Range(0, boopSounds.Count)]);
        }
    }

    public void OnPop()
    {
        floating = false;
        int i = Random.Range(0, popSounds.Count);
        audioSource.PlayOneShot(popSounds[i]);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Balloon"));
        gameController.ShowEndScreen();
    }

    public void OnBoop()
    {
        gameController.AddScore(100);
        audioSource.PlayOneShot(boopSounds[Random.Range(0, boopSounds.Count)]);
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Collision with " + col.collider.gameObject.name + " on layer " + LayerMask.LayerToName(col.collider.gameObject.layer));
        int mask = (1 << col.collider.gameObject.layer);
        Debug.Log("Mask " + mask + ", layers " + dangerousLayers);
        if ((mask & dangerousLayers) > 0) {
            popping = true;
            animator.SetBool("Pop", true);
            Debug.Log("Pop");
        }
        else if ((mask & LayerMask.GetMask("Player")) > 0) {
            OnBoop();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        if (LayerMask.LayerToName(other.gameObject.layer) == "Truck") {
            slipStream = true;
            slipStreamTime = SLIP_STREAM_DURATION;
        }
        else if (LayerMask.LayerToName(other.gameObject.layer) == "Seagull") {
            seagullBoop = true;
            other.gameObject.GetComponent<Seagull>()?.OnBalloonReached();
        }
    }
}
