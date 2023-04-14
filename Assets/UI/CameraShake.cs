using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Coroutine shakeCoroutine;
    public float originMagnitude = 0.05f;
    public float magnitude = 0.05f;
    public float duration = 1000.0f;
    public float smoothness = 0.5f;

    public void Shake()
    {
        if (shakeCoroutine == null)
        {
            shakeCoroutine = StartCoroutine(DoShake());
        }
    }

    public void StopShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
            transform.localPosition = Vector3.zero;
        }
    }

    public void ShakeMagnitudeValue(float life)
    {
        magnitude = originMagnitude - (life / 1000);
    }

    private IEnumerator DoShake()
  {
        Vector3 originalPos = transform.localPosition;

        while (true)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            yield return null;
        }
    }
}
