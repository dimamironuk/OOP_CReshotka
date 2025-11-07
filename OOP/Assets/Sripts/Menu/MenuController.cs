using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _sellerPanel = null;
    [SerializeField] private GameObject _menuPausePanel = null;

    //Seller
    public void ExitSellerPanel()
    {
        _sellerPanel.SetActive(false);
    }

    //Menu Pause
    public void OpenMenuPausePanel()
    {
        _menuPausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ExitMenuPausePanel()
    {
        _menuPausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ButtonGoMainMenu()
    {
        Application.LoadLevel(0);
    }

    //Main menu
    public void ButtonNewGame()
    {
        Application.LoadLevel(1);
    }
    public void ButtonGame()
    {

    }
    public void ButtonSettings()
    {

    }
    public void ButtonExit()
    {
        Application.Quit();
    }
}
