using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Down Factory")]
public class DownRoomFactory : RoomFactory
{
    public override GameObject CreateRoom()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
