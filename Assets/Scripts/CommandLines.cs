using Cysharp.Threading.Tasks;
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
                    if ((int)balls[i].ballType != (int)tileManager.tiles[j].occupyingTarget.targetType)
                    {
                        Debug.Log("Fail");
                        return;
                    }
                    else
                    {
                        Debug.Log("Success");
                    }
                }
            }
        }
    }

    private void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();

        for (int i = 0; i < transform.childCount; i++)
        {
            commandLines.Add(transform.GetChild(i).GetComponent<CommandLine>());
        }

        balls.AddRange(FindObjectsOfType<Ball>());

    }

    private void Start()
    {
        for (int i = 0; i < commandLines.Count; i++)
        {
            totalNumberOfCommandTiles += commandLines[i].commands.Count;
        }


    }

    public async void StartCommanding()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < commandLines.Count; j++)

            commandLines[j].ExecuteNextCommand();
            await UniTask.Delay(TimeSpan.FromSeconds(1.1f), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
        }
    }

    
}
