using System.Windows;

namespace PiggyGame
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }


        //cand dai start game(event)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //daca casutele cu nume sunt goale nu se intampla nimic
            if (this.PlayerOne.Text == "")
                return;
            if (this.PlayerTwo.Text == "")
                return;

            //daca casutele nu sunt goale incepe un nou joc cu numele date 
            var game = new GameWindow(this.PlayerOne.Text, this.PlayerTwo.Text);
            //afiseaza jocul nou
            game.Show();
            //inchide meniul
            this.Close();
        }
    }
}
