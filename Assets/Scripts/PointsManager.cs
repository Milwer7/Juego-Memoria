using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public float actualCombo = 0f;
    [SerializeField] TextMeshProUGUI pointsLabel, comboLabel;

    public float GetComboMultiplier() {
        if (actualCombo < 10)
        {
            return -1 * (actualCombo * 0.15f);
        }
        else
        {
            return -1.5f;
        }
    }

    public void ModifyPoints(int value)
    {
        if (int.TryParse(pointsLabel.text, out int currentValue))
        {
            int newValue = currentValue + value;
            pointsLabel.text = newValue.ToString();

            if (value > 0)
            {
                actualCombo += 1;
            }
            else
            {
                actualCombo = 0f;
            }
            comboLabel.text = actualCombo.ToString();
            SoundFXManager.instance.BackgroundMusic.pitch = 1 + Mathf.Min((actualCombo * 0.03f), 1.4f);
        }
    }
}
