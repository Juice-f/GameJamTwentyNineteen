using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour {
    public Animation anim;
    public Animation map;
    private void Update () {
        if (SetupStage.ins.SetUIButton == null) {
            SetupStage.ins.SetUIButton = gameObject;
            SetupStage.ins.ClearList ();
            //SetupStage.ins.canMove = true;
            gameObject.SetActive (false);
        }
    }
    public void ToNextMap () {
        //Do something.
        //OnClick.Invoke ();
        anim.Play ();
    }
}