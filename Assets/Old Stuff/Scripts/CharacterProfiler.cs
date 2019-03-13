using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfiler : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    [SerializeField]CharacterController.Player playerID;
    public CharacterController.Player GetPotraitPlayerID => playerID;
    [SerializeField] Sprite defaultPotrait;

    private void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        spriteRenderer.sprite = defaultPotrait;
    }

    public void DisplayCharacter (Sprite character) {
        spriteRenderer.sprite = character;
    }

}