using UnityEngine;

public class Product : MonoBehaviour
{
    private string _name;
    private int _price;
    private string _description;
    private Sprite _icon;

    public void Init(string name, int price, string description, Sprite icon = null)
    {
        _name = name;
        _price = price;
        _description = description;
        _icon = icon;
    }

    public void GetInfo()
    {
        Debug.Log($"Назва: {_name}\nЦіна: {_price}\nОпис: {_description}\n-------------------------");
    }
}
