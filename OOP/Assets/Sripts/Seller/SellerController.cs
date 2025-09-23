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

        Product potion = CreateProduct("���� ���� ��������", 50, "³������� 20 HP");
        Product sword = CreateProduct("������� ���", 120, "������ ����� ��������� ���");

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
