using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{   
    // Need to set this whenever enters new scene
    public Transform respawnPoint;

    private BoneField boneField;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // boneField = GetComponent<BoneField>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bad"){
            // boneField.ThrowAllBones();
            transform.position = respawnPoint.position;
        }
    }

}
