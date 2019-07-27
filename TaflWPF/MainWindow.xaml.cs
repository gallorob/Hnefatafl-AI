using HnefataflAI.Games.Boards;
using HnefataflAI.Pieces.Impl;
using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
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
    }
}
