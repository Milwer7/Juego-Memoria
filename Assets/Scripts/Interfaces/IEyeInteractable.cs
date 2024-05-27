using UnityEngine;

public interface IEyeInteractable
{
    void Interact(Vector3 coordinates);
    void StopInteraction();
}