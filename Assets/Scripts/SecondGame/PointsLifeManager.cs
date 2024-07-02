using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointsLifeManager : MonoBehaviour
{
    // Good and Bad scores, with the max spawners and max posible good scores.
    public int goodScores = 0, badScores = 0;
    public int maxGoodScores = 0, maxSpawners;
    // Actual score of the player.
    private int actualScore = 0;
    // Actual life of the player.
    private int life = 3;
    // Controller for the 5 spawners.
    [SerializeField]
    private SecondGameSpawner SpawnManager;
    // Text with the scores of the player.
    [SerializeField]
    private TextMeshPro PhaseText;

    // Getter for the score.
    public int GetScore() { return actualScore; }

    // Clears the scores after a phase.
    public void ClearPhase() { goodScores = 0; badScores = 0; maxGoodScores = 0; }

    // Updates the scores of the player at the end of a phase.
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
        // Calls the end of the game when losing.
        if (life == 0 && badScores > 0)
        {
            life--;
            SpawnManager.EndGame(actualScore);
            PhaseText.text = $"Obtuviste {actualScore} en total. Felicidades.";
            return;
        }
        UpdatePhaseText();
    }

    // Updates the scores of the player.
    public void UpdatePhaseText()
    {
        PhaseText.text = "";
        PhaseText.text += goodScores == 1 ? $"Obtuviste {goodScores} comida buena. " : $"Obtuviste {goodScores} comidas buenas. ";
        PhaseText.text += badScores == 1 ? $"Obtuviste {badScores} comida mala. " : $"Obtuviste {badScores} comidas malas. ";
        if (life >= 0)
        {
            PhaseText.text += life == 1 ? $"Te queda {life} vida.\n" : $"Te quedan {life} vidas.\n";
            PhaseText.text += $"Tu puntaje actual es: {actualScore}";
        }
        else
        {
            PhaseText.text += $"Te quedaste sin vidas y tu puntaje final fue de {actualScore}.\n ¡Bien Jugado!";
        }
    }

    // Checks if all the good scores were obtained and no bad scores were obtained.
    public bool IsRoundPerfect()
    {
        if (badScores == 0 && goodScores == maxGoodScores)
        {
            return true;
        }
        return false;
    }
}
