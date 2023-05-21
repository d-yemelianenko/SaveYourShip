using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public string nameItem;
    public int id;
    public int countItem;
    public bool isRemovable;
    public bool isDroped;
    public Sprite icon;
    public UnityEvent customEvent;
}
