using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager Instance = null;
    public static TransitionManager instance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<TransitionManager>();
            return Instance;
        }
    }

    public void TransitionShow(GameObject obj, float speed)
    {
        StartCoroutine(FadeIn(obj, speed));
    }

    private IEnumerator FadeIn(GameObject obj, float speed)
    {
        Image imageComponent = obj.GetComponent<Image>();
        if (imageComponent == null) yield break;

        float t = 0;
        Color color = imageComponent.color;
        color.a = 0;
        imageComponent.color = color;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            color.a = Mathf.Clamp01(t);
            imageComponent.color = color;
            yield return null;
        }
    }
}
