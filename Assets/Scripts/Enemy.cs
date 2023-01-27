using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0;
    public List<Transform> waypoints;
    private int waypointIndex;
    private float range;
    private Transform tf;
    private Vector3 startpositie;

    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;
        tf = GetComponent<Transform>();
        startpositie = tf.position;

    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.LookAt(waypoints[waypointIndex]);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, waypoints[waypointIndex].position)< range)
        {
            waypointIndex++;
            if(waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if (tf.position.y < -3)
        { 
            tf.position = startpositie;
        }
    }
}
