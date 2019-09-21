using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBone : MonoBehaviour
{
    public bool CanCollect = false;
    private void OnCollisionEnter2D(Collision2D other) {
        CanCollect = true;
    }
}
