using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public GameObject ToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            ToFollow.transform.position.x,
            ToFollow.transform.position.y,
            transform.position.z);
    }
}
