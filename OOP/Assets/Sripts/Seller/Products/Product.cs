using System;
using UnityEngine;

public class Product : MonoBehaviour, IItmes
{
    [SerializeField] private string _name;
    [SerializeField] private int _idSeller;
    [SerializeField] private int _price;
    [SerializeField] private string _description;
    [SerializeField] private int _bonus;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Rarity _rarity;
    [SerializeField] private TypeItem _typeProduct;
    public Rarity ItemRarity { get => _rarity; set => _rarity = value; }
    private void Awake()
    {
        _icon = GetComponent<SpriteRenderer>().sprite;
    }
    public void SetBonus(int bonus)
    {
        _bonus = bonus;
    }
    public void SetRarity(Rarity rarity)
    {
        _rarity = rarity;
    }
    public void SetIdSeller(int id)
    {
        _idSeller = id;
    }
    public Product GetItem()
    {
        return this;
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
    public int GetBonus()
    {
        return _bonus;
    }
    public string GetDescription()
    {
        return _description;
    }
    public Rarity GetRarity()
    {
        return _rarity;
    }
    public TypeItem GetTypeItem()
    {
        return _typeProduct;
    }
    public void GetInfo()
    {
        Debug.Log($"Назва: {_name}\nЦіна: {_price}\nОпис: {_description}\n-------------------------");
    }
    public Sprite GetIcon() { 
        return _icon;
    }
}
