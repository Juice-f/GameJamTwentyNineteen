using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHolder : MonoBehaviour {

    [SerializeField] GameObject characterPrefab;
    public PlayerCursorController cursor;
    SpriteRenderer currentSprite;
    bool toggle;
    Color defaultColor;
    public Color onSelectColor = Color.white;
    public Color CurrentColorOnButton {
        get => currentSprite.color;
    }
    public GameObject GetCharacterReference => characterPrefab;
    void Awake () {
        currentSprite = GetComponent<SpriteRenderer> ();
        defaultColor = currentSprite.color;
    }

    public void ChangeColor () {
        toggle = !toggle;
        currentSprite.color = (toggle) ? onSelectColor : defaultColor;
    }

    public void OnButtonTrigger (PlayerCursorController origin) {
        if (SetupStage.ins.GetList.Length != 0 && SetupStage.ins.GetList[origin.currentPlayer] == characterPrefab) {
            SetupStage.ins.RemoveCharacterFromArr (origin);
            return;
        }
        SetupStage.ins.SetAddCharacterToArr (characterPrefab, origin);
    }
}