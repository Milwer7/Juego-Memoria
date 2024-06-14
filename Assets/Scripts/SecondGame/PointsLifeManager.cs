using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsLifeManager : MonoBehaviour
{
    public int goodScores = 0, badScores = 0;
    public int maxGoodScores, maxSpawners;

    private int actualScore = 0;

    private int life = 3;

    [SerializeField]
    private SecondGameSpawner SpawnManager;

    public int GetScore() { return actualScore; }

    public void clearPhase() { goodScores = 0; badScores = 0; maxGoodScores = 0; }

    public void UpdatePhase()
    {
        if (badScores == 0 && goodScores > 0)
        {
            // Update the score on screen, scaling with the difficulty 
            actualScore += goodScores * maxSpawners;
        }
        if (badScores > 0 && life > 0)
        {
            life--;
        }
        if (life == 0 && badScores > 0)
        {
            SpawnManager.EndGame(actualScore);
        }
        clearPhase();
    }

    public bool IsRoundPerfect()
    {
        if (badScores == 0 && goodScores == maxGoodScores)
        {
            return true;
        }
        return false;
    }
}
