using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SecondGameSpawner : MonoBehaviour
{
    // GameObjects on the spawnPosition of the interactables for the game.
    [SerializeField]
    private GameObject[] spawnersPosition;
    // Initial difficulty.
    private int actualDifficulty = 1, maxDifficulty = 4;
    // Initial time of every phase for the game.
    private float actualTime = 10f;
    // Active spawners, scaling with difficulty.
    private List<GameObject> activeSpawners = new List<GameObject>();
    // Points and life manager object.
    [SerializeField]
    private PointsLifeManager Manager;
    // Text with the scores of the player.
    [SerializeField]
    private TextMeshPro PhaseText;
    // Text with the initial instructions.
    [SerializeField]
    private GameObject TutorialObject, SkipPhaseObject;
    // Checking if skipPhase was triggered.
    private bool isSkip = false;

    // Reference to the running coroutine.
    Coroutine spawnCycleCoroutine;

    void Start()
    {
        // Left and Right spawns are active on base difficulty.
        activeSpawners.Add(spawnersPosition[0]);
        activeSpawners.Add(spawnersPosition[1]);
        spawnCycleCoroutine = StartCoroutine(SpawnCycle());
    }

    // Ends the spawning Coroutine.
    public void EndGame(int score)
    {
        if (spawnCycleCoroutine != null)
        {
            StopCoroutine(spawnCycleCoroutine);
            spawnCycleCoroutine = null;
            PhaseText.gameObject.SetActive(true);
        }
    }

    // Skips to the next phase of the game.
    public void SkipPhase()
    {
        SkipPhaseObject.SetActive(false);
        // Triggering difficulty increase.
        Manager.maxSpawners = actualDifficulty + 1;
        if (actualDifficulty < maxDifficulty && Manager.IsRoundPerfect())
        {
            actualDifficulty += 1;
            activeSpawners.Add(spawnersPosition[actualDifficulty]);
        }
        // Updating the Manager so it increases the points obtained.
        Manager.UpdatePhase();
        // Showing Phase Stats.
        PhaseText.gameObject.SetActive(true);
        // Deleting the objects spawned by the activeSpawners.
        foreach (GameObject spawner in activeSpawners)
        {
            spawner.GetComponent<ISpawner>().ClearChilds();
        }
        Manager.ClearPhase();
    }

    // Coroutine for spawning and clearing objects.
    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            if(TutorialObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(20f);
                TutorialObject.SetActive(false);
            }
            if(isSkip)
            {
                isSkip = false;
                PhaseText.gameObject.SetActive(true);
                yield return new WaitForSeconds(4f);
            }

            // Hiding Phase Stats.
            PhaseText.gameObject.SetActive(false);
            // Reactivating SkipPhase Area.
            SkipPhaseObject.SetActive(true);
            // Call the Spawn method on all active spawners.
            foreach (GameObject spawner in activeSpawners)
            {
                spawner.GetComponent<ISpawner>().Spawn();
            }
            // Waiting the phase time.
            yield return new WaitForSeconds(actualTime);

            // Triggering phase end.
            SkipPhase();

            // Wait some time after clearing for showing a message to the player.
            yield return new WaitForSeconds(4f);
        }
    }

    // Method to advance to the next phase immediately.
    public void AdvanceToNextPhase()
    {
        // Stopping the coroutine, skipping phase and starting it again.
        StopCoroutine(spawnCycleCoroutine);
        SkipPhase();
        isSkip = true;
        spawnCycleCoroutine = StartCoroutine(SpawnCycle());
    }
}
