using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour, ISpawner
{
    // Good and bad object models.
    [SerializeField]
    public GameObject goodObjectPrefab, badObjectPrefab;
    // Random trigger
    private System.Random random = new System.Random();
    // Points and life manager object.
    [SerializeField]
    private PointsLifeManager Manager;

    // Spawns a good or bad object.
    public void Spawn()
    {
        // Randomly decide whether to spawn a good or bad object.
        bool spawnGood = random.Next(2) == 0;

        // Choose a random object from the respective array.
        GameObject objectToSpawn;
        // Spawning a good or bad item and assigning this spawner as their parent.
        if (spawnGood)
        {
            objectToSpawn = goodObjectPrefab;
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
            GoodObjectInstantiator goodObjectInstantiator = spawnedObject.GetComponent<GoodObjectInstantiator>();
            goodObjectInstantiator.parentSpawner = this;
            Manager.maxGoodScores++;
        }
        else
        {
            objectToSpawn = badObjectPrefab;
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
            BadObjectInstantiator badObjectInstantiator= spawnedObject.GetComponent<BadObjectInstantiator>();
            badObjectInstantiator.parentSpawner = this;
        }

    }

    // Destroy all children of the spawner.
    public void ClearChilds()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Increases the good scores of the Manager on despawn of a good object.
    public void OnChildDespawned(GoodObjectInstantiator instantiatedObject)
    {
        Manager.goodScores += 1;
    }
    // Increases the bad scores of the Manager on despawn of a bad object.
    public void OnChildDespawned(BadObjectInstantiator instantiatedObject)
    {
        Manager.badScores += 1;
    }

}
