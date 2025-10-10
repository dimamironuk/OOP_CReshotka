using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Mainch
{
    protected override void Start()
    {
        base.Start();
        Init(80, 15, 0.3f, 2.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }
    public void CastSpell(IDamagable target)
    {
        float spellDamage = baseDMG * 1.5f;
        Debug.Log($"{gameObject.name} атакувала за допомоги чорної магії");
        target.TakeDMG( spellDamage );
    }
}
