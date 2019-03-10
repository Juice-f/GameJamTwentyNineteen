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

    bool activate;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update () {
        LimitCursorToScreenSize ();
        transform.Translate (new Vector2 (Input.GetAxis (playerInput.xAxisInput), -Input.GetAxis (playerInput.yAxisInput)) * sensitivity);

    }

    private void LimitCursorToScreenSize () {
        float widthRel = sprite.sprite.texture.width / (Screen.width); //relative width
        float heightRel = sprite.sprite.texture.height / (Screen.height); //relative height

        Vector3 viewPos = Camera.main.WorldToViewportPoint (this.transform.position);
        viewPos.x = Mathf.Clamp (viewPos.x, widthRel, 1 - widthRel);
        viewPos.y = Mathf.Clamp (viewPos.y, heightRel, 1 - heightRel);
        this.transform.position = Camera.main.ViewportToWorldPoint (viewPos);
    }

    bool toggle;
    void FixedUpdate () {
        activate = (Input.GetButtonDown (playerInput.selectButton)) ? true : false;
        if (activate) {
            if (!Physics2D.Raycast (transform.position, transform.forward)) {
                return;
            }
            GameObject button = Physics2D.Raycast (transform.position, transform.forward).collider.gameObject;
            if (button.CompareTag ("Button")) {
                CharacterHolder selectedButton = button.GetComponent<CharacterHolder> ();
                //selectedButton.onSelectColor = (selectedButton.onSelectColor != sprite.color) ? sprite.color : selectedButton.onSelectColor;
                toggle = !toggle;
                selectedPrefab = (toggle) ? selectedButton.GetCharacterHolder : null;

                selectedButton.ChangeColor ();
                Debug.Log ("Character Selected!");
            }
            if (button.CompareTag ("UIButton")) {
                button.GetComponent<ButtonScript> ().ToNextMap ();
                Debug.Log ("To the next map");
            }
        }

    }

}