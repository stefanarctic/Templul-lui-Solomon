using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public GameObject titleMenu;
    public GameObject settingsMenu;
    public GameObject pauseSettings;
    public GameObject pauseMenu;
    public GameObject creditsMenu;
    //public GameObject loadingScreen;

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    public AudioSource audioSource;
    public AudioClip ambienceAudioClip;
    public AudioClip musicAudioClip;
    public AudioClip interiorAudioClip;

    public Transform position1;
    public Transform position2;

    public Slider volumeSlider;
    public Slider pauseVolumeSlider;

    public Slider pauseSensitivitySlider;
    public Slider settingsSensitivitySlider;

    private bool isPlaying = false;
    public bool isPauseMenuOpen = false;
    public bool isSettingsMenuOpen = false;
    public bool isCreditsMenuOpen = false;

    private float audioSourceVolume;

    public string outsidePyramidName = "OutsidePyramidScene";


    private static MenuScript Instance = null;
    public static MenuScript instance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<MenuScript>();
            return Instance;
        }
    }

    private void Start()
    {
        //audioSource.loop = true;
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
        {
            ShowMenu();
            ActivateMusic();
        } else
        {
            ActivateInteriorMusic();
        }
        //audioSourceVolume = audioSource.volume;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
            OnPlay();
        //volumeSlider.value = audioSourceVolume;
        //pauseVolumeSlider.value = audioSourceVolume;
    }

    private void Update()
    {

        //print($"Volume: {audioSource.volume}, Slider: {volumeSlider.value}");

        if(Input.GetKeyDown(KeyCode.Escape) && isPlaying && !(isSettingsMenuOpen))
        {
            if (isPauseMenuOpen)
                HidePauseMenu();
            else
                ShowPauseMenu();
        }
    }

    public void MuteVolume()
    {
        audioSourceVolume = audioSource.volume;
        audioSource.volume = 0f;
    }

    public void UnMuteVolume()
    {
        audioSource.volume = audioSourceVolume;
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void OnSliderValueChanged(float newValue)
    {
        audioSource.volume = newValue;
    }

    public void OnSensitivityValueChanged(float newValue)
    {
        mouseLook.mouseSensitivity = newValue;
    }

    public void ShowPauseMenu()
    {
        //PauseMusic();
        Time.timeScale = 0f;
        isPauseMenuOpen = true;
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.PauseMenu;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        pauseMenu.SetActive(true);
        pauseSensitivitySlider.value = mouseLook.mouseSensitivity;
        //if(takeParchmentScript != null)
        //    TakeParchmentScript.instance.HideCrosshair();
    }

    public void HidePauseMenu()
    {
        //PlayMusic();
        Time.timeScale = 1f;
        isPauseMenuOpen = false;
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.None;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
        mouseLook.enabled = true;
        pauseMenu.SetActive(false);
    }

    public void OnPlay()
    {
        isPlaying = true;
        HideMenu();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
            ActivateAmbience();
        else
            ActivateInteriorMusic();
        //GameObject playerGameObject = FindObjectOfType<PlayerMovement>().gameObject;
        //playerTransform.position = position1.position;
        //playerGameObject.transform.position = position2.position;
    }

    public void OnOpenSettings()
    {
        HideMenu();
        OpenSettingsMenu();
    }

    public void OnCloseSettings()
    {
        isSettingsMenuOpen = false;
        if (isPauseMenuOpen)
        {
            ShowPauseMenu();
            HideSettingsMenu();
            return;
        }
        HideSettingsMenu();
        ShowMenu();
    }

    public void OnOpenSettingsFromPauseMenu()
    {
        HidePauseMenu();
        OpenSettingsMenu();
        //pauseVolumeSlider.value = audioSourceVolume;
    }

    public void OnCloseSettingsFromPauseMenu()
    {
        HideSettingsMenu();
        ShowPauseMenu();
    }

    public void ActivateMusic()
    {
        //audioSource.Stop();
        //audioSource.clip = musicAudioClip;
        //audioSource.volume = 0.3f;

        //volumeSlider.value = audioSource.volume;

        //audioSource.Play();
    }

    public void ActivateAmbience()
    {
        //audioSource.Stop();
        //audioSource.clip = ambienceAudioClip;
        //audioSource.volume = 0.1f;
        //audioSource.Play();
    }

    public void ActivateInteriorMusic()
    {
        //print("Activated interior music");
        //audioSource.Stop();
        //audioSource.clip = interiorAudioClip;
        //audioSource.volume = 0.3f;
        //audioSource.Play();
    }

    public void ShowMenu()
    {
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.TitleMenu;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        titleMenu.SetActive(true);
    }

    public void HideMenu()
    {
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.None;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
        mouseLook.enabled = true;
        titleMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        isSettingsMenuOpen = true;
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.SettingsMenu;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        settingsMenu.SetActive(true);
        settingsSensitivitySlider.value = mouseLook.mouseSensitivity;
    }

    public void HideSettingsMenu()
    {
        isSettingsMenuOpen = false;
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.None;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
        mouseLook.enabled = true;
        settingsMenu.SetActive(false);
    }

    public void OnOpenPauseSettings()
    {
        HidePauseMenu();
        OpenPauseSettings();
    }

    public void OnClosePauseSettings()
    {
        HidePauseSettings();
        ShowPauseMenu();
    }

    public void OpenPauseSettings()
    {
        //PauseMusic();
        isSettingsMenuOpen = true;
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.SettingsMenu;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        pauseSettings.SetActive(true);
    }

    public void HidePauseSettings()
    {
        //PlayMusic();
        isSettingsMenuOpen = false;
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.None;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
        mouseLook.enabled = true;
        pauseSettings.SetActive(false);
    }

    public void GoToMenuFromSettings()
    {
        //SceneManager.instance.ReloadScene();
        SceneManager.instance.ChangeScene(outsidePyramidName);
    }

    public void ShowCreditsMenu()
    {
        isCreditsMenuOpen = true;
        creditsMenu.SetActive(true);
    }

    public void HideCreditsMenu()
    {
        isCreditsMenuOpen = false;
        creditsMenu.SetActive(false);
    }

    public void GoToCreditsMenuFromTitleMenu()
    {
        HideMenu();
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.CreditsMenu;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        ShowCreditsMenu();
    }

    public void GoToTitleMenuFromCreditsMenu()
    {
        HideCreditsMenu();
        MenuNavigator.instance.currentMenu = MenuNavigator.MenuType.TitleMenu;
        ShowMenu();
    }

    //public void PostProcessingSetActive(bool b)
    //{
    //    postProcessLayer.enabled = b;
    //}

    //public void AntiAliasingSetActive(bool b)
    //{
    //    if (b)
    //        postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
    //    else
    //        postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
    //}

    //public void AmbientOcclusionSetActive(bool b)
    //{
    //    postProcessVolume.profile.GetSetting<AmbientOcclusion>().active = b;
    //}

    //public void BloomSetActive(bool b)
    //{
    //    postProcessVolume.profile.GetSetting<Bloom>().active = b;
    //}

    //public void VignetteSetActive(bool b)
    //{
    //    postProcessVolume.profile.GetSetting<Vignette>().active = b;
    //}

    //public void MotionBlurSetActive(bool b)
    //{
    //    postProcessVolume.profile.GetSetting<MotionBlur>().active = b;
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

}
