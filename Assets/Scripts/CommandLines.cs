using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CommandLines : MonoBehaviour
{
    public List<CommandLine> commandLines = new();
    public List<CommandLine> usedCommandLines = new();
    public List<PistonArm> pistonArms = new();
    public List<PistonArm> usedPistonArms = new();
    public List<Ball> balls = new();
    public int currentCommandTile;
    public int totalNumberOfCommandTiles;
    public int totalNumberOfMoves;

    public TileManager tileManager;
    public ChallengeManager challengeManager;
    public Transform commandIndicator;
    private CancellationTokenSource _cancellationTokenSource = new();
    public GameObject panel;
    public GameManager gameManager;
    
    
    

    private void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
        challengeManager = FindObjectOfType<ChallengeManager>();
        gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
                commandLines.Add(transform.GetChild(i).GetComponent<CommandLine>());
        }

        balls.AddRange(FindObjectsOfType<Ball>(false));
        pistonArms.AddRange(FindObjectsOfType<PistonArm>(false));

        //List<CommandTile> commandTiles = new List<CommandTile>();
        //commandTiles.AddRange(FindObjectsOfType<CommandTile>());
        //totalNumberOfCommandTiles = commandTiles.Count;
    }
    public async void StartCommanding()
    {
        balls.Clear();
        balls.AddRange(FindObjectsOfType<Ball>(false));

        usedCommandLines.Clear();
        currentCommandTile = 0;
        totalNumberOfCommandTiles = 0;
        totalNumberOfMoves = 0;




        for (int i = 0; i < pistonArms.Count; i++)
        {
            if (pistonArms[i].isDeclared)
            {
                usedPistonArms.Add(pistonArms[i]);
            }
        }

        for (int i = 0; i < commandLines.Count; i++)
        {
            if (commandLines[i].commandingPistonArm.isDeclared)
            {
                usedCommandLines.Add(commandLines[i]);
                totalNumberOfCommandTiles += commandLines[i].commands.Count;
            }
        }

        if (usedCommandLines.Count > 0)
        {
            commandIndicator.transform.position = usedCommandLines[0].commands[0].transform.position;

            for (int i = 0; i < usedCommandLines[0].commands.Count; i++)
            {
                for (int j = 0; j < usedCommandLines.Count; j++)
                {
                    currentCommandTile++;
                    usedCommandLines[j].ExecuteNextCommand();

                    if (usedCommandLines[j].commands[i].declaredCommand == CommandTile.DeclaredCommand.None)
                    {
                        commandIndicator.DOMove(usedCommandLines[j].commands[i].transform.position, 0.1f);
                        await UniTask.Delay(TimeSpan.FromSeconds(0.2f), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    }
                    else
                    {
                        commandIndicator.DOMove(usedCommandLines[j].commands[i].transform.position, 0.55f);
                        await UniTask.Delay(TimeSpan.FromSeconds(1.1f), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    }
                }
            }

        }
    }
    public void CheckBalls()
    {
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
                            gameManager.TryAgain();
                            
                            
                            return;

                        }
                        else
                        {
                            Debug.Log("Success");
                            gameManager.LevelCleared();
                            challengeManager.CheckChallenges();

                        }
                    }
                    else
                    {
                        Debug.Log("fail");
                        gameManager.TryAgain();

                    }
                }
                

            }
        }
    }
}
