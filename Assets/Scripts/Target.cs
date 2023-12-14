using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isOccupied;
    public enum TargetType { Red, Green, Blue, Black, White, Yellow, Magenta, Cyan }
    public TargetType targetType;
}
