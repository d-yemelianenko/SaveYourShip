using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    public bool isBeingWatched = false;
    public bool isFishCaught = false;
    private float watchTime = 1.0f;
    private float elapsedTime = 0f;
    private Slider progressBar;

    public float raycastDistance = 2f;
    private bool isCubeAbove = true;
    public Animator animator;

    private void Start()
    {
        Canvas canvas = CreateCanvas();
        progressBar = CreateProgressBar(canvas.transform);
    }

    private void Update()
    {
        CheckIceCube();
        if (!isCubeAbove)
        {
            animator.enabled = true;
            animator.transform.position = new Vector3(transform.position.x, transform.position.y, animator.transform.position.z);
        }
        else
        {
            animator.enabled = false;
        }

        if (isBeingWatched)
        {
            elapsedTime += Time.deltaTime;

            if (progressBar != null)
            {
                progressBar.value = elapsedTime / watchTime;
            }

            if (elapsedTime >= watchTime)// Gracz patrzy³ na rybê przez wymagany czas
            {
                Debug.Log("Z³apana");
                SetOffFishingStatus();
                isFishCaught = true;
            }
        }
        else
        {
            elapsedTime = 0f;

            if (progressBar != null)
            {
                progressBar.value = 0f;
            }
        }
    }

    public void SetOnFishingStatus()
    {
        isBeingWatched = true;
        progressBar.gameObject.SetActive(true);
    }

    public void SetOffFishingStatus()
    {
        isBeingWatched = false;
        elapsedTime = 0f;
        progressBar.gameObject.SetActive(false);
    }
    
    private void CheckIceCube()
    {
        Ray ray = new Ray(transform.position, Vector3.up);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("IceCube"))
            {
                isCubeAbove = true;
            }
            else isCubeAbove = false;
        }
    }

    private Canvas CreateCanvas()
    {
        // Tworzenie obiektu Canvas
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Dodanie komponentu CanvasScaler
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920f, 1080f);

        // Dodanie komponentu GraphicRaycaster
        canvasObject.AddComponent<GraphicRaycaster>();

        return canvas;
    }

    private Slider CreateProgressBar(Transform parent)
    {
        // Tworzenie obiektu Slidera
        GameObject progressBarObject = new GameObject("ProgressBar");
        Slider progressBar = progressBarObject.AddComponent<Slider>();

        // Ustawienia dla obiektu Slidera
        progressBar.minValue = 0f;
        progressBar.maxValue = watchTime;
        progressBar.value = 0f;

        // Tworzenie obiektu Background
        GameObject background = new GameObject("Background");
        RectTransform backgroundRect = background.AddComponent<RectTransform>();
        Image backgroundImage = background.AddComponent<Image>();

        // Tworzenie obiektu Fill Area
        GameObject fillArea = new GameObject("Fill Area");
        RectTransform fillAreaRect = fillArea.AddComponent<RectTransform>();

        // Tworzenie obiektu Fill
        GameObject fill = new GameObject("Fill");
        RectTransform fillRect = fill.AddComponent<RectTransform>();
        Image fillImage = fill.AddComponent<Image>();

        // Ustawianie hierarchii obiektów
        progressBarObject.transform.SetParent(parent);
        backgroundRect.SetParent(progressBarObject.transform);
        fillAreaRect.SetParent(progressBarObject.transform);
        fillRect.SetParent(fillAreaRect);

        // Ustawianie pozycji i skalowania obiektów
        progressBarObject.transform.localPosition = Vector3.zero;
        progressBarObject.transform.localScale = Vector3.one;

        backgroundRect.anchorMin = new Vector2(-1f, 0.5f);
        backgroundRect.anchorMax = new Vector2(2f, 0.5f);
        backgroundRect.pivot = new Vector2(0.0f, 7f);
        backgroundRect.sizeDelta = new Vector2(0f, 20f);

        fillAreaRect.anchorMin = new Vector2(-1f, 0.5f);
        fillAreaRect.anchorMax = new Vector2(2f, 0.5f);
        fillAreaRect.pivot = new Vector2(0.0f, 7f);
        fillAreaRect.sizeDelta = new Vector2(0f, 20f);

        fillRect.anchorMin = new Vector2(-1f, 0.5f);
        fillRect.anchorMax = new Vector2(2f, 0.5f);
        fillRect.pivot = new Vector2(0.0f, 0.5f);
        fillRect.sizeDelta = new Vector2(0f, 10f);

        // Dodanie grafik dla paska
        backgroundImage.sprite = Resources.Load<Sprite>("BackgroundSprite"); // Dodaj odpowiedni sprite dla t³a paska
        fillImage.sprite = Resources.Load<Sprite>("FillSprite"); // Dodaj odpowiedni sprite dla wype³nienia paska
        fillImage.color = new Color(1f, 0.5f, 0f);

        // Ustawienie rodzaju filla na poziomy
        progressBar.fillRect = fillRect;
        progressBar.direction = Slider.Direction.LeftToRight;

        // Ukrycie Slidera na starcie
        progressBar.gameObject.SetActive(false);

        return progressBar;
    }


}
