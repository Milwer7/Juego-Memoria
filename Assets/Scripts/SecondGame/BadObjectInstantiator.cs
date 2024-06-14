using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadObjectInstantiator : MonoBehaviour, IEyeInteractable
{
    [SerializeField]
    private GameObject[] randomObjectToSpawn;
    private System.Random random = new System.Random();
    public SpawnerBehaviour parentSpawner;

    private bool pointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject objectToSpawn = null;
        objectToSpawn = randomObjectToSpawn[random.Next(randomObjectToSpawn.Length)];
        Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
    }
    void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer > requiredHoldTime)
            {
                // TODO: Play a according sound 
                Destroy(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (parentSpawner != null)
        {
            parentSpawner.OnChildDespawned(this);
        }
    }

    public void Interact(Vector3 coordinates)
    {
        pointerDown = true;
    }

    public void StopInteraction()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }
}
