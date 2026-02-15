using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public GameObject menuPausePanel;
    public GameObject settingsPanel;

    public void OpenSettings()
    {
        menuPausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPausePanel.SetActive(true);
    }
}
