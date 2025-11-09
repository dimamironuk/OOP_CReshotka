using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerSettingsController : MonoBehaviour
{
    [SerializeField] private SellerController _seller;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private GameObject _storageProduct;
    private void Awake()
    {
        _seller.CreateProduct(_storageProduct.GetComponentsInChildren<Product>(includeInactive: true));
    }
    private void Start()
    {

        for (int i = 0; i < _seller.GetCountProduct() && i < _buttons.Length; i++)
        {
            Image[] images = _buttons[i].GetComponentsInChildren<Image>(true);
            switch (_seller.GetProduct(i).ItemRarity)
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
                childImage.sprite = _seller.GetProduct(i).GetImage();
                childImage.color = Color.white; 
            }
            
        }
    }
}
