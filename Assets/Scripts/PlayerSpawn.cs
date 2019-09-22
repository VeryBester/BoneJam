using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{   
    // Need to set this whenever enters new scene
    public Vector3 respawnPoint;

    private BoneField boneField;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // boneField = GetComponent<BoneField>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bad")
        {
            // boneField.ThrowAllBones();

            GetComponent<SkullController>().StartedShieldDecay = true;
            if (!GetComponent<SkullController>().Invunerable)
            {
                GameObject.FindWithTag("JukeBox").GetComponent<effectsplayer>().Playdeath();
                transform.position = respawnPoint;
            }
        }
    }

}
