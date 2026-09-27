namespace RpsTournament.Core;

/// <summary>Ühe mängitud vooru andmed.</summary>
public readonly struct GameRound
{
    public GameRound(int number, Move playerMove, Move computerMove, RoundResult result)
    {
        Number = number;
        PlayerMove = playerMove;
        ComputerMove = computerMove;
        Result = result;
    }

    public int Number { get; }
    public Move PlayerMove { get; }
    public Move ComputerMove { get; }
    public RoundResult Result { get; }

    public string PlayerMoveText => GameLogic.MoveToText(PlayerMove);
    public string ComputerMoveText => GameLogic.MoveToText(ComputerMove);
    public string ResultText => GameLogic.ResultToText(Result);
}
