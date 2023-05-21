using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    [HideInInspector]
   public  List<Item> item;

    public GameObject database;
    public GameObject dzwiekBeczki;
    public GameObject cellContainer;
    public GameObject cellEkwipunek;
    public KeyCode showInventory;
    public KeyCode takeButton;
    public CharacterController player;
    public GameObject dragPrefab;
  //  public FirstPersonController player;

    public GameObject point;

    public KeyCode interactionKey = KeyCode.I;

   
    private bool isLookingAtWheel = false;





    // Start is called before the first frame update
    void Start()
    {
       
        item = new List<Item>();
        cellContainer.SetActive(true);
        cellEkwipunek.SetActive(false);
        dzwiekBeczki.SetActive(false);

        for (int i = 1; i < cellEkwipunek.transform.childCount; i++) // naznaczamy numer itema
        {
            cellEkwipunek.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }
     


        for (int i = 1; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }

        for (int i =1; i < cellContainer.transform.childCount; i++)
        {
           
            item.Add(new Item());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowInventory();
       // ToggleEkwipunek();
        if (Input.GetKeyDown(takeButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 6f))
            {
                if (hit.collider.GetComponent<Item>())
                {
                    AudioSource pickUpSound = GetComponent<AudioSource>();
                    pickUpSound.Play();
                    AddItem(hit.collider.GetComponent<Item>());
                }
            }
        }
        
    }
    
    void ShowInventory()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 7f))
        {
            if (hit.transform.tag == "Inventory")
            {
                isLookingAtWheel = true;
            }
            else
            {
                isLookingAtWheel = false;
            }
        }
        else
        {
            isLookingAtWheel = false;
        }
        // Rotate the steering wheel based on player input
       if (isLookingAtWheel)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                ToggleEkwipunek();

            }
        }
        else 
        {
            if (cellEkwipunek.activeSelf)
            { 
                   if (Input.GetKeyDown(interactionKey))
                   {
                        cellEkwipunek.SetActive(true);
                        point.SetActive(false);
                        player.enabled = false;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Time.timeScale = 0f;
                        dzwiekBeczki.SetActive(true);
                        dzwiekBeczki.SetActive(false);
                   }             
            }
        }
    }


    void AddItem(Item currentItem)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == 0)
            {
                item[i] = currentItem;
                item[i].countItem = 1;
                DisplayItems();
                Destroy(currentItem.gameObject);
                break;
            }
        }
    }



    void ToggleEkwipunek()
    {
            if (cellEkwipunek.activeSelf)
            {           
                cellEkwipunek.SetActive(false) ;
                player.enabled = true;
                point.SetActive(true);
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                dzwiekBeczki.SetActive(true);
                dzwiekBeczki.SetActive(false);


        }
            else
            {
                cellEkwipunek.SetActive(true);             
                point.SetActive(false);
                player.enabled = false ;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                dzwiekBeczki.SetActive(true);
                dzwiekBeczki.SetActive(false);

        }
        

    }

   public void DisplayItems()
    {
        for(int i =0; i < item.Count; i++)
        { 
                Transform cell = cellContainer.transform.GetChild(i);
                Transform icon = cell.GetChild(0);
                Image img = icon.GetComponent<Image>();  
            if(item[i].id != 0)
            {         
                img.enabled = true;
                img.sprite = item[i].icon;
               // Debug.Log(item[i].id);
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
            }
        }
    }

  
}
