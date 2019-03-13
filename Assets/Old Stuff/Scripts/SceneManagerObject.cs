using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour {
    public static SceneManagerObject ins;
    void Awake () {
        if (ins != null) {
            Destroy (gameObject);
            return;
        }
        ins = this;
        DontDestroyOnLoad (gameObject);
    }
    public int[] SceneOrder = new int[3];
    // Start is called before the first frame update
    void Start () {
        if (SceneOrder.Length > 0) {
            OnTriggerChangeScene (SceneOrder[0]);
        }
    }

    public static void OnTriggerChangeScene (int sceneIndex) {
        SceneManager.LoadScene (sceneIndex);
    }
}