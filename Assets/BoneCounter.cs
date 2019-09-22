using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoneCounter : MonoBehaviour
{
    public void SetBoneCount(int count)
    {
        GetComponent<TextMeshProUGUI>().text = count + " BONES";
    }
}
