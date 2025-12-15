using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerationProducts : MonoBehaviour
{
    [SerializeField] private List<Product> products;

    private void Awake()
    {
        foreach (Product product in Resources.LoadAll<Product>("Products/")) {
            products.Add(product);
        }
    }
    private Rarity RandomRarity()
    {
        int[] chances = new int[] { 30, 25, 15, 10, 8, 5 };
        int total = 0;
        foreach (int chance in chances)
            total += chance;

        int roll = Random.Range(0, total); 
        int cumulative = 0;

        for (int i = 0; i < chances.Length; i++)
        {
            cumulative += chances[i];
            if (roll < cumulative)
                return (Rarity)i;
        }
        return Rarity.Common;
    }
    public void GeneratorSellerProducts(SellerController seller)
    {
        if(this.products.Count < 9) return;
        int countProducts = Random.Range(2, 10);
        Product[] products = new Product[countProducts];
        for (int i = 0; i < countProducts; i++) {
            Product product = Instantiate(this.products[Random.Range(0, this.products.Count)]);
            product.GetComponent<SpriteRenderer>().enabled = false;
            product.SetIdSeller(seller.GetId());
            product.SetRarity(RandomRarity());
            products[i] = product;
        }
        seller.CreateProduct(products);
    }
}
