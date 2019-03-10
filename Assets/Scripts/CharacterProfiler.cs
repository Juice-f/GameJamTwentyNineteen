using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfiler : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    CharacterController.Player playerID;
    [SerializeField] PlayerCursorController currentPlayer;
    [SerializeField] GameObject potrait;

    private void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    public void DisplayCharacter (Sprite character) {
        potrait.GetComponent<SpriteRenderer> ().sprite = character;
    }

}