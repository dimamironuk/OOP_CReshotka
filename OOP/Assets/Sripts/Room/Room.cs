using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType { Enemy, Boss, Seller, None}
    public RoomType roomType;
}
