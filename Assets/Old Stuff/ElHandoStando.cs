using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElHandoStando : MonoBehaviour
{
    public Slapdata slapdata;
    public GameObject dad;
    public float timeUntilReslap;
    List<ISlappable> slappables = new List<ISlappable>();
    public float moveSpeed;
    public void StartSlappin(float lifeTime, Slapdata slapdata, GameObject daddy, float reslap, float moveSpeed)
    {
        this.slapdata = slapdata;
        this.timeUntilReslap = reslap;
        this.moveSpeed = moveSpeed;
        dad = daddy;
        Destroy(gameObject, lifeTime);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    IEnumerator List(ISlappable slappable)
    {


        slappables.Add(slappable);
        yield return new WaitForSeconds(timeUntilReslap);
        slappables.Remove(slappable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ISlappable>() != null && collision.gameObject != dad)
        {
            ISlappable slappable = collision.GetComponent<ISlappable>();
            if (!slappables.Contains(slappable))
            {
                slappable.Slap(slapdata, gameObject);
                StartCoroutine(List(slappable));
            }


        }



    }

}
