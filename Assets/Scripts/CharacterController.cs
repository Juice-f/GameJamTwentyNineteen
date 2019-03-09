using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour, ISlappable
{
    public UnityEvent OnSlapped;
    public bool isSlapStunned = false;
    public enum Player { one, two, three, four }
    public float damageTaken = 100;
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

 


    public virtual void Slap(Slapdata slapdata, GameObject slapOrigin)
    {
        damageTaken += slapdata.damage;
        StopCoroutine(SlapStun(0));
        StartCoroutine(SlapStun(slapdata.stunTime));
        OnSlapped.Invoke();
        float slapForceWDamage = slapdata.slapForce * (damageTaken / 100);
        Debug.Log(slapForceWDamage);
        Vector2 direction = -((slapOrigin.transform.position - new Vector3(0,5,0)) - transform.position).normalized;
        Debug.Log(direction);
        GetComponent<Rigidbody2D>().AddForce(slapForceWDamage * direction, ForceMode2D.Impulse);
    }

    IEnumerator SlapStun(float time)
    {
        Debug.Log(name + " was slapped");
        isSlapStunned = true;
        Debug.Log(isSlapStunned);
        yield return new WaitForSeconds(time);
 
        isSlapStunned = false;
        Debug.Log(isSlapStunned);

    }


}

