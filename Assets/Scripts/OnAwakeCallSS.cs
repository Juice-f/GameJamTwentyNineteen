using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAwakeCallSS : MonoBehaviour {
    void Awake () {
        GameObject.FindObjectOfType<SetupStage> ().LoadingStage ();
    }
}