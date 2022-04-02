using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    const float moveSpeed = 3f;
    const float jumpVelocity = 6f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool grounded = false;
    bool jumping = false;

    public Transform groundCheck;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (grounded) {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        if (grounded && !jumping && Input.GetButtonDown("Jump")) {
            jumping = true;
        }

        if (Mathf.Abs(horizontal) > 0.01f) {
            sr.flipX = (horizontal >= 0);
        }

        animator.SetBool("Jumping", jumping);
        animator.SetFloat("HorizontalVelocity", Mathf.Abs(rb.velocity.x));
    }

    void FixedUpdate() 
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.15f, ~(1 << LayerMask.NameToLayer("Player")));
        if (hit.collider != null) {
            if (!grounded && jumping) {
                jumping = false;
            }
            grounded = true;
        }
        else {
            grounded = false;
        }
    }

    void OnDrawGizmos()
    {
    }

    public void OnJumpEvent()
    {
        if (jumping) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }
}
