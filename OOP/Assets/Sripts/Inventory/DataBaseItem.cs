using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseItem : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private List<Button> buttons = new List<Button>();

    private void Start()
    {
        for (int i = 0; i < items.Count; i++) {
            buttons[i].interactable = true;
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        buttons[items.Count - 1].interactable = true;
    }
    public void RemoveItem(Item item) { 
        buttons[items.Count - 1].interactable = false;
        items.Remove(item);

    }
}
