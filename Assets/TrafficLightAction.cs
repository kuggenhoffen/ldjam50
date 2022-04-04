using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightAction : MonoBehaviour
{

    public Animator sparkAnimator;
    public GameObject trafficLightOn;
    public GameObject trafficLightOff;
    public GameObject truck;
    public Controller controller;

    const float ACTION_COOLDOWN = 10f;
    float actionWaitTime = 0f;
    float sparkTime;

    bool triggerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        trafficLightOff.SetActive(true);
        trafficLightOn.SetActive(false);
        sparkTime = Random.Range(2f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (sparkTime > 0f) {
            sparkTime -= Time.deltaTime;
        }
        else {
            sparkAnimator.ResetTrigger("TriggerSpark");
            sparkAnimator.SetTrigger("TriggerSpark");
            sparkTime = Random.Range(2f, 10f);
        }

        if (actionWaitTime > 0f) {
            actionWaitTime -= Time.deltaTime;
        }
        else {
            if (triggerActive && Input.GetButtonDown("Action") && controller.IsControlEnabled()) {
                Debug.Log("Truck enable");
                truck.SetActive(true);
                actionWaitTime = ACTION_COOLDOWN;
                trafficLightOff.SetActive(false);
                trafficLightOn.SetActive(true);
                controller.AddScore(50);
            }
            else {
                if (!trafficLightOff.activeSelf) {
                    Debug.Log("Traffic light off");
                    trafficLightOff.SetActive(true);
                    trafficLightOn.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            controller.ActivateHint("[Enter] Bite wire");
            triggerActive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            controller.DeactivateHint("[Enter] Bite wire");
            triggerActive = false;
        }
    }
}
