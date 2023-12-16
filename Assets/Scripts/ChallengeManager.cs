using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChallengeManager : MonoBehaviour
{
    public List<Challenge> challenges;
    public CommandLines commandLines;

    private void Awake()
    {
        commandLines = FindObjectOfType<CommandLines>();
    }


    public void CheckChallenges()
    {
        for (int i = 0; i < challenges.Count; i++)
        {
            if (challenges[i].challangeType == Challenge.ChallangeType.LimitedMove)
            {
                if (commandLines.totalNumberOfMoves <= challenges[i].moveAmount)
                {
                    Debug.Log("ChallengeSuccess");
                }
                else
                {
                    Debug.Log("ChallengeFail");
                }
            }
            

        }
    }
}
