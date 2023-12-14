using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.None)
        {
            Debug.Log("None");
        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Rectract)
        {
            Debug.Log("Rectract");
            if (commandingPistonArm.isRectracted) return;
            commandingPistonArm.arm.DOScaleY(3, 1);
            commandingPistonArm.gripper.DOLocalMoveY(3.5f, 1);
            commandingPistonArm.grabbingPoint.DOLocalMoveY(4, 1).OnComplete(() => { CheckConverter(); });
            commandLines.totalNumberOfMoves++;

        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Extract)
        {
            Debug.Log("Extract");
            if (commandingPistonArm.isExtracted) return;
            commandingPistonArm.arm.DOScaleY(7, 1);
            commandingPistonArm.gripper.DOLocalMoveY(7.5f, 1);
            commandingPistonArm.grabbingPoint.DOLocalMoveY(8, 1).OnComplete(() => { CheckConverter(); }); ;
            commandLines.totalNumberOfMoves++;
        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Clockwise)
        {
            Debug.Log("Clockwise");
            Vector3 armRot = commandingPistonArm.transform.rotation.eulerAngles;
            Vector3 rot = new Vector3(0, 0, 90);
            commandingPistonArm.transform.DORotate(armRot + rot, 1, RotateMode.Fast).OnComplete(() => { CheckConverter(); }); ;
            commandLines.totalNumberOfMoves++;

        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.CounterClockwise)
        {
            Debug.Log("CounterClockwise");
            Vector3 armRot = commandingPistonArm.transform.rotation.eulerAngles;
            Vector3 rot = new Vector3(0, 0, -90);
            commandingPistonArm.transform.DORotate(armRot + rot, 1, RotateMode.Fast).OnComplete(() => { CheckConverter(); }); ;
            commandLines.totalNumberOfMoves++;


        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Grab)
        {
            Debug.Log("Grab");

            commandingPistonArm.gripper.DOScaleX(0.5f, 1);

            for (int i = 0; i < commandLines.balls.Count; i++)
            {
                if (commandingPistonArm.grabbingPoint.transform.position == commandLines.balls[i].transform.position)
                {
                    Ball ball = commandLines.balls[i];
                    commandingPistonArm.grabbedBall = ball;

                    if (ball.isGrabbed)
                    {
                        Debug.Log("BALL IS GRABBED BY ANOTHER ARM");

                        nextCommandIndex++;

                        if (nextCommandIndex >= 9) nextCommandIndex = 0;

                        return;
                    }
                    else
                    {
                        ball.isGrabbed = true;
                        ball.transform.SetParent(commandingPistonArm.grabbingPoint.transform);
                    }

                }

            }

        }
        if (commands[nextCommandIndex].declaredCommand == CommandTile.DeclaredCommand.Release)
        {
            Debug.Log("Release");
            if (commandingPistonArm.isReleased) return;

            if (commandingPistonArm.grabbedBall != null)
            {
                commandingPistonArm.grabbedBall.transform.SetParent(null);
                commandingPistonArm.grabbedBall.isGrabbed = false;
            }

            commandingPistonArm.gripper.DOScaleX(0.3f, 1);


        }

        nextCommandIndex++;
        commandLines.currentCommandTile++;

        if (nextCommandIndex >= commands.Count)
        {
            nextCommandIndex = 0;
        }

        if (commandLines.currentCommandTile == commandLines.totalNumberOfCommandTiles)
        {
            commandLines.CheckBalls();
        }
    }

    private void CheckConverter()
    {
        if (commandingPistonArm.grabbedBall != null)
        {
            for (int i = 0; i < tileManager.tiles.Count; i++)
            {
                if (commandingPistonArm.grabbedBall.transform.position ==
                    tileManager.tiles[i].transform.position)
                {
                    if (tileManager.tiles[i].isOccupied)
                    {
                        if (tileManager.tiles[i].occupyingConverter != null)
                        {
                            if (tileManager.tiles[i].occupyingConverter.converterType == Converter.ConverterType.YingYang)
                            {
                                if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Black)
                                {
                                    commandingPistonArm.grabbedBall.ballType = Ball.BallType.White;
                                    commandingPistonArm.grabbedBall.spriteRenderer.sprite = commandingPistonArm.grabbedBall.whiteBall;

                                }
                                else if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.White)
                                {
                                    commandingPistonArm.grabbedBall.ballType = Ball.BallType.Black;
                                    commandingPistonArm.grabbedBall.spriteRenderer.sprite = commandingPistonArm.grabbedBall.blackBall;
                                }
                            }
                            if (tileManager.tiles[i].occupyingConverter.converterType == Converter.ConverterType.RGB)
                            {
                                if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Red)
                                {
                                    commandingPistonArm.grabbedBall.ballType = Ball.BallType.Blue;
                                    commandingPistonArm.grabbedBall.spriteRenderer.sprite = commandingPistonArm.grabbedBall.blueBall;
                                }
                                else if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Blue)
                                {
                                    commandingPistonArm.grabbedBall.ballType = Ball.BallType.Green;
                                    commandingPistonArm.grabbedBall.spriteRenderer.sprite = commandingPistonArm.grabbedBall.greenBall;
                                }
                                else if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Green)
                                {
                                    commandingPistonArm.grabbedBall.ballType = Ball.BallType.Red;
                                    commandingPistonArm.grabbedBall.spriteRenderer.sprite = commandingPistonArm.grabbedBall.redBall;
                                }
                            }

                            if (tileManager.tiles[i].occupyingConverter.converterType == Converter.ConverterType.ColorMerger)
                            {
                                if (!tileManager.tiles[i].occupyingConverter.isOccupyingBall)
                                {
                                    tileManager.tiles[i].occupyingConverter.occupyingBall = commandingPistonArm.grabbedBall;
                                    tileManager.tiles[i].occupyingConverter.isOccupyingBall = true;
                                }
                                else if (tileManager.tiles[i].occupyingConverter.isOccupyingBall)
                                {
                                    Ball occupyingBall = tileManager.tiles[i].occupyingConverter.occupyingBall;

                                    if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Red)
                                    {
                                        if (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType == Ball.BallType.Green)
                                        {
                                            occupyingBall.spriteRenderer.sprite = occupyingBall.yellowBall;
                                            occupyingBall.ballType = Ball.BallType.Yellow;

                                            Ball grabbedBall = commandingPistonArm.grabbedBall;
                                            commandingPistonArm.grabbedBall = null;
                                            grabbedBall.gameObject.SetActive(false);
                                            
                                        }
                                        else if (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType == Ball.BallType.Blue)
                                        {
                                            occupyingBall.spriteRenderer.sprite = occupyingBall.magentaBall;
                                            occupyingBall.ballType = Ball.BallType.Magenta;

                                            Ball grabbedBall = commandingPistonArm.grabbedBall;
                                            commandingPistonArm.grabbedBall = null;
                                            grabbedBall.gameObject.SetActive(false);
                                        }
                                    }
                                    else if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Green)
                                    {
                                        if (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType == Ball.BallType.Red)
                                        {
                                            occupyingBall.spriteRenderer.sprite = occupyingBall.yellowBall;
                                            occupyingBall.ballType = Ball.BallType.Yellow;

                                            Ball grabbedBall = commandingPistonArm.grabbedBall;
                                            commandingPistonArm.grabbedBall = null;
                                            grabbedBall.gameObject.SetActive(false);
                                        }
                                        else if (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType == Ball.BallType.Blue)
                                        {
                                            occupyingBall.spriteRenderer.sprite = occupyingBall.cyanBall;
                                            occupyingBall.ballType = Ball.BallType.Cyan;

                                            Ball grabbedBall = commandingPistonArm.grabbedBall;
                                            commandingPistonArm.grabbedBall = null;
                                            grabbedBall.gameObject.SetActive(false);
                                        }
                                    }
                                    else if (commandingPistonArm.grabbedBall.ballType == Ball.BallType.Blue)
                                    {
                                        if (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType == Ball.BallType.Red)
                                        {
                                            occupyingBall.spriteRenderer.sprite = occupyingBall.magentaBall;
                                            occupyingBall.ballType = Ball.BallType.Magenta;

                                            Ball grabbedBall = commandingPistonArm.grabbedBall;
                                            commandingPistonArm.grabbedBall = null;
                                            grabbedBall.gameObject.SetActive(false);
                                        }
                                        else if (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType == Ball.BallType.Green)
                                        {
                                            occupyingBall.spriteRenderer.sprite = occupyingBall.cyanBall;
                                            occupyingBall.ballType = Ball.BallType.Cyan;

                                            Ball grabbedBall = commandingPistonArm.grabbedBall;
                                            commandingPistonArm.grabbedBall = null;
                                            grabbedBall.gameObject.SetActive(false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
