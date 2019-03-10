using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour {
    public Animation anim;
    public Animation map;
    public void ToNextMap () {
        //Do something.
        //OnClick.Invoke ();
        anim.Play ();
        Debug.Log ("Do something");
    }
}