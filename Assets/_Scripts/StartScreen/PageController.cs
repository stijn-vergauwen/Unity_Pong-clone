using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] HomeScreen homeScreen;
    [SerializeField] SelectGameScreen selectGameScreen;
    [SerializeField] SettingsScreen settingsScreen;
    [SerializeField] GameObject backButton;

    private void Start() {
        ShowHomeScreen();
    }

    public void ShowHomeScreen() {
        HideAllScreens();
        homeScreen.gameObject.SetActive(true);
        backButton.SetActive(false);
    }

    public void ShowSelectGameScreen() {
        HideAllScreens();
        selectGameScreen.gameObject.SetActive(true);
        selectGameScreen.ShowPlayerSelectScreen();
        backButton.SetActive(true);
    }

    public void ShowOptionsScreen() {
        HideAllScreens();
        settingsScreen.gameObject.SetActive(true);
        backButton.SetActive(true);
    }

    private void HideAllScreens() {
        homeScreen.gameObject.SetActive(false);
        selectGameScreen.gameObject.SetActive(false);
        settingsScreen.gameObject.SetActive(false);
    }
}
