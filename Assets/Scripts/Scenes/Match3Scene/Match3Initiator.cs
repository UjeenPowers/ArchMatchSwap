using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Match3Initiator : IInitializable
{
    [Inject] private Board Board;
    [Inject] private Match3Model Match3Model;
    public void Initialize()
    {
        Board.Initialize();
        Match3Model.Initialize();
    }
}
