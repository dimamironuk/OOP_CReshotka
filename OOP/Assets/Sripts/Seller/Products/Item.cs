using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeItem {Bonus, Equipment, Artifact,None };
public class Item : MonoBehaviour
{
    [SerializeField] private string _nameItem = string.Empty;
    [SerializeField] protected string _description = string.Empty;
    [SerializeField] private float _countBonus = 0;
    [SerializeField] private TypeItem _typeItem = TypeItem.None;
    
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
