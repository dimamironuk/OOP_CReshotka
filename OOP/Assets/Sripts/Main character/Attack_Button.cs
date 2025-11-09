using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Button : MonoBehaviour
{
    public Mainch mainCharacter;
    void Start()
    {
        if (mainCharacter == null)
        {
            Debug.LogError("ERROR: Mainch object not set for attack button!");
        }
    }
    public void OnAttackButtonClick()
    {
        if (mainCharacter == null)
        {
            Debug.LogWarning("Атака неможлива: Персонаж не встановлений.");
            return;
        }
        mainCharacter.PerformCooldownAttack();
    }
}
