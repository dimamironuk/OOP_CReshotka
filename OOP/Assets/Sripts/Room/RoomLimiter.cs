using System.Collections.Generic;
using UnityEngine;

public class RoomLimiter : MonoBehaviour
{
    [SerializeField] private GameObject _sellerPanel;
    private Dictionary<int, Room> rooms;
    public int activeSpawners = 0;
    private void Awake()
    {
        rooms = new Dictionary<int, Room>();
    }

    public void AddRoom(Room room)
    {
        rooms.Add(rooms.Count, room);
    }

    public void AssignTypes()
    {
        List<int> freeIndices = new List<int>(rooms.Keys);

        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            int rand = Random.Range(0, freeIndices.Count);
            int index = freeIndices[rand];
            freeIndices.RemoveAt(rand);
            rooms[index].roomType = Room.RoomType.Seller;
        }

        List<int> bossRange = new List<int>();
        foreach (int k in freeIndices)
        {
            if (k >= rooms.Count / 2)
                bossRange.Add(k);
        }

        if (bossRange.Count == 0)
            bossRange.AddRange(freeIndices);

        int bossIndex = bossRange[Random.Range(0, bossRange.Count)];
        freeIndices.Remove(bossIndex);
        rooms[bossIndex].roomType = Room.RoomType.Boss;

        foreach (int k in freeIndices)
            rooms[k].roomType = Room.RoomType.Enemy;

        SpawnObjTypes();
    }

    void SpawnObjTypes()
    {
        int countSeller = 0;
        foreach (var item in rooms)
        {
            Room room = item.Value;
            GameObject objSpawn = null;
            
            switch (room.roomType)
            {
                case Room.RoomType.Enemy:
                    objSpawn = Resources.Load<GameObject>("spawner");
                    objSpawn.GetComponent<SpawnerController>().enemyPrefab = Resources.Load<GameObject>("enemy_1");
                    break;
                case Room.RoomType.Boss:
                    objSpawn = Resources.Load<GameObject>("Boss");
                    break;
                case Room.RoomType.Seller:
                    objSpawn = Resources.Load<GameObject>("Seller");
                    objSpawn.GetComponent<SellerController>()?.SetId(countSeller++);
                    objSpawn.GetComponent<SellerController>()?.SetName("Vova abibas");
                    objSpawn.GetComponent<SellerController>().SetSellerPanel(_sellerPanel);
                    break;
                case Room.RoomType.None:
                    room.roomType = Room.RoomType.Enemy;
                    objSpawn = Resources.Load<GameObject>("spawner");
                    objSpawn.GetComponent<SpawnerController>().enemyPrefab = Resources.Load<GameObject>("enemy_1");
                    break;
            }
            room.roomSpawnObj = objSpawn;
            Transform center = room.transform.Find("Center");
            if (center != null && room.roomSpawnObj != null)
            {
                room.spawnPoint = center.position;
                room.SpawnObjRoomType();
            }
        }
    }
}
