using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    [SerializeField] private GameObject menuPausePanel;
    [SerializeField] private GameObject settingsPanel;

    public void Open()
    {
        menuPausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Close()
    {
        settingsPanel.SetActive(false);
        menuPausePanel.SetActive(true);
    }
}