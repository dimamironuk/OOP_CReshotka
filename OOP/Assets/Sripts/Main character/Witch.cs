using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Mainch
{
    void Start()
    {
        Init(80, 15, 0.3f, 2.0f);
    }
    void Update()
    {
        
    }
    public void CastSpell(IDamagable target)
    {
        float spellDamage = baseDMG * 1.5f;
        Debug.Log($"{gameObject.name} ��������� �� �������� ����� ��㳿");
        target.TakeDMG( spellDamage );
    }
}
