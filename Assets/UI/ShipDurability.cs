using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDurability : MonoBehaviour
{
    public Texture2D durabilityTexture;
    public int maxStamina = 5;
    public int stamina = 3;

    void OnGUI()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i < stamina)
            {
                GUI.DrawTexture(new Rect(20 + i * 90, 10, 80, 70), durabilityTexture);
            }
            else
            {
                GUI.DrawTexture(new Rect(20 + i * 90, 10, 80, 70), durabilityTexture, ScaleMode.ScaleAndCrop, false, 1f, Color.gray, 0, 0);
            }
        }
    }

    public void ChangeDurability(int changeValue)
    {
        stamina += changeValue;
    }
}
