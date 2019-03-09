using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour, ISlappable
{
    public UnityEvent OnSlap;

    public enum Player { one, two, three, four }
    public float damageTaken = 0;
    [SerializeField]
    Player controlledByPlayer = Player.one;

    public float joy1X, joy1Y;
    void UpdateAxi()
    {
        joy1X = Input.GetAxisRaw(joy1XInputSrc);
        joy1Y = Input.GetAxisRaw(joy1YInputSrc);
    }

    public string joy1XInputSrc, joy1YInputSrc, jumpButtonSrc, bigSlapButtonSrc, smolSlapButtonSrc, gimmickButtonSrc;

    protected virtual void Update()
    {
        UpdateAxi();
    }
    public Player ControlledByPlayer
    {
        get => controlledByPlayer;
        set
        {
            controlledByPlayer = value;
        }
    }

    public virtual void Slap(float slapForce, float damage, GameObject slapOrigin)
    {
        Debug.Log("I was slapped");
        OnSlap.Invoke();
    }

}