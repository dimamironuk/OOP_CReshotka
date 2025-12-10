using UnityEngine;

public abstract class RoomFactory : ScriptableObject
{
    public GameObject[] prefabs;
    public abstract GameObject CreateRoom();
}
