using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ChangeField : MonoBehaviour
{
    [Inject] private Match3Stats Match3Stats;
    [Inject] private Board Board;
    [SerializeField] private SimpleButton Button;
    [SerializeField] private Vector2Int FieldChangeAmount;
    void Start()
    {
        Button.OnClick += ChangeFieldSize;
    }
    private void ChangeFieldSize()
    {
        Match3Stats.ChangeFieldSize(FieldChangeAmount);
        Board.UpdateBoard();
    }
}
