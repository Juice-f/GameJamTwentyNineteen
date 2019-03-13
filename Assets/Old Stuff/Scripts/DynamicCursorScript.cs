using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCursorScript : MonoBehaviour {
    [SerializeField] int amountOfPlayers;
    [SerializeField] int activeCursors;
    [SerializeField] PlayerCursorController[] cursorControllers;
    bool[] areActive;
    void Start () {
        areActive = new bool[cursorControllers.Length];
    }

    // Update is called once per frame
    void Update () {
        EnableCursors ();

    }

    private void EnableCursors () {
        amountOfPlayers = (amountOfPlayers > 4) ? 4 : Input.GetJoystickNames ().Length;
        for (int i = 0; i < amountOfPlayers; i++) {
            if (cursorControllers[i].gameObject.activeInHierarchy) {
                continue;
            }
            cursorControllers[i].gameObject.SetActive (true);
        }
    }
    public void DisableCursors () {
        for (int i = 0; i < cursorControllers.Length; i++) {
            if (amountOfPlayers < i) {
                cursorControllers[i].gameObject.SetActive (false);
            }
        }
    }

    //If the amount of players does not fully activate all cursors after all of them were enabled, disable the remaining cursors that are not within the array.

}