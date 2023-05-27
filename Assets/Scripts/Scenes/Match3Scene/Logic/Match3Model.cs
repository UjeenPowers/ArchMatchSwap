using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Match3Model
{
    [Inject] private Board Board;
    [Inject] private Match3Stats Match3Stats;
    private Chip[,] Chips = new Chip[0,0];
    public void Initialize()
    {
        UpdateChips();
        SpawnChips();
        Board.OnBoardUpdated += UpdateChips;
    }
    private void UpdateChips()
    {
        Debug.Log("Chips array updated");
        var newChips = new Chip[Board.Cells.GetLength(0),Board.Cells.GetLength(1)];
        for (int i = 0; i < Chips.GetLength(0); i++)
        {
            for (int j = 0; j < Chips.GetLength(1); j++)
            {
                if (i < newChips.GetLength(0) && j < newChips.GetLength(1))
                {
                    newChips[i,j] = Chips[i,j];
                }
                else
                {
                    if (Chips[i,j] != null) Chips[i,j].Disappear();
                }
            }
        }
        Chips = newChips;
    }
    private void SpawnChips()
    {
        Debug.Log($"Chips supposed to spawn: {Match3Stats.ChipsPerTurn}");
        for (int i =0; i < Match3Stats.ChipsPerTurn; i++)
        {
            var col = UnityEngine.Random.Range(0,Chips.GetLength(0));
            Debug.Log($"Col picked: {col}");
            for (int j = 0; j< Chips.GetLength(1); j++)
            {
                Debug.Log(Chips[i,j]);
                if (Chips[i,j] == null) 
                {
                    Debug.Log("Chip created");
                    Chips[i,j] = CreateRandomChip();
                    Chips[i,j].SetCell(Board.Cells[i,j]);
                    break;
                }
            }
        }
    }
    private Chip CreateRandomChip()
    {
        var chip = new Chip();
        chip.Initialize();
        chip.SetRandomChip();
        return chip;
    }
}
