using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<Item> item;

    public Fishing fishing;
    public GameObject database;
    public GameObject dzwiekBeczki;
    public GameObject cellContainer;
    public GameObject cellEkwipunek;
    public CharacterController player;
    public GameObject dragPrefab;
    public SwitchFlash switchFlash;
    private int selectedSlotIndex = 0; // domyœlnie brak wybranego slotu (-1 oznacza brak wybranego slotu)
    public KeyCode interactionKey;
    public CurrentItem[] currentItem;

    //  public FirstPersonController player;
    [SerializeField]
    private GameObject playerObj;

    public GameObject point;

    private GameObject draggedItem; // Przechowuje referencjê do przeci¹ganej ikony
    private Transform previousParent; // Przechowuje referencjê do poprzedniego rodzica ikony

    private bool isLookingAtBarrel = false;


    // Start is called before the first frame update
    void Start()
    {
        item = new List<Item>();
        cellContainer.SetActive(true);
        cellEkwipunek.SetActive(false);
        dzwiekBeczki.SetActive(false);

        for (int i = 0; i < cellEkwipunek.transform.childCount; i++) // naznaczamy numer itema
        {
            cellEkwipunek.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            GameObject cell = cellContainer.transform.GetChild(i).gameObject;
            //AddDragDropHandlers(cell);
        }

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            item.Add(new Item());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowInventory();
        // ToggleEkwipunek();
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 6f))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null && (item.id != 1 && item.id != 2)) //Podnoszenie przedmiotów innych ni¿ rybka
            {
                if (Input.GetKeyDown(interactionKey))
                {
                    AudioSource pickUpSound = GetComponent<AudioSource>();
                    pickUpSound.Play();
                    AddItem(hit.collider.GetComponent<Item>());
                }
            }
            else if (item != null && (item.id == 1 || item.id == 2) && switchFlash.toolsTable[1]) //wedka
            {
                fishing = hit.collider.GetComponent<Fishing>();
                fishing.SetOnFishingStatus();
                if (fishing.isFishCaught == true)
                {
                    AudioSource pickUpSound = GetComponent<AudioSource>();
                    pickUpSound.Play();
                    AddItem(hit.collider.GetComponent<Item>());
                    fishing.isFishCaught = false;
                    fishing.SetOffFishingStatus();
                }
            }
            else if (item == null)
            {
                if (fishing != null && fishing.isBeingWatched)
                {
                    fishing.SetOffFishingStatus();
                }
            }
        }
        else if (fishing != null && fishing.isBeingWatched)
        {
            fishing.SetOffFishingStatus();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) currentItem[0].ChooseActiveItem();
        if (Input.GetKeyUp(KeyCode.Alpha2)) currentItem[1].ChooseActiveItem();
        if (Input.GetKeyUp(KeyCode.Alpha3)) currentItem[2].ChooseActiveItem();
        if (Input.GetKeyUp(KeyCode.Alpha4)) currentItem[3].ChooseActiveItem();
        if (Input.GetKeyUp(KeyCode.Alpha5)) currentItem[4].ChooseActiveItem();
        if (Input.GetKeyUp(KeyCode.Alpha6)) currentItem[5].ChooseActiveItem();
    }

    void ShowInventory()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 7f))
        {
            if (hit.transform.tag == "Inventory")
            {
                isLookingAtBarrel = true;
            }
            else
            {
                isLookingAtBarrel = false;
            }
        }
        else
        {
            isLookingAtBarrel = false;
        }

        if (isLookingAtBarrel)
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
        Debug.Log(currentItem.id);
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == 0)
            {
                item[i] = currentItem;
                item[i].countItem = 1;
                selectedSlotIndex = i;
                DisplayItems();
                Destroy(currentItem.gameObject);
                break;
            }
        }
    }

    void AddItem(Item currentItem, int slotNr)
    {
            if (item[slotNr].id == 0)
            {
                item[slotNr] = currentItem;
                item[slotNr].countItem = 1;
                selectedSlotIndex = slotNr;
                DisplayItems();
                Destroy(currentItem.gameObject);
            }
    }

    void ToggleEkwipunek()
    {
        if (cellEkwipunek.activeSelf)   // zamkniecie beczki
        {
            cellEkwipunek.SetActive(false);
            player.enabled = true;
            point.SetActive(true);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            dzwiekBeczki.SetActive(true);
            dzwiekBeczki.SetActive(false);
            selectedSlotIndex = -1;
        }
        else                            // Otwarcie beczki
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

    public void DisplayItems()
    {
        for (int i = 0; i < item.Count; i++)
        {

            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Image img = icon.GetComponent<Image>();
            if (item[i].id != 0)
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

