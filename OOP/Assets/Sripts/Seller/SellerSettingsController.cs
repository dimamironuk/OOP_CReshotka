using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellerSettingsController : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    public void ViewProductSeller(SellerController seller)
    {
        ClearButtons();
        for (int i = 0; i < seller.GetCountProduct() && i < _buttons.Length; i++)
        {
            Image[] images = _buttons[i].GetComponentsInChildren<Image>(true);
            switch (seller.GetProduct(i).ItemRarity)
            {
                case Rarity.Common:
                    _buttons[i].image.color = new Color(160f / 255f, 160f / 255f, 160f / 255f);
                    break;
                case Rarity.Uncommon:
                    _buttons[i].image.color = new Color(57f / 255f, 219f / 255f, 84f / 255f);
                    break;
                case Rarity.Rare:
                    _buttons[i].image.color = new Color(57f / 255f, 84f / 255f, 219f / 255f);
                    break;
                case Rarity.Epic:
                    _buttons[i].image.color = new Color(255f / 255f, 0f / 255f, 222f / 255f);
                    break;
                case Rarity.Mythic:
                    _buttons[i].image.color = new Color(253f / 255f, 0f / 255f, 0f / 255f);
                    break;
                case Rarity.Legendary:
                    _buttons[i].image.color = new Color(245f / 255f, 253f / 255f, 0f / 255f);
                    break;
            }


            Image childImage = null;
            foreach (var img in images)
            {
                if (img.gameObject != _buttons[i].gameObject)
                {
                    childImage = img;
                    break;
                }
            }
            if (childImage != null)
            {
                childImage.sprite = seller.GetProduct(i).GetImage();
                childImage.color = Color.white;
            }
            SetButtonInfo(i, seller.GetId());
        }
    }
    private void ClearButtons()
    {
        Image imageChooseProduct = GameObject.Find("I_ChoiceProduct")?.GetComponent<Image>();
        TextMeshProUGUI nameChooseProduct = GameObject.Find("T_NameChoiceProduct")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceChooseProduct = GameObject.Find("T_PriceChoiceProduct")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI infoChooseProduct = GameObject.Find("T_InfoChoiceProduct")?.GetComponent<TextMeshProUGUI>();
        Color c = imageChooseProduct.color;
        c.a = 0f;
        imageChooseProduct.color = c;
        nameChooseProduct.text = "";
        priceChooseProduct.text = "";
        infoChooseProduct.text = "";

        foreach (Button btn in _buttons)
        {
            btn.onClick.RemoveAllListeners();

            btn.image.color = Color.white;

            Image[] childImages = btn.GetComponentsInChildren<Image>(true);
            foreach (Image img in childImages)
            {
                if (img != btn.image)
                {
                    img.sprite = null;
                    img.color = new Color(1f, 1f, 1f, 0f); 
                }
            }
        }
    }
    public void SetButtonInfo(int indexButton, int idSeller)
    {
        _buttons[indexButton].onClick.RemoveAllListeners(); 
        _buttons[indexButton].onClick.AddListener(() => MenuController.ChooseProductInfo(indexButton,idSeller));
    }
}
