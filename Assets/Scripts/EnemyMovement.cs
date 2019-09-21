using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D rb;

    CircleCollider2D[] colliders;
    float speed = 3f;
    bool playerFound = false;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<CircleCollider2D>();

        // This is the collider for the player trigger
        //colliders[0].radius = 5f;
        StartCoroutine(patrol(speed));
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player"){
            Debug.Log("Entering");
            playerFound = true;
        }
    }

    IEnumerator patrol(float s){
        if(!playerFound){
            float time = 0f;
            rb.velocity = new Vector3(s, 0f);
            while(time < 5f){
                time += Time.deltaTime;
                if(playerFound){
                    rb.velocity = new Vector3(0f, 0f);
                    StartCoroutine(jump());
                    break;
                }
                else{
                    yield return null;
                }
            }
            if(!playerFound)
                StartCoroutine(patrol(-1*s));
        }

    }

    IEnumerator jump(){
        rb.velocity = new Vector3(0, 30, 0);

        yield return null;
    }

}