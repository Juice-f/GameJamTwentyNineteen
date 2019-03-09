using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlappy : MonoBehaviour, ISlappable
{
    private void OnMouseDown()
    {
        Slap(new Slapdata(0,0,0), gameObject);
    }
    public void Slap(Slapdata s, GameObject g)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000), ForceMode2D.Impulse);
    }
}
