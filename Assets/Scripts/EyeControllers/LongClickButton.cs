using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IEyeInteractable
{
    private bool pointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime;

    public UnityEvent onLongClick;

    [SerializeField]
    public Image fillImage;

    public void Interact(Vector3 coordinates)
    {
        pointerDown = true;
    }

    public void StopInteraction() 
    {
        Reset();
    }

    void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime; 
            if (pointerDownTimer > requiredHoldTime)
            {
                if (onLongClick != null)
                {
                    SoundFXManager.instance.PlayClick(transform, 1f);
                    onLongClick.Invoke();
                }
                Reset();
            }
            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }
    public void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
}