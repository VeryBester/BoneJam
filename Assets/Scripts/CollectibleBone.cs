using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBone : MonoBehaviour
{
    public bool CanCollect = false;
    public float VelocityKillThreshold = 0.2f;
    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag.Equals("Enemy") &&
            GetComponent<Rigidbody2D>().velocity.magnitude > VelocityKillThreshold
            )
        {
            other.gameObject.GetComponent<EnemyDeath>().Damage(10);
        }


        CanCollect = true;
    }
}
