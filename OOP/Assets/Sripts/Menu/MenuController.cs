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
    public static void ChooseProductInfo(int index, int idSeller)
    {
        SellerController[] sellers = Resources.FindObjectsOfTypeAll<SellerController>();
        if (sellers == null) return;

        SellerController seller = null;
        foreach (SellerController value in sellers)
        {
            if (value.GetId() == idSeller)
            {
                seller = value;
                break;
            }
        }

        if (seller == null) return;

        Product chooseProduct = (index >= 0 && index < seller.GetCountProduct()) ? seller.GetProduct(index) : null;

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
        Time.timeScale = 1.0f;
        Application.LoadLevel(0);
    }

    //Main menu
    public void ButtonNewGame()
    {
        Application.LoadLevel(3);
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
