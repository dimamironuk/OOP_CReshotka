using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Mythic,
    Legendary
}

public interface IItmes
{
    Rarity ItemRarity { get; set; }
    Product GetItem();
}
