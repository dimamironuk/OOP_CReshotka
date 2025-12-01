using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomFactory : ScriptableObject
{
    public GameObject[] prefabs;
    public abstract GameObject CreateRoom();
}
