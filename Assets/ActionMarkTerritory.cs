using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ActionMarkTerritory : MonoBehaviour
{
    public Controller controller;
    AudioSource audioSource;

    const float ACTION_COOLDOWN = 15f;
    float actionWaitTime = 0f;

    bool triggerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && actionWaitTime <= 0f && Input.GetButtonDown("Action") && controller.IsControlEnabled()) {
            controller.AddScore(50);
            actionWaitTime = ACTION_COOLDOWN;
            audioSource.Play();
        }
        else if (actionWaitTime > 0f) {
            actionWaitTime -= Time.deltaTime;
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            controller.ActivateHint("[Enter] Mark territory");
            triggerActive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            controller.DeactivateHint("[Enter] Mark territory");
            triggerActive = false;
        }
    }
}
