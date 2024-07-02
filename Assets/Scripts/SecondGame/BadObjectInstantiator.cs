using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadObjectInstantiator : MonoBehaviour, IEyeInteractable
{
    // List of random objects to spawn.
    [SerializeField]
    private GameObject[] randomObjectToSpawn;
    // Random trigger
    private System.Random random = new System.Random();
    // Parent spawner to notify.
    public SpawnerBehaviour parentSpawner;

    // eye button clicking variables.
    private bool pointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime = 2f;

    private bool isDestroyedByClick = false;

    void Start()
    {
        // Triggering a random bad food object to spawn.
        GameObject objectToSpawn = null;
        objectToSpawn = randomObjectToSpawn[random.Next(randomObjectToSpawn.Length)];
        Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
    }
    void Update()
    {
        // eye button clicking system.
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                // Destroy the object.
                // TODO: Play a according sound 
                isDestroyedByClick = true;
                Destroy(this.gameObject);
            }
        }
    }

    // Telling the spawner that its child is being destroyed.
    private void OnDestroy()
    {
        if (parentSpawner != null && isDestroyedByClick)
        {
            parentSpawner.OnChildDespawned(this);
        }
    }

    // Click on sight.
    public void Interact(Vector3 coordinates)
    {
        pointerDown = true;
    }

    // Unclick when not seeing the button.
    public void StopInteraction()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }
}
