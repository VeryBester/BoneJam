using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject CollectiblePrefab;
    public int MinDrop;
    public int MaxDrop;
    public float DropForceMagnitude;
    public int HP = 2;

    public void Damage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
            Die();
    }

    public void Die()
    {

        int amt = Random.Range(MinDrop, MaxDrop);
        for (int i = 0; i < amt; i++)
        {
            GameObject g = GameObject.Instantiate(CollectiblePrefab);
            g.GetComponent<CollectibleBone>().CanCollect = true;
            g.transform.position = transform.position;
            g.GetComponent<Rigidbody2D>().AddForce(new Vector3(
                Random.Range(-1, 1),
                Random.Range(-1, 1),
                0
                ).normalized * DropForceMagnitude);
        }

        GameObject.Destroy(this.gameObject);
    }
}
