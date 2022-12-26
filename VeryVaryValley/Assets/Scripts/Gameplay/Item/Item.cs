using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool throwable;
    public bool usable;
}
