using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject portal, meteor;
    [SerializeField]
    private GameObject spawnPlane;
    // Plane Coordinates
    private float minX, minY, maxX, maxY;
    [SerializeField]
    private int portalAmount, meteorAmount;
    [SerializeField]
    private PointsManager Manager;
    public float duration = 4.0f;
    [SerializeField]
    private GameObject tutorialObject;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the plane limits, a Unity Plane with scale 1 has 5x5 dimensions.
        Vector3 scale = spawnPlane.transform.localScale;
        minX = -5 * scale.z / 2f;
        maxX = 5 * scale.z / 2f;
        minY = -5 * scale.x / 2f;
        maxY = 5 * scale.x / 2f;
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(10f);
        tutorialObject.SetActive(false);
        while (portalAmount + meteorAmount > 0)
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds

            // Spawn a portal at a random position within the spawn plane
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            int spawnChoice = Random.Range(1, portalAmount + meteorAmount);

            GameObject newObject = GetRandomObject(spawnChoice);
            newObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            // Make the new portal a child of the SpawnManager GameObject
            newObject.transform.parent = transform;
            newObject.transform.position = new Vector3(x, y, transform.position.z);
            // Debug.Log("Spawn position is:" + x + y);
            Debug.Log($"Spawn speed is: {duration + Manager.GetComboMultiplier()}");
            StartCoroutine(AnimateObject(newObject));
        }
        // Game end timer
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneChanger.instance.ChangeScene(0);
    }

    public GameObject GetRandomObject(int spawnChoice)
    {
        GameObject newObject;
        
        if (spawnChoice <= portalAmount)
        {
            newObject = Instantiate(portal, new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<ObstacleBehaviour>().AttachManager(Manager);
            newObject.GetComponent<ObstacleBehaviour>().SetPointsModifier(1);
            portalAmount--;
        }
        else 
        {
            newObject = Instantiate(meteor, new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<ObstacleBehaviour>().AttachManager(Manager);
            newObject.GetComponent<ObstacleBehaviour>().SetPointsModifier(-1);
            meteorAmount--;
        }
        return newObject;

    }

    private IEnumerator AnimateObject(GameObject instanciatedObject)
    {
        float objectSpeed = duration + Manager.GetComboMultiplier();
        float elapsedTime = 0f;
        Vector3 initialPosition = instanciatedObject.transform.position;
        Vector3 targetPosition = spawnPlane.transform.position;

        while (elapsedTime < objectSpeed && instanciatedObject != null)
        {
            // Move the object towards the Plane position
            instanciatedObject.transform.position = Vector3.Lerp(initialPosition, new Vector3(instanciatedObject.transform.position.x, instanciatedObject.transform.position.y, targetPosition.z), elapsedTime / objectSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (instanciatedObject != null)
        {
            instanciatedObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, targetPosition.z);

            // Despawn the object
            Destroy(instanciatedObject);
        }
    }
}
