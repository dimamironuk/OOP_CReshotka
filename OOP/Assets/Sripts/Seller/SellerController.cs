using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

    private void Update()
    {
        Vector2 touchPos = Vector2.zero;
        bool touchBegan = false;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchBegan = true;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchBegan = touch.phase == TouchPhase.Began;
        }
#endif
       /* if (touchBegan)
        {
            Collider2D hit = Physics2D.OverlapPoint(touchPos);
            if (hit != null && hit.gameObject == gameObject && _playerNear)
            {
                OpenSellerPanel();
            }
        }*/
    }
    private void OnMouseDown()
    {
        if (_playerNear)
        {
            OpenSellerPanel();
        }
    }
    private void OpenSellerPanel()
    {
        if (_sellerPanel != null && _gamePanel != null && _settingsController != null)
        {
            _sellerPanel.SetActive(true);
            _gamePanel.SetActive(false);
            _settingsController.ViewProductSeller(this);
        }
    }
    public void BuyProduct(int index)
    {
        if (index < 0 || index >= _products.Count)
        {
            Debug.Log("Error: Out of list");
            return;
        }
        Product product = _products[index];
        _products.RemoveAt(index);
        Item item = Item.Create(product);
        FindAnyObjectByType<DataBaseItem>().AddItem(item);

        if (_settingsController != null)
        {
            _settingsController.ViewProductSeller(this);
        }
    }
    public void SetSellerPanel(GameObject panel) => _sellerPanel = panel;
    public void SetGamePanel(GameObject panel) => _gamePanel = panel;
    public void SetSellerSettings(SellerSettingsController settings) => _settingsController = settings;
    public void SetName(string name) => _nameSeller = name;
    public void SetId(int id) => this.id = id;
    public int GetId() => id;
    public Product GetProduct(int index) => (index >= 0 && index < _products.Count) ? _products[index] : null;
    public int GetCountProduct() => _products.Count;
    public void ClearProducts() => _products.Clear();
    public void CreateProduct(Product[] products) => _products.AddRange(products);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) _playerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) _playerNear = false;
    }
}
