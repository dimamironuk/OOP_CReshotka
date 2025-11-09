using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class EnemyDeathRelay : MonoBehaviour
{
    public event Action<EnemyDeathRelay> Destroyed;

    private static bool quitting;
    private void OnApplicationQuit() => quitting = true;

    private void OnDestroy()
    {
        if (quitting) return;
        Destroyed?.Invoke(this);
    }
}
