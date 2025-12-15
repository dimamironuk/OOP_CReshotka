using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathRelay : MonoBehaviour
{
    private readonly List<EnemyObserver> observers = new();

    public void Subscribe(EnemyObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void Unsubscribe(EnemyObserver observer)
    {
        observers.Remove(observer);
    }

    private void OnDestroy()
    {
        foreach (var observer in observers)
        {
            observer.OnEnemyDead();
        }
    }
}