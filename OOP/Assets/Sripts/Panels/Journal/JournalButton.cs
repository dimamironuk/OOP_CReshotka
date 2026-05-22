using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalButton : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject virtualJoystick;

    public void Toggle()
    {
        if (journalPanel == null) return;
        bool isActive = !journalPanel.activeSelf;

        journalPanel.SetActive(isActive);
        if (virtualJoystick != null)
        {
            virtualJoystick.SetActive(!isActive);
        }
        if (isActive)
        {
            JournalManager manager = GetComponent<JournalManager>();
            if (manager != null) manager.OpenQuestsSection();
        }
    }
}
