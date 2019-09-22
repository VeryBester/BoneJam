using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollector : MonoBehaviour
{
    public BoneField BoneField;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 10 && other.gameObject.GetComponent<CollectibleBone>().CanCollect)
        {

            GameObject.FindWithTag("JukeBox").GetComponent<effectsplayer>().Playcollect();
            
            BoneField.GiveBone(other.gameObject.GetComponent<SpriteRenderer>().sprite);
            GameObject.Destroy(other.gameObject);
            
        }
    }
}
