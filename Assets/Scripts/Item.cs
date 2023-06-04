using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Item : MonoBehaviour
{
    public TileBase tile;
    public Sprite icon;
    public string nameItem;
    public int id;
    public int countItem;
    public bool isRemovable;
    public bool isDroped;
    public UnityEvent customEvent;
}
