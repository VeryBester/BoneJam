using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFieldCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            print("BOP");
            collision.gameObject.GetComponent<EnemyDeath>().Damage(1);
            GameObject.FindWithTag("JukeBox").GetComponent<effectsplayer>().PlayMinordmg();
        }
    }
}
