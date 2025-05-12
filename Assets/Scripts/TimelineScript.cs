using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineScript : MonoBehaviour
{
    public string sceneName = "";
    public float sizeMultiplier = 1.1f;
    public void MouseEnter()
    {
        print($"Mouse entered {sceneName}");
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * sizeMultiplier, gameObject.transform.localScale.y * sizeMultiplier, gameObject.transform.localScale.z);
    }

    public void MouseExit()
    {
        print($"Mouse entered {sceneName}");
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / sizeMultiplier, gameObject.transform.localScale.y / sizeMultiplier, gameObject.transform.localScale.z);
    }

}
