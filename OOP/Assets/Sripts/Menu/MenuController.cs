using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _sellerPanel = null;
    [SerializeField] private GameObject _menuPausePanel = null;
    [SerializeField] private GameObject _menuPauseButton = null;

    //Seller
    public void ExitSellerPanel()
    {
        _sellerPanel.SetActive(false);
        _menuPauseButton.SetActive(true);
    }
    public void ChooseProductInfo(int index)
    {
        SellerController seller = FindObjectOfType<SellerController>();
        if (seller == null) return;
        if (index < 0 || index >= seller.GetCountProduct())
        {
            index = -1; 
        }

        Product chooseProduct = index >= 0 ? seller.GetProduct(index) : null;
        Image imageChooseProduct = GameObject.Find("I_ChoiceProduct")?.GetComponent<Image>();
        TextMeshProUGUI nameChooseProduct = GameObject.Find("T_NameChoiceProduct")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceChooseProduct = GameObject.Find("T_PriceChoiceProduct")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI infoChooseProduct = GameObject.Find("T_InfoChoiceProduct")?.GetComponent<TextMeshProUGUI>();

        if (chooseProduct != null)
        {
            imageChooseProduct.sprite = chooseProduct.GetImage();
            nameChooseProduct.text = chooseProduct.GetName();
            priceChooseProduct.text = "Price: " + chooseProduct.GetPrice().ToString();
            infoChooseProduct.text = chooseProduct.GetDescription();
            Color c = imageChooseProduct.color;
            c.a = 1f;
            imageChooseProduct.color = c;
        }
        else
        {
            Color c = imageChooseProduct.color;
            c.a = 0f;
            imageChooseProduct.color = c;
            nameChooseProduct.text = "";
            priceChooseProduct.text = "";
            infoChooseProduct.text = "";
        }
    }

    //Menu Pause
    public void OpenMenuPausePanel()
    {
        _menuPausePanel.SetActive(true);
        _menuPauseButton.SetActive(false);
        Time.timeScale = 0.0f;
    }
    public void ExitMenuPausePanel()
    {
        _menuPausePanel.SetActive(false);
        _menuPauseButton.SetActive(true);
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
