using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Random.Range(0.1f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
        if (transform.position.x >= 9) {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = new Vector3(-9, Random.Range(0f, 1.85f), 0f);
        velocity = Random.Range(0.1f, 0.3f);
    }
}
