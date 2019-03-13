using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapHolder : MonoBehaviour {
    public int levelSceneIndex;

    // Start is called before the first frame update
    public void LoadMap () {
        GameObject.FindObjectOfType<SetupStage> ().StartCoroutine ("LoadingStage");
        SceneManager.LoadScene (levelSceneIndex);

    }

}