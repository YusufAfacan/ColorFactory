using UnityEngine;


public class ChallengeManager : MonoBehaviour
{
    
    private CommandLines commandLines;
    public int moveAmount;
    public int armsUsed;
    public bool isStarCollected;
    public bool levelHaveStar;
    private void Awake()
    {
        commandLines = FindObjectOfType<CommandLines>();
        
    }

    public void CheckChallenges()
    {
        if (moveAmount > 0 && commandLines.totalNumberOfMoves <= moveAmount)
        {
            Debug.Log("LimitedMoveChallengeSuccess");
        }
        else if(moveAmount > 0 && commandLines.totalNumberOfMoves > moveAmount)
        {
            Debug.Log("LimitedMoveChallengeFail");
        }

        if (armsUsed > 0 && commandLines.usedPistonArms.Count <= armsUsed)
        {
            Debug.Log("LimitedArmsChallengeSuccess");
        }
        else if(armsUsed > 0 && commandLines.usedPistonArms.Count > armsUsed)
        {
            Debug.Log("LimitedArmsChallengeFail");
        }
        
        if(levelHaveStar && isStarCollected)
        {
            Debug.Log("CollectStarChallengeSuccess");
        }
        else if(levelHaveStar && !isStarCollected)
        {
            Debug.Log("CollectStarChallengeFail");
        }

    }
}
