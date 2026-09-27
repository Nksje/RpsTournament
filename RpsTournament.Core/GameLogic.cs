namespace RpsTournament.Core;

/// <summary>Kivi-paber-käärid mänguloogika.</summary>
public static class GameLogic
{
    public const int RoundsPerTournament = 5;
    public const int MinNameLength = 2;
    public const int MaxNameLength = 30;

    private static readonly Move[] AllMoves = { Move.Rock, Move.Paper, Move.Scissors };
    private static readonly Random Random = new();

    /// <summary>Valib arvutile juhusliku käigu.</summary>
    public static Move GetRandomMove()
    {
        return AllMoves[Random.Next(AllMoves.Length)];
    }

    /// <summary>Määrab vooru tulemuse mängija vaatenurgast.</summary>
    public static RoundResult DetermineResult(Move player, Move computer)
    {
        if (player == computer)
        {
            return RoundResult.Draw;
        }

        bool playerWins = player switch
        {
            Move.Rock => computer == Move.Scissors,
            Move.Paper => computer == Move.Rock,
            Move.Scissors => computer == Move.Paper,
            _ => false
        };

        return playerWins ? RoundResult.Win : RoundResult.Lose;
    }

    /// <summary>Kontrollib, kas nime pikkus on lubatud vahemikus.</summary>
    public static bool IsValidName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        int length = name.Trim().Length;
        return length >= MinNameLength && length <= MaxNameLength;
    }

    public static string MoveToText(Move move)
    {
        switch (move)
        {
            case Move.Rock:
                return "Kivi";
            case Move.Paper:
                return "Paber";
            case Move.Scissors:
                return "Käärid";
            default:
                return move.ToString();
        }
    }

    public static string ResultToText(RoundResult result)
    {
        switch (result)
        {
            case RoundResult.Win:
                return "Võit";
            case RoundResult.Lose:
                return "Kaotus";
            default:
                return "Viik";
        }
    }
}
