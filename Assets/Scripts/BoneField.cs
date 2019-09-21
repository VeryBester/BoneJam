using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneField : MonoBehaviour
{

    [Header("General Parameters")]
    public float BoneFieldSize = .5f;
    public float MaxFieldDistance = 4f;
    public Vector3 FieldResetOffset = Vector3.zero;
    public Vector3 FollowOffset = Vector3.zero;
    public int NumberOfFields = 20;
    public float SmoothFactor = 1;
    public SkullController SkullController;
    private Vector3 ReferenceVelocity = Vector3.zero;
    private List<GameObject> BoneFields;

    [Header("Swim Parameters")]
    public float SwimSpeed = 5f;
    public float SwimThreshold = .1f;
    public bool CanSwim = false;

    [Header("Spring Parameters")]
    public float FieldSpringDistance = 0.2f;
    public float FieldSpringDampingRatio = 0;
    public float FieldSpringFrequency = 1;

    [Header("Field Prefab")]
    public GameObject BoneFieldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBoneFields();
    }

    public bool ThrowBone()
    {
        if(BoneFields.Count > 0)
        {
            GameObject toDestroy = BoneFields[BoneFields.Count - 1];
            BoneFields.RemoveAt(BoneFields.Count - 1);
            GameObject.Destroy(toDestroy);

            return true;
        }

        return false;
    }

    public void GiveBone()
    {
        GameObject newBoneField = GameObject.Instantiate(BoneFieldPrefab);
        newBoneField.transform.position = transform.position;
        newBoneField.transform.localScale = new Vector3(BoneFieldSize, BoneFieldSize, BoneFieldSize);
        SpringJoint2D jointRef = newBoneField.GetComponent<SpringJoint2D>();
        jointRef.connectedBody = GetComponent<Rigidbody2D>();
        jointRef.distance = FieldSpringDistance;
        jointRef.dampingRatio = FieldSpringDampingRatio;
        jointRef.frequency = FieldSpringFrequency;

        BoneFields.Add(newBoneField);
    }

    private void SpawnBoneFields()
    {
        BoneFields = new List<GameObject>();

        for (int i = 0; i < NumberOfFields; i++)
        {
            GameObject newBoneField = GameObject.Instantiate(BoneFieldPrefab);
            newBoneField.transform.position = transform.position;
            newBoneField.transform.localScale = new Vector3(BoneFieldSize, BoneFieldSize, BoneFieldSize);
            SpringJoint2D jointRef = newBoneField.GetComponent<SpringJoint2D>();
            jointRef.connectedBody = GetComponent<Rigidbody2D>();
            jointRef.distance = FieldSpringDistance;
            jointRef.dampingRatio = FieldSpringDampingRatio;
            jointRef.frequency = FieldSpringFrequency;

            BoneFields.Add(newBoneField);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnforceMaxDistance();
        SmoothFollow();
    }

    void FixedUpdate()
    {
    }

    private void SmoothFollow()
    {
        ReferenceVelocity = GetComponent<Rigidbody2D>().velocity;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            SkullController.transform.position + FollowOffset,
            ref ReferenceVelocity,
            SmoothFactor,
            SkullController.MaxSpeed);
        GetComponent<Rigidbody2D>().velocity = ReferenceVelocity;
    }

    private void EnforceMaxDistance()
    {
        bool grounded = false;
        bool touching = false;

        foreach (GameObject g in BoneFields)
        {
            if (Vector3.Distance(SkullController.transform.position, g.transform.position) <= SwimThreshold)
            {
                touching = true;
            }
            if (Vector3.Distance(transform.position, g.transform.position) > MaxFieldDistance)
            {
                g.transform.position = transform.position + FieldResetOffset;
                g.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            ContactPoint2D[] point2Ds = new ContactPoint2D[20];
            int pointCount = g.GetComponent<CircleCollider2D>().GetContacts(point2Ds);
            for (int i = 0; i < pointCount; i++)
            {
                if (point2Ds[i].collider.gameObject.layer != 8)
                {
                    grounded = true;
                    break;
                }
            }
        }

        SkullController.CanSwim = touching && grounded;
    }
}
