using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Seagull : MonoBehaviour
{
    public GameObject balloon;
    public Transform firstTarget;
    public Transform lastTarget;
    public SeagullAction seagullAction;
    SpriteRenderer sr;
    Rigidbody2D rb;
    bool firstTargetReached;
    bool balloonReached;
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        firstTargetReached = false;
        balloonReached = false;
    }

    void OnEnable()
    {
        Debug.Log("Seagull awake");
        firstTargetReached = false;
        balloonReached = false;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetVector;
        if (!firstTargetReached) {
            targetVector = firstTarget.position - transform.position;
            if (targetVector.magnitude < 0.5f) {
                firstTargetReached = true;
            }
        }
        else if (!balloonReached) {
            targetVector = balloon.transform.position - transform.position;
            if (balloon.transform.position.x < 3f) {
                balloonReached = true;
                Debug.Log("Balloon too far");
            }
            if (targetVector.magnitude < 0.2f) {

                balloonReached = true;
                Debug.Log("Reached balloon");
            }
        }
        else {
            targetVector = lastTarget.transform.position - transform.position;
            if (targetVector.magnitude < 0.2f) {
                Debug.Log("Seagull returned");
                seagullAction.OnSeagullReturned();
            }
        }

        lastPosition = transform.position;
        transform.position += targetVector.normalized * Time.deltaTime * 2f;

        if (Mathf.Abs(transform.position.x - lastPosition.x) > 0.01f) {
            sr.flipX = (transform.position.x - lastPosition.x >= 0);
        }
    }

    public void OnBalloonReached()
    {
        balloonReached = true;
        firstTargetReached = true;
    }

}
