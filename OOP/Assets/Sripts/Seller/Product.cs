using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    private void Awake()
    {
        _icon = GetComponent<SpriteRenderer>().sprite;
    }
    public void Init(string name, int price, string description, Sprite icon = null)
    {
        _name = name;
        _price = price;
        _description = description;
        _icon = icon;
    }
    public Sprite GetImage()
    {
        return _icon;
    }
    public string GetName()
    {
        return _name;
    }
    public int GetPrice()
    {
        return _price;
    }
    public string GetDescription()
    {
        return _description;
    }
    public void GetInfo()
    {
        Debug.Log($"Назва: {_name}\nЦіна: {_price}\nОпис: {_description}\n-------------------------");
    }
}
