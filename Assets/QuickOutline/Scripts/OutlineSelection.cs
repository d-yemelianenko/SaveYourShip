using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    [SerializeField]
    private float outlineDistance = 7f;
    [SerializeField]
    private SwitchFlash switchFlash;
    [SerializeField]
    public Animator anim;
    public GameObject mlot;
    private float smashAnimTime = 0.8f;
    private float elapsedTime = 0f;

    public KeyCode interactionKey = KeyCode.Mouse0;

    private void Start()
    {
        //mlot.enabled = false;
       anim = mlot.GetComponent<Animator>();
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Niszczenie
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, outlineDistance)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;

            if (highlight.CompareTag("IceCube") && highlight != selection && Input.GetKey(interactionKey) && switchFlash.toolsTable[0])
            {
                anim.SetBool("Atak", true);
                elapsedTime += Time.deltaTime;
                Debug.Log(elapsedTime);
                if (elapsedTime >= smashAnimTime)// Gracz patrzy³ na rybê przez wymagany czas
                {
                    anim.SetBool("Atak", false);
                    Destroy(highlight.gameObject);
                    elapsedTime = 0;
                }
            }
            if (Input.GetKeyUp(interactionKey))
            {
                elapsedTime = 0;
                anim.SetBool("Atak", false);
            }

            // Podœwietlanie
            if ((highlight.CompareTag("Selectable") || highlight.CompareTag("IceCube") || highlight.CompareTag("Inventory") || highlight.CompareTag("Tools")) && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
}
