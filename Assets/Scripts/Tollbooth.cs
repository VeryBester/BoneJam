using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tollbooth : MonoBehaviour
{
    public int cost = 10;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            GameObject.FindGameObjectWithTag("BoneField").GetComponent<BoneField>().TakeBone(cost, direction);
        }
    }
}
