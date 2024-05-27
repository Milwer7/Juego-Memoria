using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeGazeController : MonoBehaviour
{
    // EyeTracking component
    OVREyeGaze eyeGaze;

    IEyeInteractable last_interactable = null;

    private void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();
    }

    private void Update()
    {
        if (eyeGaze == null) return;

        // Eye Gaze component working with eye tracking capabilities
        if (eyeGaze.EyeTrackingEnabled)
        {
            // Drawing the ray for testing purposes
            Debug.DrawRay(eyeGaze.transform.position, eyeGaze.transform.forward * 300, Color.green);

            if (Physics.Raycast(eyeGaze.transform.position, eyeGaze.transform.forward * 300, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<IEyeInteractable>(out var interactable))
                {
                    if (last_interactable != null && last_interactable != interactable)
                    {
                        last_interactable.StopInteraction();
                    }
                    last_interactable = interactable;
                    // Debug.Log("Collisioning with: " + hit.collider.gameObject.name);
                    interactable.Interact(hit.point);
                }
            }
            else
            {
                if (last_interactable != null)
                {
                    last_interactable.StopInteraction();
                }
            }
        }
    }
}