using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
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
    public ChallengeManager challengeManager;
    public Transform commandIndicator;
    private CancellationTokenSource _cancellationTokenSource = new();
    public GameObject panel;
    public List<Ball> balls = new();

    private void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
        challengeManager = FindObjectOfType<ChallengeManager>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
                commandLines.Add(transform.GetChild(i).GetComponent<CommandLine>());
        }

        balls.AddRange(FindObjectsOfType<Ball>(false));

        List<CommandTile> commandTiles = new List<CommandTile>();
        commandTiles.AddRange(FindObjectsOfType<CommandTile>());
        totalNumberOfCommandTiles = commandTiles.Count;
    }
    public async void StartCommanding()
    {
        for (int i = 0; i < commandLines[0].commands.Count; i++)
        {
            for (int j = 0; j < commandLines.Count; j++)
            {
                currentCommandTile++;
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
    public void CheckBalls()
    {
        balls.Clear();
        balls.AddRange(FindObjectsOfType<Ball>(false));


        for (int i = 0; i < balls.Count; i++)
        {
            for (int j = 0; j < tileManager.tiles.Count; j++)
            {
                if (balls[i].transform.position == tileManager.tiles[j].transform.position)
                {
                    if (tileManager.tiles[j].occupyingTarget != null)
                    {
                        if ((int)balls[i].ballType != (int)tileManager.tiles[j].occupyingTarget.targetType)
                        {
                            Debug.Log("fail");
                            return;

                        }
                        else
                        {
                            Debug.Log("Success");
                            challengeManager.CheckChallenges();

                        }
                    }

                }

            }
        }
    }
}
