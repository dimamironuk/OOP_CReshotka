using UnityEngine;
using System;

public class DailyQuestManager : MonoBehaviour
{
    public QuestDatabase database;
    private const string LastQuestDateKey = "LastCompletedQuestDate";
    public bool IsQuestCompletedToday()
    {
        string lastDate = PlayerPrefs.GetString(LastQuestDateKey, "");
        string todayDate = DateTime.Today.ToString("yyyyMMdd");
         
        return lastDate == todayDate;
    }

    public void MarkQuestAsCompleted()
    {
        string todayDate = DateTime.Today.ToString("yyyyMMdd");
        PlayerPrefs.SetString(LastQuestDateKey, todayDate);
        PlayerPrefs.Save();
    }

    public QuestData GetTodayQuest()
    {
        if (database == null || database.allPossibleQuests.Count == 0) return null;

        DateTime today = DateTime.Today;
        int dateSeed = today.Year * 10000 + today.Month * 100 + today.Day;

        UnityEngine.Random.InitState(dateSeed);
        int randomIndex = UnityEngine.Random.Range(0, database.allPossibleQuests.Count);

        return database.allPossibleQuests[randomIndex];
    }
}