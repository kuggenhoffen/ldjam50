using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SeagullAction : MonoBehaviour
{
    public GameObject nestOn;
    public GameObject nestOff;
    public GameObject seagull;
    public Controller controller;
    AudioSource audioSource;
    const float ACTION_COOLDOWN = 10f;
    float actionWaitTime = 0f;
    bool triggerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        nestOff.SetActive(true);
        nestOn.SetActive(false);
        seagull.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionWaitTime <= 0f) {
            if (triggerActive && controller.IsControlEnabled() && Input.GetButtonDown("Action")) {
                actionWaitTime = 1f;
                Debug.Log("Seagull enable");
                seagull.transform.position = nestOff.transform.position;
                seagull.SetActive(true);
                nestOff.SetActive(false);
                nestOn.SetActive(true);
                controller.AddScore(50);
                audioSource.Play();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            controller.ActivateHint("[Enter] Bark at bird");
            triggerActive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            controller.DeactivateHint("[Enter] Bark at bird");
            triggerActive = false;
        }
    }

    public void OnSeagullReturned()
    {
        seagull.SetActive(false);
        actionWaitTime = 0f;
        Debug.Log("nest off");
        nestOff.SetActive(true);
        nestOn.SetActive(false);
    }
}
