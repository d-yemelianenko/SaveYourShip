using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;
    [SerializeField]
    private float outlineDistance = 7f;
    [SerializeField]
    private SwitchFlash switchFlash;
    [SerializeField]
    public Animator animHammer;
    public GameObject hammer;
    private float smashAnimTime = 0.8f;
    private float elapsedTime = 0f;

    public KeyCode interactionKey = KeyCode.Mouse0;

    private void Start()
    {
       animHammer = hammer.GetComponent<Animator>();
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

            if (highlight.CompareTag("IceCube") &&  Input.GetKey(interactionKey) && switchFlash.toolsTable[0])
            {
                animHammer.SetBool("Atak", true);
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= smashAnimTime)// Gracz rozbija bloczek
                {
                    animHammer.SetBool("Atak", false);
                    DestroyCube destroyCube = highlight.gameObject.GetComponent<DestroyCube>();
                    destroyCube.BeforeDestroy();
                    Destroy(highlight.gameObject);
                    elapsedTime = 0;
                }
            }
            if (Input.GetKeyUp(interactionKey))
            {
                elapsedTime = 0;
                animHammer.SetBool("Atak", false);
            }

            // Podœwietlanie
            if ((highlight.CompareTag("Selectable") || highlight.CompareTag("Cannon") || highlight.CompareTag("IceCube") || highlight.CompareTag("Inventory") || highlight.CompareTag("Tools")))
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
