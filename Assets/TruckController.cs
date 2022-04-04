using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TruckController : MonoBehaviour
{
    AudioSource audioSource;
    float velocity = 6f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    void Awake()
    {
        transform.position = Vector3.left * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * velocity * Time.deltaTime;
        if (transform.position.x > 10f && !audioSource.isPlaying) {
            transform.position = Vector3.left * 10f;
            gameObject.SetActive(false);
        }

    }
}
