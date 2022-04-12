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
    public Camera cam;

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
        //newPosX = Mathf.Clamp(newPosX, minX, maxX);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

        float distance = (target.position - cam.transform.position).z;
 
        float leftBorder = cam.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
        float rightBorder = cam.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;
        float maxLeft = leftBoundObject.transform.position.x - leftBounds.size.x / 2f;
        float maxRight = rightBoundObject.transform.position.x + rightBounds.size.x / 2f;
        if (leftBorder < maxLeft) {
            transform.position = new Vector3(transform.position.x + (maxLeft - leftBorder), transform.position.y, transform.position.z);
        }
        if (rightBorder > maxRight) {
            transform.position = new Vector3(transform.position.x + (maxRight - rightBorder), transform.position.y, transform.position.z);
        }

/*        float distance = (leftBoundObject.transform.position - cam.transform.position).z;

        leftBorder = cam.ViewportToWorldPoint (new Vector3(0, 0, leftBoundObject.transform.position.z)).x;
        rightBorder = cam.ViewportToWorldPoint (new Vector3(0, 0, distance)).x + 1f;
        
        float posX = Mathf.Clamp(transform.position.x, leftBorder - 10f, rightBorder);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
*/
    }

    void OnDrawGizmos()
    {
        float distance = (target.position - cam.transform.position).z;
 
        float leftBorder = cam.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
        float rightBorder = cam.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftBorder, 5f, 0f), new Vector3(leftBorder, -5f, 0f));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(rightBorder, 5f, 0f), new Vector3(rightBorder, -5f, 0f));
    }

}
