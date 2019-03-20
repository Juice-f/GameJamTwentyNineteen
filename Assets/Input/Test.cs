using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class Test : MonoBehaviour {
    public InputAction testing;
    public NewInput controls;
    public GameObject testSubject;
    public float subjectsSpeed = 2;
    public bool slowx10 = false;
    float Speed => (slowx10) ? subjectsSpeed / 10 : subjectsSpeed;
    Vector2 direction;
    bool inputActive = false;
    void Awake(){
        Debug.Log(Joystick.current);
        controls.Test.Movement.performed += GetInput;
        controls.Test.Movement.Enable();
    }

    public void GetInput (InputAction.CallbackContext context) {
        
        direction = context.ReadValue<Vector2>();
        inputActive = true;
        
        

    }
    void FixedUpdate(){
        testSubject.GetComponent<Rigidbody2D>().velocity = (inputActive) ? direction.normalized * Speed : Vector2.zero;
        inputActive = false;
        Debug.Log(direction);
    }

    void OnEnable () {
        controls.Test.Movement.Enable();
    }

    void OnDisable () {
        controls.Test.Movement.performed -= GetInput;
        controls.Test.Movement.Disable();
    }
}