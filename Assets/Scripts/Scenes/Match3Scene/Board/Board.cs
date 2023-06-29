using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Board
{
    [Inject] private Match3Stats Match3Stats;
    [Inject] private Match3Model Match3Model;
    private Transform BoardAnchor;
    public Cell[,] Cells {get; private set;} = new Cell[0,0];
    public Action OnBoardUpdated;
    public Board(Transform anchor)
    {
        BoardAnchor = anchor;
    }
    public void Initialize()
    {
        UpdateBoard();
    }
    public void UpdateBoard()
    {
        UpdateBoardSize();
        OnBoardUpdated?.Invoke();
    }
    private void UpdateBoardSize()
    {
        if (Match3Stats.FieldSize.x == Cells.GetLength(0) && Match3Stats.FieldSize.y == Cells.GetLength(1)) return;
        int deltaX = Match3Stats.FieldSize.x - Cells.GetLength(0);
        int deltaY = Match3Stats.FieldSize.y - Cells.GetLength(1);

        var newArray = new Cell[Cells.GetLength(0) + deltaX, Cells.GetLength(1) + deltaY];

        //generate new aray
        for (int i = 0; i < newArray.GetLength(0); i++)
        {
            for (int j = 0; j < newArray.GetLength(1); j++)
            {
                if (i < Cells.GetLength(0) && j < Cells.GetLength(1))
                {
                    newArray[i, j] = Cells[i, j];
                    newArray[i, j].UpdateSize(new Vector2Int(i,j), newArray.GetLength(0),newArray.GetLength(1));
                }
                else
                {
                    newArray[i, j] = new Cell(Match3Model);
                    newArray[i, j].Initialize(BoardAnchor);
                    newArray[i, j].Appear(new Vector2Int(i,j),newArray.GetLength(0),newArray.GetLength(1));
                }
            }
        }

        //get rid of excessive cells form prev array
        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            for (int j = 0; j < Cells.GetLength(1); j++)
            {
                if (i >= newArray.GetLength(0) || j >= newArray.GetLength(1)) Cells[i,j].Disappear();
            }
        }

        Cells = newArray;
    }
}
