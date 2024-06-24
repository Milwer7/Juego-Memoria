using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows 2D sprites to face the player on every frame.
/// </summary>
public class SpriteBillboard : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }
}
