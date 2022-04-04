using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{

    public Camera cam;
    Bounds bounds;
    public Transform target;
    public float parallaxScale;
    public float parallaxOffset;
    Vector3 origin;
    public bool limitToScreen;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        bounds = renderer.bounds;
        if (cam == null) {
            cam = Camera.main;
        }
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(origin.x - (target.position.x + parallaxOffset ) / parallaxScale, transform.position.y, transform.position.z);

        if (limitToScreen) {
            float distance = (transform.position - cam.transform.position).z;
    
            float leftBorder = cam.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x - (bounds.size.x * 0.5f);
            float rightBorder = cam.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + (bounds.size.x * 0.5f);

            float posX = Mathf.Clamp(transform.position.x, leftBorder - 10f, rightBorder);

            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
    }
}
