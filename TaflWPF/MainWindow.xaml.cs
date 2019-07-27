using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaflWPF.Model;
using TaflWPF.ViewModel;

namespace TaflWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public BoardViewModel BoardVM { get; set; }
		//public PieceItemViewModel PieceItem { get; set; }
		public MainWindow()
		{
			this.BoardVM = new BoardViewModel(BoardBuilder.GetHistoricalHnefatafl11Table());
			//this.PieceItem = new PieceItemViewModel(new Attacker(new HnefataflAI.Commons.Positions.Position(1, 'a')));
			InitializeComponent();
			this.DataContext = this;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

		private void GridBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is Border border && border.DataContext is PieceItemViewModel item)
			{
				SelectedPiece = item;

				BoardVM.CalculatePossibleMoves(item);

				e.Handled = true;
			}
		}
	}
}
