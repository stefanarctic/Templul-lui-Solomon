using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    public Camera cameraPlayer;
    public Camera cameraEnvironment;

    private void Start()
    {
        StartUI();
    }

    public void StartUI()
    {
        cameraPlayer.gameObject.SetActive(false);
        cameraEnvironment.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        cameraPlayer.gameObject.SetActive(true);
        cameraEnvironment.gameObject.SetActive(false);
    }
}
