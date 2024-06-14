using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour, ISpawner
{
    [SerializeField] // Good and bad object models.
    public GameObject goodObjectPrefab, badObjectPrefab;

    // Random 
    private System.Random random = new System.Random();
    private PointsLifeManager Manager;

    public void Spawn()
    {
        // Randomly decide whether to spawn a good or bad object
        bool spawnGood = random.Next(2) == 0;

        // Choose a random object from the respective array
        GameObject objectToSpawn;
        // Spawning a good or bad item and assigning this spawner as their parent.
        if (spawnGood)
        {
            objectToSpawn = goodObjectPrefab;
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
            GoodObjectInstantiator goodObjectInstantiator = spawnedObject.GetComponent<GoodObjectInstantiator>();
            goodObjectInstantiator.parentSpawner = this;
        }
        else
        {
            objectToSpawn = badObjectPrefab;
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
            BadObjectInstantiator badObjectInstantiator= spawnedObject.GetComponent<BadObjectInstantiator>();
            badObjectInstantiator.parentSpawner = this;
        }

    }

    public void ClearChilds()
    {
        // Destroy all children of the spawner
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnChildDespawned(GoodObjectInstantiator instantiatedObject)
    {
        Manager.goodScores += 1;
    }
    public void OnChildDespawned(BadObjectInstantiator instantiatedObject)
    {
        Manager.badScores += 1;
    }

}
