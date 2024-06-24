using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IEyeInteractable
{
    // eye button clicking variables.
    private bool pointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime;

    // Creating a LongClick event.
    public UnityEvent onLongClick;

    // Image to fill while clicking.
    [SerializeField]
    public Image fillImage;


    void Update()
    {
        // eye button clicking system.
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime; 
            if (pointerDownTimer > requiredHoldTime)
            {
                if (onLongClick != null)
                {
                    // Invoking the LongClick event and triggering audio effect.
                    SoundFXManager.instance.PlayClick(transform, 1f);
                    onLongClick.Invoke();
                }
                Reset();
            }
            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }
    // Resetting all the variables after clicking.
    public void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
    // Click on sight.
    public void Interact(Vector3 coordinates)
    {
        pointerDown = true;
    }
    // Unclick when not seeing the button.
    public void StopInteraction() 
    {
        Reset();
    }
}