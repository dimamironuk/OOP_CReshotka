using UnityEngine;
using UnityEngine.EventSystems;

public class NpcDialogue : MonoBehaviour, IPointerClickHandler
{
    [Header("Role Settings")]
    [SerializeField] private bool isQuestGiver = false;

    [Header("Dialogue Assets")]
    [SerializeField] private Dialogue mainGreeting;
    [SerializeField] private Dialogue alreadyDoneDialogue;

    [Header("Dependencies")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DailyQuestManager dailyManager;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";

    private bool playerNear = false;

    private void Start()
    {
        if (dialogueManager == null) dialogueManager = FindObjectOfType<DialogueManager>();
        if (isQuestGiver && dailyManager == null)
            dailyManager = FindObjectOfType<DailyQuestManager>();
        if (dialogueManager == null) Debug.LogError($"DialogueManager not found: {gameObject.name}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (playerNear)
        {
            HandleInteraction();
        }
        else
        {
            Debug.Log("Too far to talk!");
        }
    }

    private void HandleInteraction()
    {
        if (dialogueManager == null) return;

        if (isQuestGiver && dailyManager != null)
        {
            QuestData todayQuest = dailyManager.GetTodayQuest();

            if (dailyManager.IsQuestCompletedToday())
            {
                if (mainGreeting.choiceOptions.Length > 0)
                {
                    mainGreeting.choiceOptions[0].buttonText = "Again, what today`s quest?";
                    mainGreeting.choiceOptions[0].nextDialogue = alreadyDoneDialogue;
                    mainGreeting.choiceOptions[0].isDailyQuestStarter = false;
                }
            }
            else if (todayQuest != null)
            {
                if (mainGreeting.choiceOptions.Length > 0)
                {
                    mainGreeting.choiceOptions[0].buttonText = "What today`s quest?";
                    mainGreeting.choiceOptions[0].nextDialogue = todayQuest.questDialogue;
                    mainGreeting.choiceOptions[0].isDailyQuestStarter = true;
                }
            }
        }
        dialogueManager.StartDialogue(mainGreeting);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNear = false;
        }
    }
}