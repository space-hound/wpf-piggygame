using System.Windows;


namespace PiggyGame
{
    public partial class GameWindow : Window
    {
        //constructorul pentru jocul nou
        //ia ca parametrii numele date in meniu
        public GameWindow(string n1, string n2)
        {
            InitializeComponent();
            //seteaza numele playerilor
            this.Game.SetNames(n1, n2);
        }
    }
}
