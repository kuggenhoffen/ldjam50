using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BalloonController : MonoBehaviour
{

    Rigidbody2D rb;

    public Transform boyancyPosition;
    public Animator animator;
    public Score score;

    bool floating = true;
    bool popping = false;
    int dangerousLayers;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dangerousLayers = LayerMask.GetMask("Spiky", "Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void OnPop()
    {
        floating = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Balloon"));
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
            score.AddScore(100);
        }
    }
}
