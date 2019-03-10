using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour, ISlappable {
    public UnityEvent OnSlapped;
    public bool isSlapStunned = false;
    public enum Player { one, two, three, four }
    public float damageTaken = 100;
    [SerializeField]
    Player controlledByPlayer = Player.one;

    public float joy1X, joy1Y;
    private void Start () {
        Debug.Log ("Start Called");
        switch (controlledByPlayer) {
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

    protected virtual void Update () {
        UpdateAxi ();
    }
    public Player ControlledByPlayer {
        get => controlledByPlayer;
        set {
            controlledByPlayer = value;
        }
    }

    public virtual void Slap (Slapdata slapdata, GameObject slapOrigin) {
        damageTaken += slapdata.damage;
        StopCoroutine (SlapStun (0));
        StartCoroutine (SlapStun (slapdata.stunTime * damageTaken / 100));
        OnSlapped.Invoke ();
        float slapForceWDamage = slapdata.slapForce * (damageTaken / 100);
        Debug.Log (slapForceWDamage);
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

}