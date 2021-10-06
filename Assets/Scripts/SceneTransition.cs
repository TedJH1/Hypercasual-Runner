using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public RawImage fadeImage;

    private void Start()
    {
        StartCoroutine(FadeTransition(null, 1f, 0f));
    }

    // Changes to specified scene when called
    public void StartTransition(int sceneNumber)
    {
        StartCoroutine(FadeTransition(sceneNumber, 0f, 1f));
    }

    // Fade black foreground in or out
    private IEnumerator FadeTransition(int? sceneNumber, float startAlpha, float endAlpha)
    {
        float fadeTime = 1f;
        for (float time = 0f; time < fadeTime; time += Time.deltaTime)
        {
            Color imageColor = fadeImage.color;
            imageColor.a = Mathf.Lerp(startAlpha, endAlpha, time / fadeTime);
            fadeImage.color = imageColor;
            yield return null;
        }
        // If fading out, load the next scene
        if (sceneNumber.HasValue)
            SceneManager.LoadScene((int)sceneNumber);
    }
}
