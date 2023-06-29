using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Match3Model
{
    [Inject] private Board Board;
    [Inject] private Match3Stats Match3Stats;
    [Inject] private InputController InputController;
    [Inject] private ActionSystem ActionSystem;
    [Inject] private DiContainer DiContainer;
    [Inject] private AnimationChainer AnimationChainer;
    private Chip[,] Chips = new Chip[0,0];
    public void Initialize()
    {
        UpdateChips();
        SpawnChips();
        Board.OnBoardUpdated += UpdateChips;
    }
    private void UpdateChips()
    {
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
        for (int i =0; i < Match3Stats.ChipsPerTurn; i++)
        {
            var col = UnityEngine.Random.Range(0,Chips.GetLength(0));
            for (int j = 0; j< Chips.GetLength(1); j++)
            {
                if (Chips[col,j] == null) 
                {
                    Chips[col,j] = CreateRandomChip();
                    Chips[col,j].SetCell(Board.Cells[col,j]);
                    break;
                }
            }
        }
    }
    private Chip CreateRandomChip()
    {
        var chip = new Chip();
        DiContainer.Inject(chip);
        chip.Initialize();
        chip.SetRandomChip();
        return chip;
    }
    public void Swipe(Vector2Int pos, Vector2Int swipe)
    {
        InputController.LockInput();
        Vector2Int finalPos = (pos + swipe);
        if (finalPos.x >= 0 && finalPos.y >= 0 && finalPos.x < Chips.GetLength(0) && finalPos.y < Chips.GetLength(1))
        {
            if (Chips[finalPos.x,finalPos.y] == null)
            {
                if (swipe != Vector2Int.up)
                {
                    Chips[pos.x, pos.y].AnimateSwipe(Board.Cells[finalPos.x,finalPos.y]);
                    Chips[finalPos.x,finalPos.y] = Chips[pos.x, pos.y];
                    Chips[pos.x, pos.y] = null;
                }
            }
            else
            {
                Chips[pos.x, pos.y].AnimateSwipe(Board.Cells[finalPos.x,finalPos.y]);
                Chips[finalPos.x,finalPos.y].AnimateSwipe(Board.Cells[pos.x, pos.y]);
                var temp = Chips[pos.x, pos.y];
                Chips[pos.x, pos.y] = Chips[finalPos.x,finalPos.y];
                Chips[finalPos.x,finalPos.y] = temp;
            }
            Fall();
            // SpawnChips();
        }
        InputController.UnlockInput();
    }
    private void Fall()
    {
        AnimationChainer.AddBreak();
        Debug.Log("Fall");
        for (int width =0; width < Chips.GetLength(0); width++)
        {
            int fall = 0;
            for (int heights = 0; heights < Chips.GetLength(1); heights++)
            {
                if (Chips[width,heights] == null) 
                {
                    fall++;
                }
                if (Chips[width,heights] != null && fall != 0)
                {
                    Debug.Log("Chip supposed to fall");
                    Chips[width,heights-fall] = Chips[width,heights];
                    Chips[width,heights] = null;
                    Chips[width,heights-fall].AnimateFall(Board.Cells[width, heights-fall], () => ActionSystem.FinishAction());
                }
            }
        }
    }
    private void Match()
    {

    }
}
