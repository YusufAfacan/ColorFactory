using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CommandLines : MonoBehaviour
{
    public List<CommandLine> commandLines = new();

    private CancellationTokenSource _cancellationTokenSource = new();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            commandLines.Add(transform.GetChild(i).GetComponent<CommandLine>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public async void StartCommanding()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < commandLines.Count; j++)

            commandLines[j].ExecuteNextCommand();
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
        }
    }
}
