using System;
using UnityEngine;
using UnityEngine.XR.Hands;

[Serializable]
public struct JointToHumanBodyBonesReference
{
    [SerializeField]
    [Tooltip("The XR Hand Joint Identifier that will drive the Transform.")]
    XRHandJointID m_XRHandJointID;
    
    [SerializeField]
    [Tooltip("The HumanBodyBones that will be driven by the specified XR Joint.")]
    HumanBodyBones m_HumanBodyBoneTransform;
    
    public XRHandJointID xrHandJointID
    {
        get => m_XRHandJointID;
        set => m_XRHandJointID = value;
    }
    
    public HumanBodyBones humanBodyBoneTransform
    {
        get => m_HumanBodyBoneTransform;
        set => m_HumanBodyBoneTransform = value;
    }
}