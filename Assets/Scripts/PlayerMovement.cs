using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IEyeInteractable
{
    [SerializeField]
    GameObject player;

    public void Interact(Vector3 coordinates)
    {
        player.transform.position = coordinates;
    }

    public void StopInteraction()
    {
    }
}