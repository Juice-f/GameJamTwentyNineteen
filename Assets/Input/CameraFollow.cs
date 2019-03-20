using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 offset = new Vector3 (0, 1, 0);
    public float speed = 0.2f;
    void Start () { }

    // Update is called once per frame
    void LateUpdate () {

        Vector3 position = Vector3.Lerp (transform.position, target.transform.position + offset, speed);

        transform.position = position;
    }
}