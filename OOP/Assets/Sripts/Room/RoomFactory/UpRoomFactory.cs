using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Up Factory")]
public class UpRoomFactory : RoomFactory
{
    public override GameObject CreateRoom()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
