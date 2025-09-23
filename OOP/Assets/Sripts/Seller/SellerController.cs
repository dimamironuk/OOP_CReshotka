using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerController : MonoBehaviour
{
    [SerializeField] private string _nameSeller;
    [SerializeField] private List<Product> _products;
    private void Start()
    {
        _products = new List<Product>();

        Product potion = CreateProduct("Мале зілля лікування", 50, "Відновлює 20 HP");
        Product sword = CreateProduct("Залізний меч", 120, "Базова зброя ближнього бою");

        _products.Add(potion);
        _products.Add(sword);

        foreach (var p in _products)
        {
            p.GetInfo();
        }
    }

    private Product CreateProduct(string name, int price, string description)
    {
        GameObject go = new GameObject(name);
        Product product = go.AddComponent<Product>();
        product.Init(name, price, description);
        return product;
    }
}
