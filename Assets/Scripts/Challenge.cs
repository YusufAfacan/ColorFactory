using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Challenge
{
    public enum ChallangeType {CollectStar, LimitedMove, SingleArmUse, DontUseRBG, DontUseYingYang, DontUseMerger }
    public ChallangeType challangeType;
    public bool isCompleted;
    public int moveAmount;
}
