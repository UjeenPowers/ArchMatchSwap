using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Match3BaseStats", menuName = "ScriptableObjects/Match3BaseStats", order = 1)]
public class Match3BaseStats : ScriptableObject
{
    public Vector2Int FieldSize;
    public int ChipsPerTurn = 4;
}
