using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour, ISlappable {
    public UnityEvent OnSlapped;
    public bool isSlapStunned = false;
    public enum Player { one, two, three, four }
    public float damageTaken = 100;
    public int stocks = 3;
    [SerializeField]

    Player controlledByPlayerHidden = Player.one;
    Player controlledByPlayer {
        get => controlledByPlayerHidden;
        set {
            switch (value) {

                case Player.one:
                    SetInput ("P1");
                    break;
                case Player.two:
                    SetInput ("P2");
                    break;
                case Player.three:
                    SetInput ("P3");
                    break;
                case Player.four:
                    SetInput ("P4");
                    break;
            }

            controlledByPlayerHidden = value;

        }
    }

    public float joy1X, joy1Y;
    public virtual void Start () {
        canMove = true;
        //        Debug.Log("Start Called");

    }
    void UpdateAxi () {
        joy1X = Input.GetAxisRaw (joy1XInputSrc);
        joy1Y = Input.GetAxisRaw (joy1YInputSrc);
    }

    private void SetInput (string player) {
        joy1XInputSrc = player + "Horizontal";
        joy1YInputSrc = player + "Vertical";
        jumpButtonSrc = player + "JumpButton";
        bigSlapButtonSrc = player + "BSlapButton";
        smolSlapButtonSrc = player + "SSlapButton";
        gimmickButtonSrc = player + "GimmickButton";
        Debug.Log (player + " has their inputs set!");

    }

    public string joy1XInputSrc, joy1YInputSrc, jumpButtonSrc, bigSlapButtonSrc, smolSlapButtonSrc, gimmickButtonSrc;
    public bool canMove = true;
    protected virtual void Update () {
        if (canMove)
            UpdateAxi ();
    }
    public Player ControlledByPlayer {
        get => controlledByPlayer;
        set {
            controlledByPlayer = value;
        }
    }

    public virtual void Slap (Slapdata slapdata, GameObject slapOrigin) {
        Debug.Log ("aaa " + slapOrigin.name);
        damageTaken += slapdata.damage;
        StopCoroutine (SlapStun (0));
        StartCoroutine (SlapStun (slapdata.stunTime * damageTaken / 100));
        OnSlapped.Invoke ();
        float slapForceWDamage = slapdata.slapForce * (damageTaken / 100);
        //        Debug.Log (slapForceWDamage);
        Vector2 direction = -((slapOrigin.transform.position - new Vector3 (0, 5, 0)) - transform.position).normalized;
        Debug.Log (direction);
        GetComponent<Rigidbody2D> ().AddForce (slapForceWDamage * direction, ForceMode2D.Impulse);
    }

    IEnumerator SlapStun (float time) {
        Debug.Log (name + " was slapped");
        isSlapStunned = true;
        Debug.Log (isSlapStunned);
        yield return new WaitForSeconds (time);

        isSlapStunned = false;
        Debug.Log (isSlapStunned);

    }
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Deathzone")) {
            if (stocks == 0) {
                //Have character not respawn

                //Game! Remaining player won!
                Debug.Log (GameObject.FindObjectsOfType<CharacterController> ().Length);
                if (GameObject.FindObjectsOfType<CharacterController> ().Length == 2) {
                    Debug.Log ("Game!");
                    foreach (var item in SetupStage.ins.GetList) {
                        GameObject.FindGameObjectWithTag ("VictoryMessage").GetComponent<TMPro.TMP_Text> ().enabled = true;
                        canMove = false;
                        //gameObject.SetActive (false);
                        StartCoroutine ("ToTheSelectionScreen");
                        return;
                        //Enable victory screen
                        //Wait one second.
                        //Disable victory screen and load selectin scene.
                    }
                }
                gameObject.SetActive (false);

            }
            int rng = Random.Range (0, GameObject.FindGameObjectsWithTag ("Respawners").Length);
            transform.position = GameObject.FindGameObjectsWithTag ("Respawners") [rng].transform.position;
            damageTaken = 100;
            --stocks;
        }
    }

    IEnumerator ToTheSelectionScreen () {
        yield return new WaitForSeconds (3);
        GameObject.FindGameObjectWithTag ("VictoryMessage").GetComponent<TMPro.TMP_Text> ().enabled = false;
        SceneManager.LoadScene (1);
    }

}