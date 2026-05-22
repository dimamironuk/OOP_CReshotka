using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
<<<<<<< HEAD

    public Vector3 posGenerated = Vector3.zero;
    public GameObject _spawnObject = null;
    public virtual void OnGenerated() { }
    public virtual void OnPlayerEnter() { }
}

public class EnemyRoom : Room
{
    public override void OnGenerated()
    {
        Transform center = transform.Find("Center");
        posGenerated = center != null ? center.position : Vector3.zero;

        _spawnObject = Resources.Load<GameObject>("spawner");
        _spawnObject.GetComponent<SpawnerController>().enemyPrefab = Resources.Load<GameObject>("enemy_1");
        Instantiate(_spawnObject, posGenerated, Quaternion.identity, transform);
    }
}
public class ShopRoom : Room
{
    public override void OnGenerated()
    {
        Transform center = transform.Find("Center");
        posGenerated = center != null ? center.position : Vector3.zero;

        GameObject sellerInstance = Instantiate(Resources.Load<GameObject>("Seller"), posGenerated, Quaternion.identity, transform);
        SellerController sellerSettings = sellerInstance.GetComponent<SellerController>();
        Transform canvas = GameObject.Find("Canvas")?.transform;

        sellerSettings._sellerPanel = FindChildByName(canvas, "SellerPanel");
        sellerSettings._menuPauseButton = GameObject.Find("B_MenuPause");
    }
    private GameObject FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == name)
                return child.gameObject;
        }
        return null;
    }

}
public class BossRoom : Room
{
    public override void OnGenerated()
    {
        Instantiate(_spawnObject, posGenerated, Quaternion.identity, transform);
=======
    public enum RoomType { Enemy, Boss, Seller, None}
    public RoomType roomType;
    public GameObject roomSpawnObj = null;
    public Vector3 spawnPoint = Vector3.zero;

    public void SpawnObjRoomType()
    {
        roomSpawnObj = Instantiate(roomSpawnObj,spawnPoint,Quaternion.identity);
        if (RoomType.Seller == roomType)
        {
            GenerationProducts genProduct = GameObject.FindGameObjectWithTag("GeneratorProducts").GetComponent<GenerationProducts>();
            genProduct.GeneratorSellerProducts(roomSpawnObj.GetComponent<SellerController>());
        }
>>>>>>> main
    }
}
