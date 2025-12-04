using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomController : MonoBehaviour
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public Direction direction;

    private RoomVariables _roomVariables;
    public bool isSpawned = false;

    private void Start()
    {
        _roomVariables = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariables>();
        Invoke(nameof(SpawnRoom),0.3f);
        Destroy(gameObject, 0.3f);
    }

    private void SpawnRoom()
    {
        if (isSpawned) return;

        GameObject roomToSpawn = null;

        switch (direction)
        {
            case Direction.Up:
                roomToSpawn = _roomVariables.up[Random.Range(0, _roomVariables.up.Length)];
                break;

            case Direction.Down:
                roomToSpawn = _roomVariables.down[Random.Range(0, _roomVariables.down.Length)];
                break;

            case Direction.Left:
                roomToSpawn = _roomVariables.left[Random.Range(0, _roomVariables.left.Length)];
                break;

            case Direction.Right:
                roomToSpawn = _roomVariables.right[Random.Range(0, _roomVariables.right.Length)];
                break;
        }

        Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);
        isSpawned = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Center")
        {
            Destroy(gameObject);
        }
    }
}
