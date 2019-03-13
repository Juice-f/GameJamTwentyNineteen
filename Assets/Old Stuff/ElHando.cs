using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElHando : MonoBehaviour
{
    public GameObject IgnoreThisOne;
    public Vector2 moveDir = new Vector2();
    public float moveSpeed = 0;
    List<ISlappable> slappedObjects = new List<ISlappable>();
    public Slapdata slapdata = new Slapdata(0, 0, 0);
    [SerializeField]
    float cloudOffset;
    [SerializeField]
    GameObject cloud;

    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * moveDir * Time.deltaTime, Space.World);
        var angle = Mathf.Atan2(moveDir.x, -moveDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        cloud = Instantiate(cloud);
        cloud.transform.position = transform.position + new Vector3(0,cloudOffset,0);
        Destroy(cloud, 2.5f);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ISlappable>() != null && collision.gameObject != IgnoreThisOne)
        {
            ISlappable toBeSlapped = collision.GetComponent<ISlappable>();

            if (!slappedObjects.Contains(toBeSlapped))
            {
                slappedObjects.Add(toBeSlapped);
                toBeSlapped.Slap(slapdata, gameObject);


            }

        }
        else Destroy(transform.parent);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, cloudOffset, 0), .4f);
    }
}
