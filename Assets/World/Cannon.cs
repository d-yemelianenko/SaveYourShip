using UnityEngine;

public class Cannon : MonoBehaviour
{
    /*
    public Transform shootPoint; // Punkt, z którego wychodzi pocisk
    public GameObject bulletPrefab; // Prefab pocisku
    public GameObject explosionPrefab; // Prefab efektu eksplozji
    public AudioClip shootSound; // DŸwiêk wystrza³u
    [SerializeField]
    public Animator animTorch;
    public GameObject torch;*/

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
        if (!isLoaded && Physics.Raycast(ray, out raycastHit, usingDistance))
        {
            highlight = raycastHit.transform;

            if (highlight.CompareTag("Cannon") && Input.GetKey(interactionKey) && switchFlash.toolsTable[3])
            {
                isLoaded = true;
                currentItem.Remove(index);
                switchFlash.switchFlashC();
                //Destroy(highlight.gameObject);
                //animTorch.SetBool("Atak", true);
                //elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime);
                //if (elapsedTime >= smashAnimTime)// Gracz patrzy³ na rybê przez wymagany czas
                //{
                //    animHammer.SetBool("Atak", false);
                //    elapsedTime = 0;
                //}
            }
        }
    }

    public void SetItemId(int currentId)
    {
        index = currentId;
    }

    /*private void LoadBullet()
    {
        if (isLoaded) return; // Je¿eli armata jest ju¿ za³adowana, nie wykonuj kolejnej ³adowania

        // Stwórz now¹ instancjê pocisku na pozycji shootPoint
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        isLoaded = true; // Armata jest teraz za³adowana

        // Odtwórz dŸwiêk wystrza³u
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }

    public void Fire()
    {
        if (!isLoaded) return; // Je¿eli armata nie jest za³adowana, nie wykonuj strza³u

        // Wystrzel pocisk
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Destroy(bullet, 5f); // Zniszcz pocisk po 5 sekundach

        // SprawdŸ trafienie w obiekt góry za pomoc¹ Raycasta
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
        {
            if (hit.collider.CompareTag("Gora"))
            {
                // Zniszcz obiekt góry
                Destroy(hit.collider.gameObject);
            }
        }

        // Wywo³aj efekt eksplozji na pozycji trafienia
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, hit.point, Quaternion.identity);
        }

        isLoaded = false; // Armata jest teraz roz³adowana
    }*/
}
