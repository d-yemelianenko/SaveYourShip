using UnityEngine;

public class ShipDurability : MonoBehaviour
{
    public GameObject PlayerInterface;
    public GameObject InventoryPanel;
    public GameObject GameOver;
    public Texture2D durabilityTexture;
    public int maxStamina = 5;
    public int stamina = 5;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameOver.SetActive(false);
        Time.timeScale = 1f;
    }
    void OnGUI()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i < stamina)
            {
                GUI.DrawTexture(new Rect(20 + i * 90, 10, 80, 70), durabilityTexture);
            }
            
            if (stamina <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameOver.SetActive(true);
                Time.timeScale = 0;
                PlayerInterface.SetActive(false);
                InventoryPanel.SetActive(false);
            }
        }
    }

    public void ChangeDurability(int changeValue)
    {
        stamina += changeValue;
    }
}
