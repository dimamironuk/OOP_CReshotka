using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Core Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextSentenceButton;

    [Header("Choice UI")]
    [SerializeField] private GameObject choiceButtonContainer;
    [SerializeField] private Button[] choiceButtons;

    [Header("Portrait UI")]
    [SerializeField] private Image npcPortrait;

    [Header("Input Control")]
    [SerializeField] private GameObject virtualJoystick;

    [Header("Daily Quest Support")]
    [SerializeField] private DailyQuestManager dailyManager;

    private Queue<string> sentenceQueue;
    private Dialogue currentDialogue;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private string fullCurrentSentence = "";

    private void Start()
    {
        sentenceQueue = new Queue<string>();
        dialoguePanel.SetActive(false);
        if (choiceButtonContainer != null) choiceButtonContainer.SetActive(false);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => HandleChoice(index));
        }

        if (nextSentenceButton != null)
            nextSentenceButton.onClick.AddListener(OnScreenClick);

        if (dailyManager == null) dailyManager = FindObjectOfType<DailyQuestManager>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        dialoguePanel.SetActive(true);
        npcNameText.text = dialogue.npcName;

        if (npcPortrait != null && dialogue.portraitSprite != null)
        {
            npcPortrait.sprite = dialogue.portraitSprite;
            npcPortrait.gameObject.SetActive(true);
        }
        else if (npcPortrait != null)
        {
            npcPortrait.gameObject.SetActive(false);
        }
        if (virtualJoystick != null) virtualJoystick.SetActive(false);

        sentenceQueue.Clear();
        foreach (string sentence in dialogue.sentences)
            sentenceQueue.Enqueue(sentence);

        choiceButtonContainer.SetActive(false);
        nextSentenceButton.gameObject.SetActive(true);

        DisplayNextSentence();
    }

    private void OnScreenClick()
    {
        if (isTyping)
            CompleteSentenceInstantly();
        else
            DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentenceQueue.Count == 0)
        {
            FinishSentences();
            return;
        }

        string sentence = sentenceQueue.Dequeue();
        fullCurrentSentence = sentence;

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    private void CompleteSentenceInstantly()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueText.text = fullCurrentSentence;
        isTyping = false;
    }

    private void FinishSentences()
    {
        if (currentDialogue.choiceOptions != null && currentDialogue.choiceOptions.Length > 0)
        {
            nextSentenceButton.gameObject.SetActive(false);
            ShowChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }

    public void HandleChoice(int choiceIndex)
    {
        if (currentDialogue == null || choiceIndex >= currentDialogue.choiceOptions.Length) return;

        DialogueOption chosenOption = currentDialogue.choiceOptions[choiceIndex];
        choiceButtonContainer.SetActive(false);

        if (chosenOption.isDailyQuestStarter && dailyManager != null)
        {
            dailyManager.MarkQuestAsCompleted();
        }

        if (chosenOption.nextDialogue != null)
        {
            StartDialogue(chosenOption.nextDialogue);
        }
        else if (!string.IsNullOrEmpty(chosenOption.sceneToLoad) && !chosenOption.executeResultSentenceBeforeLoad)
        {
            LoadScene(chosenOption.sceneToLoad);
        }
        else
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            StartCoroutine(TypeFinalResult(chosenOption));
        }
    }

    IEnumerator TypeFinalResult(DialogueOption option)
    {
        yield return StartCoroutine(TypeSentence(option.resultSentence));
        yield return new WaitForSeconds(1.5f);

        if (!string.IsNullOrEmpty(option.sceneToLoad))
            LoadScene(option.sceneToLoad);
        else
            EndDialogue();
    }

    private void ShowChoices()
    {
        int optionsCount = currentDialogue.choiceOptions.Length;
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < optionsCount)
            {
                TextMeshProUGUI btnText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null) btnText.text = currentDialogue.choiceOptions[i].buttonText;
                choiceButtons[i].gameObject.SetActive(true);
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
        choiceButtonContainer.SetActive(true);
    }

    private void LoadScene(string sceneName)
    {
        if (virtualJoystick != null) virtualJoystick.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        if (virtualJoystick != null) virtualJoystick.SetActive(true);
        currentDialogue = null;
    }
}