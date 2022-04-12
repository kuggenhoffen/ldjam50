using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    enum JumpingState {
        GROUNDED,
        JUMPING,
        NOT_GROUNDED
    };

    const float moveSpeed = 3f;
    const float jumpVelocity = 6f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource audioSource;
    bool grounded;
    JumpingState jumpingState = JumpingState.NOT_GROUNDED;
    bool active = true;
    public Transform groundCheck;
    public Animator animator;
    public List<AudioClip> stepSounds;
    public List<AudioClip> jumpSounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = Mathf.Abs(rb.velocity.x);

        switch (jumpingState) {
            default:
            case JumpingState.GROUNDED:
                break;
            case JumpingState.JUMPING:
                if (!grounded) {
                    jumpingState = JumpingState.NOT_GROUNDED;
                }
                break;
            case JumpingState.NOT_GROUNDED:
                if (grounded) {
                    jumpingState = JumpingState.GROUNDED;
                }
                break;
        }

        animator.SetBool("Jumping", jumpingState != JumpingState.GROUNDED);
        animator.SetFloat("HorizontalVelocity", hVelocity);

        if (!active) {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (jumpingState == JumpingState.GROUNDED) {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            if (hVelocity > 0.1f && !audioSource.isPlaying) {
                audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Count)]);
            }
        }

        if (jumpingState == JumpingState.GROUNDED && Input.GetButtonDown("Jump")) {
            jumpingState = JumpingState.JUMPING;
            audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Count)]);
        }

        if (Mathf.Abs(horizontal) > 0.01f) {
            sr.flipX = (horizontal >= 0);
        }
    }

    void FixedUpdate() 
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        if (hit.collider != null) {
            grounded = true;
        }
        else {
            grounded = false;
        }
    }


    public void OnJumpEvent()
    {
        if (jumpingState == JumpingState.JUMPING) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }

    public void SetActive(bool newActive)
    {
        active = newActive;
    }
}
