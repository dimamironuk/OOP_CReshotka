using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Left Factory")]
public class LeftRoomFactory : RoomFactory
{
    public override GameObject CreateRoom()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
