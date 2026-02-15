using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
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
    }
}
