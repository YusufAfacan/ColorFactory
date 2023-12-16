using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CommandLine : MonoBehaviour
{
    public List<CommandTile> commands = new();
    public PistonArm commandingPistonArm;
    public int nextCommandIndex;
    public CommandLines commandLines;
    public TileManager tileManager;

    private void Awake()
    {
        commandLines = FindObjectOfType<CommandLines>();
        tileManager = FindObjectOfType<TileManager>();

        for (int i = 0; i < transform.childCount; i++)
        {
            commands.Add(transform.GetChild(i).GetComponent<CommandTile>());
        }
    }

    public void ExecuteNextCommand()
    {
        if (commands[nextCommandIndex] == null) { return; }
        if (commandingPistonArm == null) { return; }
        if (!commandingPistonArm.isDeclared) { return; }

        switch (commands[nextCommandIndex].declaredCommand)
        {
            case CommandTile.DeclaredCommand.None:
                commandingPistonArm.None();
                break;
            case CommandTile.DeclaredCommand.Extract:
                commandingPistonArm.Extract();
                break;
            case CommandTile.DeclaredCommand.Rectract:
                commandingPistonArm.Rectract();
                break;
            case CommandTile.DeclaredCommand.Clockwise:
                commandingPistonArm.Clockwise();
                break;
            case CommandTile.DeclaredCommand.CounterClockwise:
                commandingPistonArm.CounterClockwise();
                break;
            case CommandTile.DeclaredCommand.Grab:
                commandingPistonArm.Grab();
                break;
            case CommandTile.DeclaredCommand.Release:
                commandingPistonArm.Release();
                break;
        }

        nextCommandIndex++;

        if (nextCommandIndex >= commands.Count)
        {
            nextCommandIndex = 0;
        }

        if (commandLines.currentCommandTile == commandLines.totalNumberOfCommandTiles)
        {
            commandLines.CheckBalls();
        }
    }

}
