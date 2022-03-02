using System;
using UnityEngine;

/// <summary>
/// This will share logic for any unit on the field. Could be friend or foe, controlled or not.
/// Things like taking damage, dying, animation triggers etc
/// </summary>
public class UnitBase : MonoBehaviour {
    public Tile OccupedTile;
    public UnitName UnitName;
}

[Serializable]
public enum UnitName
{
    Fire, // 暫定以後加新的把這刪掉
    Green,
    Bomb,
}
