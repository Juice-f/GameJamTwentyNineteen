using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHolder : MonoBehaviour {
    [SerializeField] GameObject characterHolder;
    SpriteRenderer currentSprite;
    bool toggle;
    Color defaultColor;
    public Color onSelectColor = Color.white;
    public Color CurrentColorOnButton {
        get => currentSprite.color;
    }
    void Awake () {
        currentSprite = GetComponent<SpriteRenderer> ();
        defaultColor = currentSprite.color;
    }
    public GameObject GetCharacterHolder {
        get {
            return characterHolder;
        }
    }

    public void ChangeColor () {
        toggle = !toggle;
        currentSprite.color = (toggle) ? onSelectColor : defaultColor;
    }
}