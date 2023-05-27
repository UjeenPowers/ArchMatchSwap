using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class Match3Stats
{
    private Match3BaseStats Match3BaseStats;
    public Vector2Int FieldSize {get; private set;}
    public int ChipsPerTurn {get; private set;}
    public Match3Stats()
    {
        Match3BaseStats = Addressables.LoadAssetAsync<Match3BaseStats>("Match3BaseStats").WaitForCompletion();
        ResetStats();
    }
    private void ResetStats()
    {
        FieldSize = Match3BaseStats.FieldSize;
        ChipsPerTurn = Match3BaseStats.ChipsPerTurn;
    }
    public void ChangeFieldSize(Vector2Int deltaSize)
    {
        FieldSize += deltaSize;
        if (FieldSize.x > 8) 
        {
            FieldSize = new Vector2Int(8,FieldSize.y);
            Debug.LogError("More than 8 columns is currently not allowed");
        }
        if (FieldSize.x < 3) 
        {
            FieldSize = new Vector2Int(3,FieldSize.y);
            Debug.LogError("Less than 3 columns is currently not allowed");
        }
        if (FieldSize.y > 13) 
        {
            FieldSize = new Vector2Int(FieldSize.x,13);
            Debug.LogError("More than 13 rows is currently not allowed");
        }
        if (FieldSize.y < 3) 
        {
            FieldSize = new Vector2Int(FieldSize.x,3);
            Debug.LogError("Less than 3 rows is currently not allowed");
        }
    }
}
