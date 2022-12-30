using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UIElements;

public class IndependentRotationChild : MonoBehaviour
{
    private Quaternion lastParentRotation;

    void Start()
    {
        lastParentRotation = transform.parent.parent.localRotation;
    }
    void Update()
    {
        transform.localRotation = Quaternion.Inverse(transform.parent.localRotation) *
        lastParentRotation *
        transform.localRotation;
        lastParentRotation = transform.parent.parent.localRotation;
    }
}