using UnityEngine;

public class Cannon : MonoBehaviour
{
    /*
    public GameObject bulletPrefab; // Prefab pocisku
    public GameObject explosionPrefab; // Prefab efektu eksplozji
    public AudioClip shootSound; // D�wi�k wystrza�u
    [SerializeField]
    public Animator animTorch;
    public GameObject torch;*/

    public Transform shootPoint; // Punkt, z kt�rego wychodzi pocisk
    private Transform highlight;
    private RaycastHit raycastHit;
    [SerializeField]
    private float usingDistance = 7f;
    [SerializeField]
    private SwitchFlash switchFlash;
    [SerializeField]
    private CurrentItem currentItem;
    private float cannonActivationTime = 0.8f;
    private float elapsedTime = 0f;
    private int index;

    private bool isLoaded = false;
    public KeyCode interactionKey = KeyCode.Mouse0;

    private void Start()
    {
        //animTorch = torch.GetComponent<Animator>();
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
                GetComponent<ParticleSystem>().Play();
                GetComponent<AudioSource>().Play();
                isLoaded = false;
                RaycastHit hit;
                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
                {
                    if (hit.collider.CompareTag("IceMountain"))
                    {
                        // Zniszcz obiekt góry
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void SetItemId(int currentId)
    {
        index = currentId;
    }

    /*private void LoadBullet()
    {
        if (isLoaded) return; // Je�eli armata jest ju� za�adowana, nie wykonuj kolejnej �adowania

        // Stw�rz now� instancj� pocisku na pozycji shootPoint
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        isLoaded = true; // Armata jest teraz za�adowana

        // Odtw�rz d�wi�k wystrza�u
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }

    public void Fire()
    {
        if (!isLoaded) return; // Je�eli armata nie jest za�adowana, nie wykonuj strza�u

        // Wystrzel pocisk
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Destroy(bullet, 5f); // Zniszcz pocisk po 5 sekundach

        // Sprawd� trafienie w obiekt g�ry za pomoc� Raycasta
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
        {
            if (hit.collider.CompareTag("Gora"))
            {
                // Zniszcz obiekt g�ry
                Destroy(hit.collider.gameObject);
            }
        }

        // Wywo�aj efekt eksplozji na pozycji trafienia
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, hit.point, Quaternion.identity);
        }

        isLoaded = false; // Armata jest teraz roz�adowana
    }*/
}
