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
using TaflWPF.Common;
using TaflWPF.Utils;

namespace TaflWPF.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        #region Properties
        public BoardViewModel BoardVM { get; private set; }
        internal IGameEngine GameEngine { get; private set; }
        internal Game Game { get; private set; }
        public MoveCommand MoveCommand { get; private set; }
        public ObservableCollection<String> GameMoves { get; set; }

        private readonly String[] _Move = new string[2] { "", "" };
        private readonly DispatcherTimer DispatcherTimer = new DispatcherTimer();
        private readonly DateTime Started = DateTime.Now;

        private String m_Move;
        public String Move
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
        private String m_CurrentPlayer;
        public String CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; NotifyPropertyChanged(nameof(CurrentPlayer)); }
        }

        private String m_Timer;
        public String Timer
        {
            get { return m_Timer; }
            set { m_Timer = value; NotifyPropertyChanged(nameof(Timer)); }
        }


        private String m_BoardValue;
        public String BoardValue
        {
            get { return m_BoardValue; }
            set { m_BoardValue = value; NotifyPropertyChanged(nameof(BoardValue)); }
        }
        #endregion

        #region Constructor
        public GameViewModel(BoardTypes boardType, RuleTypes ruleType)
        {
            MoveCommand = new MoveCommand(SendMoveToGame);
            this.GameMoves = new ObservableCollection<String>();
            this.BoardVM = new BoardViewModel(BoardBuilder.GetBoard(boardType));
            this.GameEngine = new GameEngineImpl(ruleType);
            
            // temporarily assign different names
            HumanPlayer player1 = new HumanPlayer(PieceColors.BLACK);
            HumanPlayer player2 = new HumanPlayer(PieceColors.WHITE) { PlayerName = "Sigfrid" };

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
        public void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is PieceItemViewModel item)
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
                e.Handled = true;
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Timer = String.Format("{0:hh\\:mm\\:ss}", DateTime.Now.Subtract(Started));
        }
        #endregion

        #region Game interaction
        private void SendMoveToGame(string move)
        {
            try
            {
                IPlayer playing = GetPlayer();
                Move playedMove = Game.PlayTurn(playing, move.ToLower());
                RefreshCaptured();
                BoardVM.RefreshBoard();
                CheckIfPieceWasCaptured();
                CheckPieceUnderThreat();

                // TODO isSuicidal and isWinning should be obtained from GameStatus
                GameMoves.Add(LoggingUtils.LogCyningstanStyle(playedMove, Game.GameStatus.CapturedPieces, false, false, Game.GameStatus.IsGameOver));

                if (!Game.GameStatus.IsGameOver)
                {
                    Game.CurrentlyPlaying = Game.GameStatus.NextPlayer;
                    UpdateCurrentPlayer();
                    UpdateBoardValue();
                }
                else
                {
                    MessageBox.Show(String.Format(Messages.GAMEOVER_TEXT, Game.GameStatus.Reason, PieceColorsUtils.GetRoleFromPieceColor(Game.CurrentlyPlaying), Messages.GAMEOVER_TITLE, MessageBoxButton.OK, MessageBoxImage.Exclamation));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Messages.ERROR_TEXT, ex.Message), Messages.ERROR_TITLE, MessageBoxButton.OK, MessageBoxImage.Warning);
                GameLogger.Log(ex.Message + " " + ex.StackTrace);
            }
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
        public void CheckPieceUnderThreat()
        {
            BoardVM.Pieces.ToList().ForEach(piece => piece.IsThreatened = piece.Piece != null
                &&
                piece.Piece.PieceColors.Equals(Game.CurrentlyPlaying)
                &&
                GameEngine.RuleEngine.IsPieceThreatened(piece.Piece, BoardVM.Board));
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
            return String.Format(Messages.MOVE, _Move[0].ToLower(), _Move[1].ToLower());
        }
        private IPlayer GetPlayer()
        {
            return Game.Player1.PieceColors.Equals(Game.CurrentlyPlaying) ? Game.Player1 : Game.Player2;
        }
        private void UpdateCurrentPlayer()
        {
            CurrentPlayer = String.Format(Messages.CURRENTPLAYER, GetPlayer().PlayerName, PieceColorsUtils.GetRoleFromPieceColor(Game.CurrentlyPlaying));
        }
        private void UpdateBoardValue()
        {
            BoardEvaluator boardEvaluator = new BoardEvaluator();
            BoardValue = String.Format(Messages.BOARDVALUE, boardEvaluator.EvaluateBoard(BoardVM.Board, Game.CurrentlyPlaying));
        }
        #endregion
    }
}
