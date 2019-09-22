using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBones : MonoBehaviour
{
    public BoneField BoneField;
    public GameObject bone;
    private Camera cam;
    private float speed = 15f;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        TryThrow();
    }

    private void TryThrow()
    {
        if (Input.GetMouseButtonDown(0) && BoneField.ThrowBone())
        {
            GameObject.FindWithTag("JukeBox").GetComponent<effectsplayer>().Playshield();
            Vector3 clickPos = cam.ScreenToWorldPoint(Input.mousePosition);
            float mouseX = clickPos.x;
            float mousey = clickPos.y;
            Transform t = GetComponent<Transform>();

            Vector2 direction = new Vector2(mouseX - t.position.x, mousey - t.position.y);
            float normalizingFactor = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
            direction = new Vector2(direction.x / normalizingFactor, direction.y / normalizingFactor);

            Throw(direction);
            
        }
    }

    public void Throw(Vector3 direction)
    {
        GameObject b = Instantiate(bone);
        b.transform.position = transform.position;
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        rb.angularVelocity = 1000;
    }

    // IEnumerator destroyBone(GameObject b){
    //     yield return new WaitForSeconds(2);
    //     Destroy(b);
    // }
}
