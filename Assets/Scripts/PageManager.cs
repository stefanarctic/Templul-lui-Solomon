using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{

    private static PageManager Instance = null;
    public static PageManager instance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<PageManager>();
            return Instance;
        }
    }

    public Camera cameraPlayer;
    public Camera cameraEnvironment;

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


    public Animator locationTextAnimator;
    public MenuScript menuScript;
    public GameObject storyMenu;
    public GameObject background;
    public GameObject currentPage;
    public GameObject[] pages;

    public int pageNumber = 0;

    public void Init()
    {
        storyMenu.SetActive(true);
        foreach(GameObject page in pages)
        {
            page.SetActive(false);
        }
        pageNumber = 1;
        currentPage = pages[0];
        currentPage.SetActive(true);
    }

    public bool CheckSceneIfInterior()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1;
    }

    public void NextPage()
    {
        if (pageNumber >= pages.Length && CheckSceneIfInterior())
            return;
        currentPage.SetActive(false);
        currentPage = pages[++pageNumber - 1];
        currentPage.SetActive(true);

    }

    public void PreviousPage()
    {

        if (pageNumber <= 1 && CheckSceneIfInterior())
            return;
        currentPage.SetActive(false);
        currentPage = pages[--pageNumber - 1];
        currentPage.SetActive(true);
    }

    public void Page1Next()
    {
        //print("Page1Next before next page");
        NextPage();
        //print("Page1Next after next page");
    }

    public void Page2Next()
    {
        NextPage();
        //background.SetActive(false);
    }

    public void Page3Next()
    {
        storyMenu.SetActive(false);
        menuScript.OnPlay();
        MenuScript.instance.ShowPauseMenu();
        MenuScript.instance.HidePauseMenu();
        StartGame();
    }

    public void Page4Next()
    {
        //go to pyramid
        storyMenu.SetActive(false);
        menuScript.OnPlay();
        MenuScript.instance.ShowPauseMenu();
        MenuScript.instance.HidePauseMenu();
        locationTextAnimator.SetTrigger("TriggerFadeIn");/**/
    }

}
