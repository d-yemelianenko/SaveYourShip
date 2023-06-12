using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform shootPoint; // Punkt, z kt�rego wychodzi pocisk
    private Transform highlight;
    private RaycastHit raycastHit;
    [SerializeField]
    private float usingDistance = 7f;
    [SerializeField]
    private SwitchFlash switchFlash;
    [SerializeField]
    private CurrentItem currentItem;
    [SerializeField]
    private GameObject boomEffect;
    [SerializeField]
    private GameObject boomEffectMountain;
    private float cannonActivationTime = 0.8f;
    private float elapsedTime = 0f;
    private int index;

    private bool isLoaded = false;
    public KeyCode interactionKey = KeyCode.Mouse0;

    private void Start()
    {
        isLoaded = false; 
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, usingDistance))
        {
            highlight = raycastHit.transform;

            if (highlight.CompareTag("Cannon") && Input.GetKey(interactionKey) && !isLoaded && switchFlash.toolsTable[3])
            {
                isLoaded = true;
                currentItem.Remove(index);
                switchFlash.switchFlashC();
            }

            if (highlight.CompareTag("Cannon") && Input.GetKey(interactionKey) && isLoaded && switchFlash.toolsTable[2])
            {
                boomEffect.GetComponent<ParticleSystem>().Play();
                GetComponent<AudioSource>().Play();
                isLoaded = false;
                RaycastHit hit;
                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
                {
                    if (hit.collider.CompareTag("IceMountain"))
                    {
                        ParticleSystem particle = boomEffectMountain.GetComponent<ParticleSystem>();
                        particle.transform.position = hit.collider.transform.position;
                        particle.Play();
                        
                        Destroy(hit.collider.gameObject);   // Zniszcz obiekt góry
                    }
                }
            }
        }
    }

    public void SetItemId(int currentId)
    {
        index = currentId;
    }
}
