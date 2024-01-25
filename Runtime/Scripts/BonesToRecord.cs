using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BonesToRecord
{
    public bool
        Hips,
        LeftUpperLeg,
        RightUpperLeg,
        LeftLowerLeg,
        RightLowerLeg,
        LeftFoot,
        RightFoot,
        Spine,
        Chest,
        Neck,
        Head,
        LeftShoulder,
        RightShoulder,
        LeftUpperArm,
        RightUpperArm,
        LeftLowerArm,
        RightLowerArm,
        LeftHand,
        RightHand,
        LeftToes,
        RightToes,
        LeftEye,
        RightEye,
        Jaw,
        LeftFingers,
        RightFingers,
        UpperChest;

    [HideInInspector]
    public static bool[] refBones = new bool[27];

    public int FromHumanBodyBones(int index)
    {
        if (index >= (int)HumanBodyBones.LeftThumbProximal && index <= (int)HumanBodyBones.LeftLittleDistal)
        {
            return 24;
        }
        
        if (index >= (int)HumanBodyBones.RightThumbProximal && index <= (int)HumanBodyBones.RightLittleDistal)
        {
            return 25;
        }

        if (index == (int)HumanBodyBones.UpperChest)
            return 26;

        return index;
    }
    
    public void SyncList()
    {
        refBones[0] = Hips;
        refBones[1] = LeftUpperLeg;
        refBones[2] = RightUpperLeg;
        refBones[3] = LeftLowerLeg;
        refBones[4] = RightLowerLeg;
        refBones[5] = LeftFoot;
        refBones[6] = RightFoot;
        refBones[7] = Spine;
        refBones[8] = Chest;
        refBones[9] = Neck;
        refBones[10] = Head;
        refBones[11] = LeftShoulder;
        refBones[12] = RightShoulder;
        refBones[13] = LeftUpperArm;
        refBones[14] = RightUpperArm;
        refBones[15] = LeftLowerArm;
        refBones[16] = RightLowerArm;
        refBones[17] = LeftHand;
        refBones[18] = RightHand;
        refBones[19] = LeftToes;
        refBones[20] = RightToes;
        refBones[21] = LeftEye;
        refBones[22] = RightEye;
        refBones[23] = Jaw;
        refBones[24] = LeftFingers;
        refBones[25] = RightFingers;
        refBones[26] = UpperChest;
    }

    public void SyncField()
    {
        Hips = refBones[0];
        LeftUpperLeg = refBones[1];
        RightUpperLeg = refBones[2]; 
        LeftLowerLeg = refBones[3]; 
        RightLowerLeg = refBones[4]; 
        LeftFoot = refBones[5]; 
        RightFoot = refBones[6]; 
        Spine = refBones[7]; 
        Chest = refBones[8]; 
        Neck = refBones[9]; 
        Head = refBones[10]; 
        LeftShoulder = refBones[11]; 
        RightShoulder = refBones[12]; 
        LeftUpperArm = refBones[13]; 
        RightUpperArm = refBones[14]; 
        LeftLowerArm = refBones[15]; 
        RightLowerArm = refBones[16]; 
        LeftHand = refBones[17]; 
        RightHand = refBones[18]; 
        LeftToes = refBones[19]; 
        RightToes = refBones[20]; 
        LeftEye = refBones[21]; 
        RightEye = refBones[22]; 
        Jaw = refBones[23]; 
        LeftFingers = refBones[24]; 
        RightFingers = refBones[25]; 
        UpperChest = refBones[26]; 
    }
    
    
}