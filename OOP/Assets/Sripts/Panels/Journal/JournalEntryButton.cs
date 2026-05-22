using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Journal/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    [TextArea(10, 20)] public string bio;
}

public class JournalEntryButton : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    private JournalManager manager;
    private string description;
    private string title;

    public void Setup(string btnTitle, string btnDescription, JournalManager mgr)
    {
        title = btnTitle;
        description = btnDescription;
        manager = mgr;
        titleText.text = title;
    }

    public void OnClick()
    {
        manager.ShowFullText(title, description);
    }
}