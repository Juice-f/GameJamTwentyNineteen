using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {
    // Start is called before the first frame update
    public void OnAnimationEnd () {
        this.gameObject.SetActive (false);
    }
}