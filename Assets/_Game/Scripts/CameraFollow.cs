using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        targetPlayer();
       
    }
    private void targetPlayer()
    {
        transform.position = Vector3.Lerp(transform.position,
           target.position  + offset, Time.deltaTime * speed);
    }
}
