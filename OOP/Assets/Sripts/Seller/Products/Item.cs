using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum TypeItem {Bonus, Equipment, Artifact,None };
public class Item : MonoBehaviour
{
    [SerializeField] private string _nameItem = string.Empty;
    [SerializeField] protected string _description = string.Empty;
    [SerializeField] private float _countBonus = 0;
    [SerializeField] private TypeItem _typeItem = TypeItem.None;
    [SerializeField] private Rarity _rarity = Rarity.Uncommon;
    [SerializeField] private Sprite _sprite;
    public Sprite GetSprite()
    {
        return _sprite;
    }
    static public Item Create(Product product)
    {
        GameObject go = new GameObject(product.GetName()); 
        Item item = go.AddComponent<Item>();             
        item._nameItem = product.GetName();
        item._description = product.GetDescription();
        item._countBonus = product.GetBonus();
        item._typeItem = product.GetTypeItem();
        item._rarity = product.GetRarity();
        item._sprite = product.GetIcon();
        return item;
    }
    public void UseItem(MainCharacter character)
    {
        switch (_typeItem)
        {
            case TypeItem.Bonus:
                {
                    character.AddHealth((int)_countBonus);
                    break;
                }
            case TypeItem.Equipment:
                break;
            case TypeItem.Artifact:
                break;
            case TypeItem.None:
                break;
            default:
                break;
        }
    }
}
