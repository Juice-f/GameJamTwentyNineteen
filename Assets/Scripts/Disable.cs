using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Disable : MonoBehaviour {
    public UnityEvent play;
    // Start is called before the first frame update
    public void OnAnimationEnd () {
        this.gameObject.SetActive (false);
    }
    public void PlayNextAnimation () {
        play.Invoke ();
    }

}