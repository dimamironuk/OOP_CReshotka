using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueOption
{
    public string buttonText;
    [TextArea(3, 10)]
    public string resultSentence;
    public bool isDailyQuestStarter;
    public Dialogue nextDialogue;

    [Header("Scene Transition")]
    public string sceneToLoad;
    public bool executeResultSentenceBeforeLoad = true;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string npcName;
    public Sprite portraitSprite;
    [TextArea(3, 10)] public string[] sentences;
    public DialogueOption[] choiceOptions;
}