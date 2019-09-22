using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Move : MonoBehaviour
{
    public Dialog ToTrigger;
    public Vector3 Destination;
    public float speed;

    public GameObject ToMove;

    void Start() {
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        while(Vector3.Distance(ToMove.transform.position, Destination) >= 2)
        {
            ToMove.transform.position += (Destination - ToMove.transform.position).normalized * speed * Time.deltaTime;
            yield return null;
        }

        ToTrigger.Trigger();
    }


}
