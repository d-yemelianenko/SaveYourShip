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

    /*
    // Metoda dodaj¹ca obs³ugê zdarzeñ przeci¹gania i upuszczania dla komórki
    private void AddDragDropHandlers(GameObject cell)
    {
        EventTrigger eventTrigger = cell.GetComponent<EventTrigger>();
        if (eventTrigger == null)
            eventTrigger = cell.AddComponent<EventTrigger>();

        // Dodanie obs³ugi zdarzenia rozpoczêcia przeci¹gania
        EventTrigger.Entry onBeginDragEntry = new EventTrigger.Entry();
        onBeginDragEntry.eventID = EventTriggerType.BeginDrag;
        onBeginDragEntry.callback.AddListener((data) => { OnBeginDragCell((PointerEventData)data); });
        eventTrigger.triggers.Add(onBeginDragEntry);

        // Dodanie obs³ugi zdarzenia przeci¹gania
        EventTrigger.Entry onDragEntry = new EventTrigger.Entry();
        onDragEntry.eventID = EventTriggerType.Drag;
        onDragEntry.callback.AddListener((data) => { OnDragCell((PointerEventData)data); });
        eventTrigger.triggers.Add(onDragEntry);

        // Dodanie obs³ugi zdarzenia upuszczenia
        EventTrigger.Entry onDropEntry = new EventTrigger.Entry();
        onDropEntry.eventID = EventTriggerType.Drop;
        onDropEntry.callback.AddListener((data) => { OnDropCell((PointerEventData)data); });
        eventTrigger.triggers.Add(onDropEntry);
    }

    // Metoda obs³uguj¹ca rozpoczêcie przeci¹gania komórki
    private void OnBeginDragCell(PointerEventData eventData)
    {
        GameObject cell = eventData.pointerPress.gameObject;

        if (cell.transform.childCount > 0)
        {
            draggedItem = cell.transform.GetChild(0).gameObject;
            previousParent = cell.transform;

            // Zablokowanie interakcji z przeci¹ganym obiektem
            CanvasGroup canvasGroup = draggedItem.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = draggedItem.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;

            // Przeniesienie przeci¹ganej ikony na wy¿sz¹ warstwê renderingu
            draggedItem.transform.SetParent(draggedItem.transform.root);
        }
    }

    // Metoda obs³uguj¹ca przeci¹ganie komórki
    private void OnDragCell(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            // Aktualizacja pozycji przeci¹ganej ikony na pozycjê kursora
            RectTransform draggedItemRectTransform = draggedItem.GetComponent<RectTransform>();
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = Camera.main.transform.InverseTransformPoint(draggedItemRectTransform.position).z;
            draggedItemRectTransform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
    } }

    // Metoda obs³uguj¹ca upuszczenie komórki
    /*
    private void OnDropCell(PointerEventData eventData)
    {
        GameObject cell = eventData.pointerEnter.gameObject;

        if (cell != null && cell != draggedItem && cell.GetComponent<Inventory>() == null)
        {
            // Sprawdzenie, czy komórka docelowa jest pusta
            if (cell.transform.childCount == 0)
            {
                draggedItem.transform.SetParent(cell.transform);
                draggedItem.transform.localPosition = Vector3.zero;

                // Zaktualizowanie indeksu wybranej komórki
                selectedSlotIndex = cell.GetComponent<CurrentItem>().index;
            }
            // Jeœli komórka docelowa nie jest pusta, zamiana miejscami ikon
            else
            {
                Transform draggedItemParent = draggedItem.transform.parent;
                Transform cellParent = cell.transform.parent;

                draggedItem.transform.SetParent(cellParent);
                draggedItem.transform.localPosition = Vector3.zero;

                cell.transform.SetParent(draggedItemParent);
                cell.transform.localPosition = Vector3.zero;
            }
        }

        // Przywrócenie interakcji z przeci¹ganym obiektem
        if (draggedItem != null)
        {
            CanvasGroup canvasGroup = draggedItem.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = true;

            draggedItem.transform.SetParent(previousParent);
            draggedItem.transform.localPosition = Vector3.zero;

            draggedItem = null;
            previousParent = null;
        }
    }*/
}

