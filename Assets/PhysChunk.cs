using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysChunk : MonoBehaviour
{
    public Vector3 Destination;
    public float EstimatedFallTime;

    public bool CanMove = true;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("CollectibleBone") && CanMove)
        {
            StartCoroutine(Fall());
            CanMove = false;
        }
    }

    public IEnumerator Fall()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(EstimatedFallTime);
        transform.rotation = Quaternion.identity;
        transform.position = Destination;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
