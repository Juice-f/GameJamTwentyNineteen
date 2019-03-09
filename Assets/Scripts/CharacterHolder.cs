using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHolder : MonoBehaviour {
    [SerializeField] GameObject characterHolder;
    public GameObject GetCharacterHolder {
        get {
            return characterHolder;
        }
    }
}