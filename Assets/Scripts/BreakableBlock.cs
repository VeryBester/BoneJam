using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{

    /* 
    To Dane: 
        
    Tag the bones as bones 
    
    Love, 
    David
    */
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Bone")){
            Destroy(this);
        }
    }
}
