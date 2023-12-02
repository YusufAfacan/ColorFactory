using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLine : MonoBehaviour
{
    public List<CommandTile> commands = new();
    public PistonArm commandingPistonArm;
    public int nextCommandIndex;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            commands.Add(transform.GetChild(i).GetComponent<CommandTile>());
        }
    }

    public void ExecuteNextCommand()
    {
        if (commands[nextCommandIndex] == null) { return; }
        if(commandingPistonArm == null) { return; }

        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.None)
        {
            Debug.Log("None");
        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Rectract)
        {
            Debug.Log("Rectract");
            if (commandingPistonArm.isRectracted) return;
            commandingPistonArm.arm.DOScaleY(1, 1);
            commandingPistonArm.gripper.DOLocalMoveY(1.5f, 1);

        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Extract)
        {
            Debug.Log("Extract");
            if (commandingPistonArm.isExtracted) return;
            commandingPistonArm.arm.DOScaleY(3, 1);
            commandingPistonArm.gripper.DOLocalMoveY(3.5f, 1);
        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Clockwise)
        {
            Debug.Log("Clockwise");
            commandingPistonArm.transform.DORotate(new Vector3(0, 180, transform.rotation.z + 90), 1, RotateMode.Fast);
        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.CounterClockwise)
        {
            Debug.Log("CounterClockwise");
            commandingPistonArm.transform.DORotate(new Vector3(0, 180, transform.rotation.z - 90), 1, RotateMode.Fast);
        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Grab)
        {
            Debug.Log("Grab");
            if (commandingPistonArm.isGrabbed) return;
            commandingPistonArm.gripper.DOScaleX(0.5f, 1);

        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Release)
        {
            Debug.Log("Release");
            if (commandingPistonArm.isReleased) return;
            commandingPistonArm.gripper.DOScaleX(0.3f, 1);
        }

        nextCommandIndex++;

        if (nextCommandIndex >= 15) nextCommandIndex = 0;

    }

}
