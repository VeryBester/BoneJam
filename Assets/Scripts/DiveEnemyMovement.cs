using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveEnemyMovement : MonoBehaviour
{

    Rigidbody2D rb;

    CircleCollider2D[] colliders;
    float speed = 3f;
    bool playerFound = false;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<CircleCollider2D>();

        // This is the collider for the player trigger
        colliders[0].radius = 5f;
        StartCoroutine(patrol(speed));
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player"){
            playerFound = true;
        }
    }

    // Routine enemy does when it first starts, just walks back and forth
    IEnumerator patrol(float s){
        if(!playerFound){
            float time = 0f;
            rb.velocity = new Vector3(s, 0f);
            while(time < 5f){
                time += Time.deltaTime;
                if(playerFound){            
                    rb.velocity = new Vector3(0f, 0f);
                    StartCoroutine(notice());
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

    // When the enemy first notices the player
    IEnumerator notice(){
        GameObject player = GameObject.FindWithTag("Player");
        bool right = player.transform.position.x > transform.position.x; 
        if(right){
            rb.AddForce(new Vector2(0, 100f));
        }
        else{
            rb.AddForce(new Vector2(0, -100f));
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(attack());
        yield return null;
    }

    // Enemy initiating and executing attack
    IEnumerator attack(){

        GameObject player = GameObject.FindWithTag("Player");
        bool right = player.transform.position.x > transform.position.x;

        if(right){
            rb.AddForce(new Vector2(-100f, 0f));
        }
        else{
            rb.AddForce(new Vector2(100f, 0f));
        }
        
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(new Vector2(0f, 500f));
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(0f,0f);
        rb.gravityScale = 0;
        
        player = GameObject.FindWithTag("Player");
        Vector2 direction = getDirection(transform, player.transform);

        rb.velocity = direction * 20f;
        while(transform.position.y > player.transform.position.y){

            yield return null;
        }

        rb.velocity = new Vector2(0f,0f);

        StartCoroutine(waitForNextAttack());
        yield return null;
    }

    IEnumerator waitForNextAttack(){
        GameObject player = GameObject.FindWithTag("Player");
        bool right = player.transform.position.x > transform.position.x; 
        
        if(right){
            rb.velocity = new Vector2(-2f, 0f);
        }
        else{
            rb.velocity = new Vector2(2f, 0f);
        }
        float timeTillNextAttack = Random.Range(3f, 5f);
        float time = 0f;
        while(time < timeTillNextAttack){
            time += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(attack());

    }

    // Helper method that returns a normalized vector for direction
    Vector2 getDirection(Transform from, Transform to){
        float diffX =  to.position.x - from.position.x; 
        float diffY =  to.position.y - from.position.y;

        float normalizingFactor = Mathf.Sqrt(diffX*diffX + diffY*diffY);
        return new Vector2(diffX/ normalizingFactor, diffY/normalizingFactor); 
    
    }

}
