using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject journalPanel;
    public Transform listContainer;
    public GameObject buttonPrefab;

    [Header("Right Panel References")]
    public TextMeshProUGUI mainTitleText;
    public TextMeshProUGUI mainContentText;

    [Header("Tab Buttons")]
    public Button questTabButton;
    public Button characterTabButton;

    [Header("Data")]
    public DailyQuestManager dailyManager;
    public List<CharacterData> allCharacters;

    [Header("Joystick")]
    [SerializeField] private GameObject virtualJoystick;

    private void Start()
    {
        if (journalPanel != null) journalPanel.SetActive(false);
    }

    public void OpenQuestsSection()
    {
        ClearList();
        mainTitleText.text = "QUESTS";

        if (dailyManager == null) dailyManager = FindObjectOfType<DailyQuestManager>();

        if (dailyManager != null && dailyManager.IsQuestCompletedToday())
        {
            QuestData quest = dailyManager.GetTodayQuest();
            if (quest != null)
            {
                CreateListButton(quest.questName, quest.info);
                mainContentText.text = "Choose quest to see details";
            }
        }
        else
        {
            mainContentText.text = "No active quests!";
        }
    }

    public void OpenCharactersSection()
    {
        ClearList();
        mainTitleText.text = "Characters";
        mainContentText.text = "Choose charachter to see details";

        if (allCharacters == null || allCharacters.Count == 0) return;

        foreach (var character in allCharacters)
        {
            if (character != null)
                CreateListButton(character.characterName, character.bio);
        }
    }

    private void CreateListButton(string title, string content)
    {
        if (buttonPrefab == null || listContainer == null) return;

        GameObject newBtn = Instantiate(buttonPrefab);
        newBtn.transform.SetParent(listContainer, false);

        JournalEntryButton btnScript = newBtn.GetComponent<JournalEntryButton>();
        if (btnScript != null)
        {
            btnScript.Setup(title, content, this);
        }
    }

    private void ClearList()
    {
        foreach (Transform child in listContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowFullText(string title, string content)
    {
        mainTitleText.text = title;
        mainContentText.text = content;
    }

    public void ToggleJournal()
    {
        bool state = !journalPanel.activeSelf;
        journalPanel.SetActive(state);
        //if (virtualJoystick != null) virtualJoystick.SetActive(false);

        if (state) OpenQuestsSection();
    }
}