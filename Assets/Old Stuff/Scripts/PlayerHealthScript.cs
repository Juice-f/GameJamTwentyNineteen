using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour {
    // Start is called before the first frame update
    public CharacterController.Player chosenPlayerOnUI;
    public TMPro.TMP_Text healthUI;
    public Image[] stockUI;
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        switch (chosenPlayerOnUI) {

            case CharacterController.Player.one:
                GetHealth (chosenPlayerOnUI);
                break;
            case CharacterController.Player.two:
                GetHealth (chosenPlayerOnUI);
                break;
            case CharacterController.Player.three:
                GetHealth (chosenPlayerOnUI);
                break;
            case CharacterController.Player.four:
                GetHealth (chosenPlayerOnUI);
                break;
        }
    }

    void GetHealth (CharacterController.Player player) {
        CharacterController[] curPlayers = GameObject.FindObjectsOfType<CharacterController> ();
        for (int i = 0; i < curPlayers.Length; i++) {
            for (int x = 0; x < stockUI.Length; x++) {
                if (curPlayers[i].stocks > x) {
                    Debug.Log (x);
                    stockUI[x].gameObject.SetActive (false);
                    continue;
                }
            }
            if (curPlayers[i].ControlledByPlayer == player) {
                healthUI.text = (curPlayers[i].damageTaken - 100).ToString () + "%";
                return;
            }
            healthUI.text = "";
        }

    }
}