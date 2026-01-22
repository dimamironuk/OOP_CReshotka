using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerController : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string _nameSeller;
    [SerializeField] private List<Product> _products;
    [SerializeField] private GameObject _sellerPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private SellerSettingsController _settingsController;
    private bool _playerNear = false;
    public void SetSellerPanel(GameObject sellerPanel)
    {
        _sellerPanel = sellerPanel;
    }
    public void SetGamePanel(GameObject gamePanel)
    {
        _gamePanel = gamePanel;
    }
    public void SetSellerSettings(SellerSettingsController settingsController)
    {
        _settingsController = settingsController;
    }
    public void SetName(string name)
    {
        _nameSeller = name;
    }
    public void SetId(int id)
    {
        this.id = id;
    }
    public int GetId()
    {
        return id;
    }
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
    public Product GetProduct(int index)
    {
        if (index < 0 || index >= _products.Count)
        {
            return null;
        }
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
            OnPointerClick();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = false;
        }
    }
    public void OnPointerClick()
    {
            _sellerPanel.SetActive(true);
            _gamePanel.SetActive(false);
            _settingsController.ViewProductSeller(this);
        if (_playerNear && _sellerPanel != null)
        {
        }
    }
}
