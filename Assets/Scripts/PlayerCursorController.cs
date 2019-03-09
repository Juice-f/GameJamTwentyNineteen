using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorController : MonoBehaviour {
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float sensitivity = 3;
    SpriteRenderer sprite;
    [Range (0, 3)]
    public int currentPlayer;
    public GameObject selectedPrefab;
    [System.Serializable] class PlayerInput {
        public string xAxisInput;
        public string yAxisInput;
        public string selectButton;
    }
    void Awake () {
        sprite = GetComponent<SpriteRenderer> ();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update () {
        float widthRel = sprite.sprite.texture.width / (Screen.width); //relative width
        float heightRel = sprite.sprite.texture.height / (Screen.height); //relative height

        Vector3 viewPos = Camera.main.WorldToViewportPoint (this.transform.position);
        viewPos.x = Mathf.Clamp (viewPos.x, widthRel, 1 - widthRel);
        viewPos.y = Mathf.Clamp (viewPos.y, heightRel, 1 - heightRel);
        this.transform.position = Camera.main.ViewportToWorldPoint (viewPos);
        transform.Translate (new Vector2 (Input.GetAxis (playerInput.xAxisInput), -Input.GetAxis (playerInput.yAxisInput)) * sensitivity);
    }
    void FixedUpdate () {
        if (Input.GetButtonDown (playerInput.selectButton)) {
            if (!Physics2D.Raycast (transform.position, transform.forward, 2)) {
                return;
            }
            if (Physics2D.Raycast (transform.position, transform.forward, 2).collider.CompareTag ("Button")) {
                PlayerSpawner.ins.SelectedCharacter = Physics2D.Raycast (transform.position, transform.forward, 2).collider.GetComponent<CharacterHolder> ().characterToSelect;
                Debug.Log ("Character Selected!");
            }
        }

    }

}