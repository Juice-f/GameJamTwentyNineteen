using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorList : MonoBehaviour {
    public List<PlayerCursorController> playerCursors;
    bool[] playerCheck;
    [SerializeField] GameObject buttonToMapRoom;

    // Start is called before the first frame update
    void Start () {
        buttonToMapRoom.SetActive (false);
        for (int i = 0; i < playerCursors.Count; i++) {
            PlayerCursorController cursor = playerCursors[i];
            if (!cursor.gameObject.activeInHierarchy) {
                playerCursors.RemoveAt (i);
            }
        }
        playerCheck = new bool[playerCursors.Count];

    }
    public bool AllHaveCharacter {
        get {

            for (int i = 0; i < playerCheck.Length; i++) {
                if (!playerCheck[i]) return false;
            }
            return true;

        }
    }
    // Update is called once per frame
    bool doOnce = true;
    void Update () {
        for (int i = 0; i < playerCursors.Count; i++) {
            playerCheck[i] = (playerCursors[i].selectedPrefab != null) ? true : false;
        }
        if (AllHaveCharacter) {
            buttonToMapRoom.SetActive (true);
        } else {
            buttonToMapRoom.SetActive (false);
        }
    }
}