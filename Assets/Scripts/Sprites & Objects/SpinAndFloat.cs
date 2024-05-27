using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndFloat : MonoBehaviour
{
    [SerializeField]
    private int rotationX, rotationY, rotationZ, marginX, marginY, marginZ;
    private int actualRotationX, actualRotationY, actualRotationZ;
    private float startY;
    [SerializeField]
    private float floatSpeed, floatAmplitude;

    private void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        actualRotationX = rotationX + Random.Range(0, marginX); actualRotationY = rotationY + Random.Range(0, marginY); actualRotationZ = rotationZ + Random.Range(0, marginZ);
        transform.Rotate(actualRotationX * Time.deltaTime, actualRotationY * Time.deltaTime, actualRotationZ * Time.deltaTime, Space.Self);

        float offsetYAxis = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.position = new Vector3(transform.position.x, startY + offsetYAxis, transform.position.z);
    }
}
