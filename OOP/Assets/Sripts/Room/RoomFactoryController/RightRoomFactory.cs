using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Right Factory")]
public class RightRoomFactory : RoomFactory
{
    public override GameObject CreateRoom()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
