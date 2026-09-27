using System.Collections.ObjectModel;
using System.Windows;
using RpsTournament.Core;
using Res = RpsTournament.WpfApp.Properties.Resources;

namespace RpsTournament.WpfApp;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<GameRound> _rounds = new();
    private int _playerScore;
    private int _computerScore;

    public MainWindow()
    {
        InitializeComponent();
        HistoryGrid.ItemsSource = _rounds;
        UpdateStatus();
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        if (_rounds.Count >= GameLogic.RoundsPerTournament)
        {
            ShowError(Res.ErrorTournamentOver);
            return;
        }

        if (!GameLogic.IsValidName(NameTextBox.Text))
        {
            ShowError(Res.ErrorNameLength);
            NameTextBox.Focus();
            return;
        }

        Move? playerMove = GetSelectedMove();
        if (playerMove is null)
        {
            ShowError(Res.ErrorNoMove);
            return;
        }

        Move computerMove = GameLogic.GetRandomMove();
        RoundResult result = GameLogic.DetermineResult(playerMove.Value, computerMove);

        switch (result)
        {
            case RoundResult.Win:
                _playerScore++;
                break;
            case RoundResult.Lose:
                _computerScore++;
                break;
        }

        _rounds.Add(new GameRound(_rounds.Count + 1, playerMove.Value, computerMove, result));
        UpdateStatus();

        if (_rounds.Count == GameLogic.RoundsPerTournament)
        {
            ShowWinner();
        }
    }

    private void NewTournamentButton_Click(object sender, RoutedEventArgs e)
    {
        _rounds.Clear();
        _playerScore = 0;
        _computerScore = 0;
        RockRadio.IsChecked = false;
        PaperRadio.IsChecked = false;
        ScissorsRadio.IsChecked = false;
        UpdateStatus();
    }

    private Move? GetSelectedMove()
    {
        if (RockRadio.IsChecked == true) return Move.Rock;
        if (PaperRadio.IsChecked == true) return Move.Paper;
        if (ScissorsRadio.IsChecked == true) return Move.Scissors;
        return null;
    }

    private void ShowWinner()
    {
        string name = NameTextBox.Text.Trim();
        string format;

        if (_playerScore > _computerScore)
            format = Res.WinnerPlayer;
        else if (_computerScore > _playerScore)
            format = Res.WinnerComputer;
        else
            format = Res.WinnerDraw;

        MessageBox.Show(string.Format(format, name, _playerScore, _computerScore),
            Res.ResultTitle, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void UpdateStatus()
    {
        bool finished = _rounds.Count >= GameLogic.RoundsPerTournament;
        PlayButton.IsEnabled = !finished;
        NameTextBox.IsEnabled = _rounds.Count == 0;
        RoundTextBlock.Text = $"Voor: {_rounds.Count}/{GameLogic.RoundsPerTournament}";
        ScoreTextBlock.Text = $"Skoor: mängija {_playerScore} – arvuti {_computerScore}";
    }

    private static void ShowError(string message)
    {
        MessageBox.Show(message, Res.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
