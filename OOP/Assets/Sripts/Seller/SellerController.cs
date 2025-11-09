using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellerController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string _nameSeller;
    [SerializeField] private List<Product> _products;
    [SerializeField] private GameObject _sellerPanel;
    [SerializeField] private GameObject _menuPauseButton;
    [SerializeField] private GameObject  _storageProduct;
    private bool _playerNear = false;
    public void CreateProduct(Product[] products)
    {
        _products.AddRange(products);
    }

    public void BuyProduct(int index)
    {
        if(index > _products.Count || index < 0)
        {
            Debug.Log("Error: Out of list");
        }
        else
        {
            _products.RemoveAt(index);
        }
    }
    public Product GetProduct(int index) {
        return _products[index];

    }
    public int GetCountProduct()
    {
        return _products.Count;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = false;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_playerNear && _sellerPanel != null)
        {
            _sellerPanel.SetActive(true);
            _menuPauseButton.SetActive(false);

        }
    }
}
