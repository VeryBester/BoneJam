using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    Vector3 spawnPosition;
    public PlayerSpawn ps;

    private void Start() {
        spawnPosition = transform.GetChild(0).position;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player"){
            Debug.Log("Going in here?");
            ps.respawnPoint = spawnPosition;
        }
    }
}
