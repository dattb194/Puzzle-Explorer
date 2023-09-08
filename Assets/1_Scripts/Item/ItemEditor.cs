using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEditor : MonoBehaviour
{
    public TypeItem type;
}
public enum TypeItem
{ 
    player,
    tree,
    sphere_stone,
    land_face,
    lane_underground,
    gold_barrel,
    lava,
    win_point,
    enemy_1
}
