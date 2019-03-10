using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupStage : MonoBehaviour {
    public static SetupStage ins;
    [SerializeField] GameObject[] listOfSelectedPlayers;
    [System.Serializable] class UIElements {
        public GameObject uiButton;
    }

    [SerializeField] UIElements elements;

    public void SetAddCharacterToArr (GameObject value, PlayerCursorController cursor) {
        if (listOfSelectedPlayers.Length < 1) {
            listOfSelectedPlayers = new GameObject[GameObject.FindObjectsOfType<PlayerCursorController> ().Length];
        }
        listOfSelectedPlayers[cursor.currentPlayer] = value;
        Debug.Log ("Adding Character To List");
    }
    public void RemoveCharacterFromArr (PlayerCursorController cursor) {
        listOfSelectedPlayers[cursor.currentPlayer] = null;
        Debug.Log ("Removing Character From List");
    }
    public GameObject[] GetList => listOfSelectedPlayers;
    void Update () {
        if (elements.uiButton) {
            elements.uiButton.SetActive (HaveAllPlayersPickedACharacter);
            Debug.Log ("Found Button");
        }
    }

    bool HaveAllPlayersPickedACharacter {
        get {
            if (listOfSelectedPlayers.Length == 0) {
                return false;
            }
            foreach (GameObject v in listOfSelectedPlayers) {
                if (v == null) return false;
            }
            return true;
        }
    }

    void Awake () {
        if (ins != null) {
            Destroy (gameObject);
            return;
        }
        ins = this;
        DontDestroyOnLoad (gameObject);
        elements.uiButton.SetActive (false);
    }
    public void LoadingStage () {
        Debug.Log ("Called");
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag ("Spawner");
        if (spawnPoints.Length != 0) {
            for (int i = 0; i < listOfSelectedPlayers.Length; i++) {
                int rng = Random.Range (0, spawnPoints.Length - 1);
                CharacterController newPlayer = Instantiate (listOfSelectedPlayers[i].GetComponent<CharacterController> (), spawnPoints[rng].transform.position, Quaternion.identity);
                Debug.Log ("Spawned " + newPlayer.name + " for " + (CharacterController.Player) i + "! ");
                newPlayer.ControlledByPlayer = (CharacterController.Player) i;

            }
        }
    }
}