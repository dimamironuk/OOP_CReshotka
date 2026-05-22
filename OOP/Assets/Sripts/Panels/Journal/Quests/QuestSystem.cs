using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Dialogue System/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questID;
    public string questName;
    public Dialogue questDialogue;
    [TextArea(10, 20)] public string info;
}

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "Dialogue System/Quest Database")]
public class QuestDatabase : ScriptableObject
{
    public List<QuestData> allPossibleQuests;
}