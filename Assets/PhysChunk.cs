using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysChunk : MonoBehaviour
{
    public Vector3 Destination;
    public float EstimatedFallTime;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("CollectibleBone"))
        {
            StartCoroutine(Fall());
        }
    }

    public IEnumerator Fall()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(EstimatedFallTime);
        transform.position = Destination;
    }
}
