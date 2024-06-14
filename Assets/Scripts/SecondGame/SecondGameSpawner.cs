using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SecondGameSpawner : MonoBehaviour
{
    // GameObjects on the spawnPosition of the interactables for the game
    [SerializeField]
    private GameObject[] spawnersPosition;
    // Initial difficulty
    private int actualDifficulty = 1, maxDifficulty = 4;
    // Initial time of every phase for the game
    private float actualTime = 10f;
    // Active spawners, scaling with difficulty
    private List<GameObject> activeSpawners;

    private PointsLifeManager Manager;

    // Reference to the running coroutine
    private Coroutine spawnCycleCoroutine;

    void Start()
    {
        // Left and Right spawns are active on base difficulty
        activeSpawners.Add(spawnersPosition[0]);
        activeSpawners.Add(spawnersPosition[1]);

        StartCoroutine(SpawnCycle());
    }

    // Ends the spawning Coroutine
    // TODO: Redirect to score window
    public void EndGame(int score)
    {
        if (spawnCycleCoroutine != null)
        {
            StopCoroutine(spawnCycleCoroutine);
            spawnCycleCoroutine = null;
        }
    }

    // Coroutine for spawning and clearing
    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            // Call the Spawn method on all active spawners
            foreach (GameObject spawner in activeSpawners)
            {
                spawner.GetComponent<ISpawner>().Spawn();
            }

            Manager.maxSpawners = actualDifficulty + 1;

            // Waiting the phase time
            yield return new WaitForSeconds(actualTime);

            // Triggering difficulty increase
            if (actualDifficulty < maxDifficulty && Manager.IsRoundPerfect())
            {
                actualDifficulty += 1;
                activeSpawners.Add(spawnersPosition[actualDifficulty]);
            }
            // Updates the Manager Phase so it increases the points obtained.
            Manager.UpdatePhase();
            
            // Deleting the objects spawned by the activeSpawners
            foreach (GameObject spawner in activeSpawners)
            {
                spawner.GetComponent<ISpawner>().ClearChilds();
            }
            Manager.clearPhase();
            // Wait some time after clearing for showing a message to the player
            yield return new WaitForSeconds(4f);
        }
    }
}
