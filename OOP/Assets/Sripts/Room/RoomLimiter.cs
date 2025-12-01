using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomLimiter : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    public int activeSpawners = 0;

    public int maxShops = 2;
    public int maxBoss = 1;

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public void AssignTypes()
    {
        if (rooms.Count == 0) return;

        int bossIndex = Random.Range((int)(rooms.Count * 0.6f), rooms.Count);
        Room bossRoom = rooms[bossIndex];
        BossRoom boss = ReplaceWithType<BossRoom>(bossRoom);

        Transform bCenter = boss.transform.Find("Center");
        boss.posGenerated = bCenter != null ? bCenter.position : Vector3.zero;
        boss._spawnObject = Resources.Load<GameObject>("BossSpawner");


        int shopsToMake = Random.Range(1, 4);

        List<Room> candidates = rooms.Where(r => !(r is BossRoom) && !(r is ShopRoom)).ToList();
        shopsToMake = Mathf.Min(shopsToMake, candidates.Count);

        for (int i = 0; i < shopsToMake; i++)
        {
            int idx = Random.Range(0, candidates.Count);
            ShopRoom shop = ReplaceWithType<ShopRoom>(candidates[idx]);
            shop.OnGenerated();
            candidates.RemoveAt(idx);
        }

        foreach (Room r in new List<Room>(rooms))
        {
            if (r is BossRoom || r is ShopRoom) continue;

            EnemyRoom enemy = ReplaceWithType<EnemyRoom>(r);
            enemy.OnGenerated();
        }
    }

    private T ReplaceWithType<T>(Room oldRoom) where T : Room
    {
        GameObject obj = oldRoom.gameObject;

        rooms.Remove(oldRoom);
        Destroy(oldRoom);

        T newRoom = obj.AddComponent<T>();
        rooms.Add(newRoom);

        return newRoom;
    }
}
