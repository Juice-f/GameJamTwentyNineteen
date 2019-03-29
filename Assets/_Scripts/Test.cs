using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class Test : MonoBehaviour {
    public InputMaster controls;
    Vector2 input;
    public float speed;
    Rigidbody2D rb;
    private void Awake () {
        controls.Hej.Move.performed += Move;
        rb = GetComponent<Rigidbody2D> ();

    }

    private void OnEnable () {
        controls.Hej.Move.Enable ();
    }

    private void OnDisable () {
        controls.Hej.Move.Disable ();
    }

    private void Move (InputAction.CallbackContext obj) {
        input = obj.ReadValue<Vector2> ();
    }

    void FixedUpdate () {
        rb.velocity = ((input.x > 0.5f && input.y > 0.5f) && (input.x < -0.5f && input.y < -0.5f)) ? Vector2.zero : input.normalized * speed;
        Debug.Log (input);
    }
}