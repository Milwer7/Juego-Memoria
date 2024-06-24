using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseSkipButton : MonoBehaviour, IEyeInteractable
{
    // eye button clicking variables.
    private bool pointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime = 2f;

    [SerializeField]
    SecondGameSpawner spawner;

    void Update()
    {
        // eye button clicking system.
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer > requiredHoldTime)
            {
                // Skips the Phase on click.
                // TODO: Play a according sound 
                spawner.SkipPhase();
                pointerDownTimer = 0;
            }
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
