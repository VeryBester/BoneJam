using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SkullController : MonoBehaviour
{
    [Header("Axis Bindings")]
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public KeyCode DashCode = KeyCode.Space;

    [Header("Movement Parameters")]
    public float BaseSpeed = 1f;
    public float MaxSpeed = 10f;
    public float DashSpeed = 10f;
    public bool CanSwim = false;
    public float SwimSpeed = 20;
    public AnimationCurve AccelerationCurve;
    public bool Invunerable = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        Accelerate();
        TrySwim();
        TrySheild();
    }

    private void Accelerate()
    {
        Vector3 impulse = new Vector3(BaseSpeed, 0, 0) *
        AccelerationCurve.Evaluate(
            Input.GetAxis(HorizontalAxis) * GetComponent<Rigidbody2D>().velocity.x / MaxSpeed);

        // Apply acceleration on X axis
        // Positive or negative based on input axis
        GetComponent<Rigidbody2D>().AddForce(Input.GetAxis(HorizontalAxis) * impulse);

        // Cap speed
        Vector3 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        if (currentVelocity.magnitude > MaxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = currentVelocity.normalized * MaxSpeed;
        }
    }

    private void TrySwim()
    {
        if (CanSwim)
        {
            GetComponent<Rigidbody2D>().AddForce(
                new Vector3(0, SwimSpeed, 0) *
                Input.GetAxis(VerticalAxis));
        }
    }

    private void TrySheild()
    {
        if(Input.GetKeyDown(DashCode))
        {
            Vector3 dashVector = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 0, 0).normalized;
            if(GameObject.FindGameObjectWithTag("BoneField").GetComponent<BoneField>().BoneShield(-dashVector))
            {
                StartCoroutine(DoShield());
            }
            
        }
    }

    private IEnumerator DoShield()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Invunerable = true;
        yield return new WaitForSeconds(1);
        Invunerable = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
