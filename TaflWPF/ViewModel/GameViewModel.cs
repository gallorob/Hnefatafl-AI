using HnefataflAI.AI;
using HnefataflAI.Commons;
using HnefataflAI.Commons.Logs;
using HnefataflAI.Commons.Positions;
using HnefataflAI.Commons.Utils;
using HnefataflAI.Games;
using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Engine;
using HnefataflAI.Games.Engine.Impl;
using HnefataflAI.Games.Rules;
using HnefataflAI.Player;
using HnefataflAI.Player.Impl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TaflWPF.Commands;
using TaflWPF.Model.Piece;
using TaflWPF.Properties;
using TaflWPF.Utils;

namespace TaflWPF.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        #region Properties
        public BoardViewModel BoardVM { get; private set; }
        internal IGameEngine GameEngine { get; private set; }
        internal Game Game { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public ICommand PreviewMouseDownCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
		public ObservableCollection<String> GameMoves { get; set; }

        private readonly string[] _Move = new string[2] { "", "" };
        private readonly DispatcherTimer DispatcherTimer = new DispatcherTimer();
        private readonly DateTime Started = DateTime.Now;

        private string m_Move;
        public string Move
        {
            get { return m_Move; }
            set { m_Move = value; NotifyPropertyChanged(nameof(Move)); }
        }
        private PieceItemViewModel m_SelectedPiece;
        public PieceItemViewModel SelectedPiece
        {
            get { return m_SelectedPiece; }
            set
            {
                if (m_SelectedPiece != null)
                    m_SelectedPiece.Selected = false;
                m_SelectedPiece = value;
                m_SelectedPiece.Selected = true;
                NotifyPropertyChanged(nameof(SelectedPiece));
            }
        }
        private string m_CurrentPlayer;
        public string CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; NotifyPropertyChanged(nameof(CurrentPlayer)); }
        }

        private string m_Timer;
        public string Timer
        {
            get { return m_Timer; }
            set { m_Timer = value; NotifyPropertyChanged(nameof(Timer)); }
        }


        private string m_BoardValue;
        public string BoardValue
        {
            get { return m_BoardValue; }
            set { m_BoardValue = value; NotifyPropertyChanged(nameof(BoardValue)); }
        }
        #endregion

        #region Constructor
        public GameViewModel(BoardTypes boardType, RuleTypes ruleType)
        {
            MoveCommand = new RelayCommand<string>(SendMoveToGame);
			PreviewMouseDownCommand = new RelayCommand<object>((sender) => OnPreviewMouseDown(sender));
            CopyCommand = new RelayCommand(CopyToClipboard);
			this.GameMoves = new ObservableCollection<string>();
            this.BoardVM = new BoardViewModel(BoardBuilder.GetBoard(boardType));
            this.GameEngine = new GameEngineImpl(ruleType);
            
            // temporarily assign different names
            HumanPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            HumanPlayer player2 = new HumanPlayer(PieceColors.WHITE, "Sigfrid");

            this.Game = new Game(BoardVM.Board, player1, player2, GameEngine);
            this.Move = GetMoveRepresentation();
            this.BoardVM.ShowCorners = GameEngine.RuleEngine.Rule.IsCornerEscape;
            UpdateCurrentPlayer();
            UpdateBoardValue();
            
            // game timer
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            DispatcherTimer.Start();
        }
        #endregion

        #region Events
        private void OnPreviewMouseDown(object sender)
        {
			if(sender is MouseButtonEventArgs args && args.Source is Border border && border.DataContext is PieceItemViewModel item)
            {
                if (item.IsPossibleMove)
                {
                    Position newPosition = GridUtils.GetPositionFromIndex(item.Index, BoardVM.Board.TotalCols);
                    _Move[1] = newPosition.ToString();
                }
                else
                {
                    SelectedPiece = item;
                    _Move[0] = item.Piece != null ? item.Piece.Position.ToString() : "";
                    _Move[1] = "";
                    CalculatePossibleMoves(item);
                }
                Move = GetMoveRepresentation();
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Timer = string.Format("{0:hh\\:mm\\:ss}", DateTime.Now.Subtract(Started));
        }
        #endregion

        #region Game interaction
        private void SendMoveToGame(string move)
        {
            try
            {
                IPlayer playing = GetPlayer();
                Move playedMove = Game.PlayTurn(playing, move.ToLower());

                // TODO isSuicidal and isWinning should be obtained from GameStatus
                GameMoves.Add(LoggingUtils.LogCyningstanStyle(playedMove, Game.GameStatus.CapturedPieces, false, false, Game.GameStatus.IsGameOver));

                if (!Game.GameStatus.IsGameOver)
                {
                    Game.CurrentlyPlaying = Game.GameStatus.NextPlayer;
                    UpdateCurrentPlayer();
                    UpdateBoardValue();

                    _Move[0] = "";
                    _Move[1] = "";
                    Move = GetMoveRepresentation();
                }
                else
                {
                    MessageBox.Show(string.Format(Resources.GameOverText, Game.GameStatus.Reason, PieceColorsUtils.GetRoleFromPieceColor(Game.CurrentlyPlaying), Resources.GameOverTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation));
                }
                RefreshCaptured();
                BoardVM.RefreshBoard();
                CheckIfPieceWasCaptured();
                CheckKingHasEscaped();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.ErrorText, ex.Message), Resources.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                GameLogger.Log(ex.Message + " " + ex.StackTrace);
            }
        }
        private void CopyToClipboard()
        {
            string text = string.Join("\n", GameMoves);
            Clipboard.SetText(text);
        }
        public void CalculatePossibleMoves(PieceItemViewModel item)
        {
            BoardVM.Pieces.ToList().ForEach(piece => piece.IsPossibleMove = false);
            if (item.Piece != null)
            {
                var moveslikejagger = this.GameEngine.RuleEngine.GetAvailableMoves(item.Piece, BoardVM.Board);
                foreach (var move in moveslikejagger)
                {
                    BoardVM.Pieces.Where((piece) => piece.Index == GridUtils.GetIndexFromPosition(move.To, BoardVM.ColumnCount)).ToList().ForEach((piece) => piece.IsPossibleMove = true);
                }
            }
        }
        public void CheckKingHasEscaped()
        {
            PieceItemViewModel king = BoardVM.Pieces.ToList().Where(piece => piece.PieceType.Equals(PieceType.KING)).FirstOrDefault();
            if (king != null)
            {
                king.HasEscaped = Game.GameStatus.IsGameOver && Game.CurrentlyPlaying.Equals(PieceColors.WHITE);
            }
        }
        public void CheckIfPieceWasCaptured()
        {
            BoardVM.Pieces.ToList().ForEach(piece => piece.IsCaptured = piece.Piece == null && CheckIfPieceWasCaptured(piece.Index));
        }
        public bool CheckIfPieceWasCaptured(int index)
        {
            List<int> captured = new List<int>();
            Game.GameStatus.CapturedPieces.ForEach(piece => captured.Add(GridUtils.GetIndexFromPosition(piece.Position, BoardVM.Board.TotalCols)));
            return captured.Contains(index);
        }
        public void RefreshCaptured()
        {
            BoardVM.Pieces.ToList().ForEach(piece => piece.IsCaptured = false);
        }
        #endregion

        #region Other
        public String GetMoveRepresentation()
        {
            return String.Format(Resources.MoveString, _Move[0].ToLower(), _Move[1].ToLower());
        }
        private IPlayer GetPlayer()
        {
            return Game.Player1.PieceColors.Equals(Game.CurrentlyPlaying) ? Game.Player1 : Game.Player2;
        }
        private void UpdateCurrentPlayer()
        {
            CurrentPlayer = String.Format(Resources.CurrentPlayer, GetPlayer().PlayerName, PieceColorsUtils.GetRoleFromPieceColor(Game.CurrentlyPlaying));
        }
        private void UpdateBoardValue()
        {
            BoardEvaluator boardEvaluator = new BoardEvaluator();
            BoardValue = String.Format(Resources.BoardValue, boardEvaluator.EvaluateBoard(BoardVM.Board, Game.CurrentlyPlaying));
        }
        #endregion
    }
}
