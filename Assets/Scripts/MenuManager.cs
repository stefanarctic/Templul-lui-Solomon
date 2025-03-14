using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private static MenuManager Instance = null;
    public static MenuManager instance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<MenuManager>();
            return instance;
        }
    }

    private bool _isMenuOpen = false;

    public bool isMenuOpen
    {
        get
        {
            return _isMenuOpen;
        }
        set
        {
            if (value)
                ShowMenu();
            else
                HideMenu();
            _isMenuOpen = value;
        }
    }

    private void Start()
    {
        isMenuOpen = _isMenuOpen;
    }

    public void ShowMenu()
    {

    }

    public void HideMenu()
    {

    }

}
