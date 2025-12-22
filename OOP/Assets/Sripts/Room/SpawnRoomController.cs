using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomController : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    public Direction direction;

    public RoomFactory upFactory;
    public RoomFactory downFactory;
    public RoomFactory leftFactory;
    public RoomFactory rightFactory;
    private RoomLimiter roomLimiter;

    private bool isTouch = false;
    private void Start()
    {
        roomLimiter = FindAnyObjectByType<RoomLimiter>();
        roomLimiter.activeSpawners++;
        Invoke(nameof(SpawnRoom), 0.2f);
        Destroy(gameObject, 0.5f);
    }
    private void SpawnRoom()
    {
        RoomFactory factory = direction switch
        {
            Direction.Up => upFactory,
            Direction.Down => downFactory,
            Direction.Left => leftFactory,
            Direction.Right => rightFactory,
            _ => null
        };
        if (factory == null) return;

        if (!isTouch)
        {
            GameObject roomInstance = Instantiate(factory.CreateRoom(), transform.position, Quaternion.identity);
            Room room = roomInstance.AddComponent<Room>();
            room.roomType = Room.RoomType.None;
            roomLimiter.AddRoom(room);
        }
    }
    private void OnDestroy()
    {
        roomLimiter.activeSpawners--;
        if (roomLimiter.activeSpawners == 0)
        {
            roomLimiter.AssignTypes();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Center"))
        {
            isTouch = true;
            Destroy(gameObject);
        }
    }
}