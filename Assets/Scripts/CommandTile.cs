using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTile : MonoBehaviour
{
    public enum DeclaredCommand {None, Extract, Rectract, Clockwise, CounterClockwise, Grab, Release }
    public DeclaredCommand declaredCommand;
    public bool isOccupied;
}
