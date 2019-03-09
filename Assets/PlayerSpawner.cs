using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour {
    public static PlayerSpawner ins;
    void Awake () {
        if (ins != null) {
            Destroy (gameObject);
            return;
        }
        ins = this;
        DontDestroyOnLoad (this);
    }
    public GameObject[] chosenCharacter = new GameObject[4];
    public GameObject SelectedCharacter { set => chosenCharacter[value.GetComponent<PlayerCursorController> ().currentPlayer] = value.GetComponent<PlayerCursorController> ().selectedPrefab; }

    public void LoadingStage () {
        SceneManager.LoadScene (1);
        List<GameObject> spawners = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Spawner"));
        for (int i = 0; i < chosenCharacter.Length; i++) {
            if (chosenCharacter[i] != null) {
                CharacterController newChar = Instantiate (chosenCharacter[i].GetComponent<CharacterController> ());
                newChar.ControlledByPlayer = (CharacterController.Player) i;
                int rng = Random.Range (0, spawners.Count);
                newChar.transform.position = spawners[rng].transform.position;
                Destroy (spawners[rng].gameObject);
                spawners.RemoveAt (rng);
            }
        }
    }

}