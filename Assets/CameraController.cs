using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public GameObject leftBoundObject;
    public GameObject rightBoundObject;
    public float minX;
    public float maxX;

    Bounds leftBounds;
    Bounds rightBounds;
    Camera cam;

    float leftBorder;
    float rightBorder;

    // Start is called before the first frame update
    void Start()
    {
        leftBounds = leftBoundObject.GetComponent<SpriteRenderer>().bounds;
        rightBounds = leftBoundObject.GetComponent<SpriteRenderer>().bounds;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosX = target.position.x;
        newPosX = Mathf.Clamp(newPosX, minX, maxX);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

/*        float distance = (leftBoundObject.transform.position - cam.transform.position).z;

        leftBorder = cam.ViewportToWorldPoint (new Vector3(0, 0, leftBoundObject.transform.position.z)).x;
        rightBorder = cam.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + 1f;
        
        float posX = Mathf.Clamp(transform.position.x, leftBorder - 10f, rightBorder);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
*/
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftBorder, 10f, 0f), new Vector3(leftBorder, -10f, 0f));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(rightBorder, 10f, 0f), new Vector3(rightBorder, -10f, 0f));
    }
}
