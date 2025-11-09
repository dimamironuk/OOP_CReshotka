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
