using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour
{

    public GameObject titleMenu;
    public GameObject settingsMenu;

    private static MenuNavigator Instance = null;
    public static MenuNavigator instance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<MenuNavigator>();
            return Instance;
        }
    }

    public enum MenuType
    {
        None,
        TitleMenu,
        SettingsMenu,
        PauseMenu,
        CreditsMenu
    }

    private MenuType _currentMenu = MenuType.None;
    public MenuType currentMenu
    {
        get
        {
            return _currentMenu;
        }
        set
        {
            _currentMenu = value;
        }
    }

}
