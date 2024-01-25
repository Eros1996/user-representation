using System;
using UnityEngine;

public enum ToRecord
{
    Keep,
    Ignore
}

[Serializable]
public struct BonesToRecord
{
    public string name;
    
    [SerializeField]
    [Tooltip("The XR Hand Joint Identifier that will drive the Transform.")]
    ToRecord m_ToRecord;
    
    [SerializeField]
    [Tooltip("The HumanBodyBones that will be driven by the specified XR Joint.")]
    HumanBodyBones m_HumanBodyBone;
    
    public ToRecord toRecord
    {
        get => m_ToRecord;
        set => m_ToRecord = value;
    }
    
    public HumanBodyBones humanBodyBone
    {
        get => m_HumanBodyBone;
        set => m_HumanBodyBone = value;
    }
}