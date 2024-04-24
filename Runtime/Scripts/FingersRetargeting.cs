using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class FingersRetargeting : MonoBehaviour
{
    public bool isRightHand;
    public bool customRotation;
    
    public Vector3 proximalRotationOffset = new Vector3(90, 90, 90);
    public Vector3 intermediateRotationOffset = new Vector3(90, 90, 90);
    public Vector3 distalRotationOffset = new Vector3(90, 90, 90);

    public Vector3 metacarpalThumbRotationOffset; 
    public Vector3 proximalThumbRotationOffset; 
    public Vector3 distalThumbRotationOffset; 
    
    public List<JointToHumanBodyBonesReference> jointToHumanBodyBones;
    
    private Vector3 thumbRotationOffsetR = new Vector3(180, 90, 90);
    private Vector3 thumbRotationOffsetL = new Vector3(0, 90, 90);
    private List<Transform> m_Fingers;
    private XRHandSubsystem m_HandSubsystem;
    private Animator m_Animator;
    private bool m_IsHandTrackingStarted;
    private GameObject m_XRRig;
    private XRInputModalityManager m_InputModalityManager;
    private XRHandSkeletonDriver m_XRHandSkeletonDriver;
    private float m_HandScale;
    private bool m_IsScaleFix;
    private Vector3 m_ThumbRotationOffset;
    private XRHand m_XrHand;
    private List<JointToTransformReference> m_JointTransformReferences;
    private VRIK vrik;
    
    public void IsScaleFix(bool isScaleFix)
    {
        m_IsScaleFix = isScaleFix;
    }
    
    private void Start()
    {
        m_XRRig = GameObject.Find("XR Origin (XR Rig)");
        m_InputModalityManager = m_XRRig.GetComponent<XRInputModalityManager>();
        m_XRHandSkeletonDriver = isRightHand ? m_InputModalityManager.rightHand.GetComponentInChildren<XRHandSkeletonDriver>() : m_InputModalityManager.leftHand.GetComponentInChildren<XRHandSkeletonDriver>();
        m_JointTransformReferences = m_XRHandSkeletonDriver.jointTransformReferences;
        m_Animator = GetComponentInParent<Animator>();
        
        //StartCoroutine(WaitVRIKInit());
    }
    
    private void OnEnable()
    {
        // if (m_HandSubsystem is null)
        //     LoadSubsystem();
        // else
        //     vrik.solver.OnPostUpdate += UpdateSkeletonFingers;
        //     //m_HandSubsystem.updatedHands += OnUpdatedHands;
        
        StartCoroutine(WaitVRIKInit());
        this.transform.localScale = Vector3.one;
        m_IsScaleFix = false;
    }

    private void OnDisable()
    {
        if (vrik is null) return;
            
        this.transform.localScale = Vector3.one;
        vrik.solver.OnPostUpdate -= UpdateSkeletonFingers;
        //m_HandSubsystem.updatedHands -= OnUpdatedHands;
    }

    private void SetHandScale(XRHand hand)
    {
        this.transform.localScale = Vector3.one;
        
        var avtWrist = this.transform;
        var fingerWristData = hand.GetJoint(XRHandJointID.Wrist);
        if (!fingerWristData.TryGetPose(out var xrWristJointPose)) return;
        
        var fingerMiddleData = hand.GetJoint(XRHandJointID.MiddleDistal);
        if(!fingerMiddleData.TryGetPose(out var xrMiddleJointPose)) return; 
        var avtMiddleDistalTransform = isRightHand ? m_Animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal) : m_Animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);;
        if (avtMiddleDistalTransform is null) return;

        var avtScale = Mathf.Abs(avtWrist.position.y - avtMiddleDistalTransform.position.y);//Vector3.Distance(avtWrist.position, avtMiddleDistalTransform.position);
        var xrScale = Mathf.Abs(xrWristJointPose.position.y - xrMiddleJointPose.position.y);//Vector3.Distance(xrWristJointPose.position, xrMiddleJointPose.position);
        
        m_HandScale = xrScale / avtScale;
        this.transform.localScale = Vector3.one * m_HandScale;
    }
    
    private void LoadSubsystem()
    {
        var handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);
        
        foreach (var handSubsystem in handSubsystems)
        {
            if (!handSubsystem.running) continue;
            m_HandSubsystem = handSubsystem;
            break;
        }

        if (m_HandSubsystem == null) return;
        m_XrHand = isRightHand ? m_HandSubsystem.rightHand : m_HandSubsystem.leftHand;
        m_ThumbRotationOffset = isRightHand ? thumbRotationOffsetR : thumbRotationOffsetL;
        //m_HandSubsystem.updatedHands += OnUpdatedHands;
        //vrik.solver.OnPostUpdate += UpdateSkeletonFingers;
    }

    private void UpdateSkeletonFingers()
    {
        if (!m_IsScaleFix)
        {
            SetHandScale(m_XrHand);
            m_IsScaleFix = true;
        }
        
        for (var i = XRHandJointID.ThumbMetacarpal.ToIndex(); i < XRHandJointID.EndMarker.ToIndex(); i++)
        {
            var fingerHumanBodyBones = jointToHumanBodyBones[i].humanBodyBoneTransform;
            if (fingerHumanBodyBones == HumanBodyBones.LastBone)
                continue;
            var avtFingerTransform = m_Animator.GetBoneTransform(fingerHumanBodyBones);
            var xrFinger = jointToHumanBodyBones[i].xrHandJointID;

            foreach (var jointToTransform in m_JointTransformReferences)
            {
                if (jointToTransform.xrHandJointID == xrFinger)
                {
                    var xrSkeletonJointTransform = jointToTransform.jointTransform;
                    //avtFingerTransform.position = xrSkeletonJointTransform.position;

                    switch (xrFinger)
                    {
                        case XRHandJointID.IndexProximal or XRHandJointID.LittleProximal or XRHandJointID.MiddleProximal or XRHandJointID.RingProximal:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation * Quaternion.Euler(proximalRotationOffset);
                            break;
                        case XRHandJointID.IndexIntermediate or XRHandJointID.LittleIntermediate or XRHandJointID.MiddleIntermediate or XRHandJointID.RingIntermediate:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation * Quaternion.Euler(intermediateRotationOffset);
                            break;
                        case XRHandJointID.IndexDistal or XRHandJointID.LittleDistal or XRHandJointID.MiddleDistal or XRHandJointID.RingDistal:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation * Quaternion.Euler(distalRotationOffset);
                            break;
                        case XRHandJointID.ThumbMetacarpal:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation * Quaternion.Euler(customRotation ? metacarpalThumbRotationOffset : m_ThumbRotationOffset);
                            break;
                        case XRHandJointID.ThumbProximal:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation * Quaternion.Euler(customRotation ?  proximalThumbRotationOffset : m_ThumbRotationOffset);
                            break;
                        case XRHandJointID.ThumbDistal:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation * Quaternion.Euler(customRotation ? distalThumbRotationOffset : m_ThumbRotationOffset);
                            break;
                        default:
                            avtFingerTransform.rotation = xrSkeletonJointTransform.rotation;
                            break;
                    }
                }
            }
        }
    }

    [ContextMenu("Setup Joints To HumanBodyBones")]
    public void SetupJointsToHumanBodyBones()
    {
        jointToHumanBodyBones = new List<JointToHumanBodyBonesReference>();
        JointToHumanBodyBonesReference joint2HumanBoneRef;
        
        for (var i = XRHandJointID.BeginMarker.ToIndex(); i < XRHandJointID.EndMarker.ToIndex(); i++)
        {
            joint2HumanBoneRef = new JointToHumanBodyBonesReference
            {
                xrHandJointID = XRHandJointIDUtility.FromIndex(i),
                humanBodyBoneTransform = HumanBodyBones.LastBone
            };
            jointToHumanBodyBones.Add(joint2HumanBoneRef);
        }
        
        if (isRightHand)
        {
            joint2HumanBoneRef = new JointToHumanBodyBonesReference
            {
                xrHandJointID = XRHandJointID.Wrist,
                humanBodyBoneTransform = HumanBodyBones.RightHand
            };
            jointToHumanBodyBones[0] = joint2HumanBoneRef;
            
            FindMatchingJoint((int)HumanBodyBones.RightThumbProximal, XRHandJointID.ThumbMetacarpal.ToIndex(), XRHandJointID.ThumbDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.RightIndexProximal, XRHandJointID.IndexProximal.ToIndex(), XRHandJointID.IndexDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.RightMiddleProximal, XRHandJointID.MiddleProximal.ToIndex(), XRHandJointID.MiddleDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.RightRingProximal, XRHandJointID.RingProximal.ToIndex(), XRHandJointID.RingDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.RightLittleProximal, XRHandJointID.LittleProximal.ToIndex(), XRHandJointID.LittleDistal.ToIndex());
        }
        else
        {
            joint2HumanBoneRef = new JointToHumanBodyBonesReference
            {
                xrHandJointID = XRHandJointID.Wrist,
                humanBodyBoneTransform = HumanBodyBones.LeftHand
            };
            jointToHumanBodyBones[0] = joint2HumanBoneRef;
            
            FindMatchingJoint((int)HumanBodyBones.LeftThumbProximal, XRHandJointID.ThumbMetacarpal.ToIndex(), XRHandJointID.ThumbDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.LeftIndexProximal, XRHandJointID.IndexProximal.ToIndex(), XRHandJointID.IndexDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.LeftMiddleProximal, XRHandJointID.MiddleProximal.ToIndex(), XRHandJointID.MiddleDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.LeftRingProximal, XRHandJointID.RingProximal.ToIndex(), XRHandJointID.RingDistal.ToIndex());
            FindMatchingJoint((int)HumanBodyBones.LeftLittleProximal, XRHandJointID.LittleProximal.ToIndex(), XRHandJointID.LittleDistal.ToIndex());
        }
    }
    
    private void FindMatchingJoint(int startIndx, int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            var joint2HumanBoneRef = new JointToHumanBodyBonesReference
            {
                xrHandJointID = XRHandJointIDUtility.FromIndex(i),
                humanBodyBoneTransform = (HumanBodyBones)startIndx 
            };
            startIndx++;
            jointToHumanBodyBones[i] = joint2HumanBoneRef;
        }
    }
    
    private IEnumerator WaitVRIKInit()
    {
        while (vrik == null)
        {
            yield return null;
            vrik = this.GetComponentInParent<VRIK>();
        }
        
        if (m_HandSubsystem is null) 
            LoadSubsystem();
        vrik.solver.OnPostUpdate += UpdateSkeletonFingers;
    }
    
    // void OnUpdatedHands(XRHandSubsystem subsystem,XRHandSubsystem.UpdateSuccessFlags updateSuccessFlags,XRHandSubsystem.UpdateType updateType)
    // {
    //     switch (updateType)
    //     {
    //         case XRHandSubsystem.UpdateType.Dynamic:
    //             // Update game logic that uses hand data
    //             break;
    //         case XRHandSubsystem.UpdateType.BeforeRender:
    //             // Update visual objects that use hand data
    //             UpdateSkeletonFingers();
    //             break;
    //     }
    // }
}
