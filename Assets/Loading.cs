using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation asyncOperation;
    public Image loadBar;
    public Text loadingPercent;
    public int sceneId;

    private void Start()
    {
        StartCoroutine(LoadSceneCor());
    }
    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(5f) ;
        asyncOperation = SceneManager.LoadSceneAsync(sceneId);
        while(!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.5f;
            loadBar.fillAmount = progress;
            loadingPercent.text = "Loading...";
            yield return 0;
        }
    }
}
