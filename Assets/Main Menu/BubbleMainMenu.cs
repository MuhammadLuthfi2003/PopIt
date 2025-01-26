using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMainMenu : MonoBehaviour
{
    public float speed = 1;
    public float offset = 1;
    public Vector3 pointA;
    public Vector3 pointB;
    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        pointB = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
    }
}
