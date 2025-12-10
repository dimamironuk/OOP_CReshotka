using System.Collections.Generic;
using UnityEngine;

public class RoomLimiter : MonoBehaviour
{
    private Dictionary<int, Room> rooms;
    public int activeSpawners = 0;
    public void AddRoom(Room room)
    {
        rooms.Add(rooms.Count, room);
    }
    public void AssignTypes()
    {
        /*foreach (Room room in rooms) { 
            if(room.roomType == Room.RoomType.None)
            {

            }
        }*/
    }
}
