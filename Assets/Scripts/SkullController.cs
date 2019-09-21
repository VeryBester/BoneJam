using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SkullController : MonoBehaviour
{
    [Header("Axis Bindings")]
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";

    [Header("Horizontal Movement Parameters")]
    public float BaseSpeed = 1f;
    public float MaxSpeed = 10f;

    [Header("Vertical Movement Parameters")]
    public bool CanSwim = false;
    public float SwimSpeed = 20;

    public AnimationCurve AccelerationCurve;

    // Update is called once per frame
    void FixedUpdate()
    {
        Accelerate();
        TrySwim();
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
}
