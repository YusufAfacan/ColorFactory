using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CommandLines : MonoBehaviour
{
    public List<CommandLine> commandLines = new();
    public int totalNumberOfCommandTiles;
    public int currentCommandTile;
    public TileManager tileManager;
    public int totalNumberOfMoves;
    public List<PistonArm> pistonArms;
    public ChallengeManager challengeManager;
    public Transform commandIndicator;

    private CancellationTokenSource _cancellationTokenSource = new();

    public GameObject panel;
    public List<Ball> balls = new();

    public void CheckBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            for(int j = 0; j < tileManager.tiles.Count; j++)
            {
                if (balls[i].transform.position == tileManager.tiles[j].transform.position)
                {
                    if (tileManager.tiles[j].occupyingTarget != null)
                    {
                        if ((int)balls[i].ballType == (int)tileManager.tiles[j].occupyingTarget.targetType)
                        {
                            Debug.Log("Success");
                            

                        }
                        else
                        {
                            Debug.Log("fail");
                            
                        }
                    }
                    else
                    {
                        Debug.Log("fail");
                    }
                }
                
            }
        }

        challengeManager.CheckChallenges();

    }

    private void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
        challengeManager = FindObjectOfType<ChallengeManager>();

        for (int i = 0; i < transform.childCount; i++)
        {
            commandLines.Add(transform.GetChild(i).GetComponent<CommandLine>());
        }

        balls.AddRange(FindObjectsOfType<Ball>());

    }

    //private void Start()
    //{
    //    for (int i = 0; i < commandLines.Count; i++)
    //    {
    //        totalNumberOfCommandTiles += commandLines[i].commands.Count;
    //    }


    //}

    public async void StartCommanding()
    {
        for (int i = 0; i < pistonArms.Count; i++)
        {
            if (pistonArms[i].isDeclared)
            {
                totalNumberOfCommandTiles += commandLines[i].commands.Count;
            }

        }



        for (int i = 0; i < commandLines[1].commands.Count; i++)
        {
            for (int j = 0; j < commandLines.Count; j++)
            {
                commandLines[j].ExecuteNextCommand();

                if (commandLines[j].commands[i].declaredCommand == CommandTile.DeclaredCommand.None)
                {
                    commandIndicator.DOMove(commandLines[j].commands[i].transform.position, 0.1f);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.2f), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                }
                else
                {
                    commandIndicator.DOMove(commandLines[j].commands[i].transform.position, 0.55f);
                    await UniTask.Delay(TimeSpan.FromSeconds(1.1f), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                }
            }
        }
    }

    
}
