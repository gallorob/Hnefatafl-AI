using HnefataflAI.Games.Boards;
using HnefataflAI.Games.Rules;
using System.Windows;
using System.Windows.Input;
using TaflWPF.ViewModel;

namespace TaflWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        public GameViewModel GameVM { get; set; }
		public MainWindow()
		{
            this.GameVM = new GameViewModel(BoardTypes.TAFL_11x11, RuleTypes.HNEFATAFL);
			InitializeComponent();
			this.DataContext = this.GameVM;
        }
        private void GridBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GameVM.OnPreviewMouseDown(sender, e);
        }

    }
}
