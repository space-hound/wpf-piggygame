using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PiggyGame.Controls
{
    public partial class PlayerControl : UserControl
    {
        //culorile pentru background si fonturi
        //are randul cel care are backgroundul negru
        private static SolidColorBrush black = new SolidColorBrush(Color.FromRgb(33, 33, 33));
        private static SolidColorBrush white = new SolidColorBrush(Color.FromRgb(250, 250, 250));


        public PlayerControl()
        {
            InitializeComponent();
            this.events();
        }

        //eventuri pentru cand da click pe roll sau hold
        public void events()
        {
            this.RollButton.Click += onRoll;
            this.HoldButton.Click += onHold;
        }

        //enventul in sine si handlerul declarat aici definit in GameControl
        public RoutedEventHandler OnHold;
        private void onHold(object sender, RoutedEventArgs e)
        {
            if (this.OnHold is null)
                return;
            this.OnHold(sender, e);
        }
        //enventul in sine si handlerul declarat aici definit in GameControl
        public RoutedEventHandler OnRoll;
        private void onRoll(object sender, RoutedEventArgs e)
        {
            if (this.OnRoll is null)
                return;
            this.OnRoll(sender, e);
        }

        private bool isTurn = false;
        public bool IsTurn
        {
            get
            {
                return this.isTurn;
            }
        }

        //scorul curent si scorul total al unul player
        //nu se pot modifica explicit decat prin cele doua functiii
        private int score = 0;
        public int Score
        {
            get
            {
                return this.score;
            }
        }
        //asta
        public void AddScore()
        {
            this.score += this.currentScore;
            this.PScore.Text = Convert.ToString(this.score);
        }
        private int currentScore = 0;
        public int CurrentScore
        {
            get
            {
                return this.score;
            }
        }
        //si asta
        public void AddCurrentScore(int val)
        {
            this.currentScore += val;
            this.PCurrentScore.Text = Convert.ToString(this.currentScore);
        }

        //resteaza scorul curent la zero si modifica texboxul sa reflecte asta
        public void ResetCurrentScore()
        {
            this.currentScore = 0;
            this.PCurrentScore.Text = Convert.ToString(this.currentScore);
        }

        //cand incepe trua se face scrisul alb backggroundul negru
        public void StartTurn()
        {
            this.isTurn = true;
            this.PContainer.Background = black;
            this.PName.Foreground = white;
            this.PScore.Foreground = white;
            this.PCurrentScore.Foreground = white;
            this.RollButton.Foreground = white;
            this.HoldButton.Foreground = white;
            this.RollButton.IsEnabled = true;
            this.HoldButton.IsEnabled = true;
        }
        //fix invers ca StartTurn() + resetaza scorul curent
        public void EndTurn()
        {
            this.isTurn = false;
            this.PContainer.Background = white;
            this.PName.Foreground = black;
            this.PScore.Foreground = black;
            this.PCurrentScore.Foreground = black;
            this.RollButton.Foreground = black;
            this.HoldButton.Foreground = black;
            this.RollButton.IsEnabled = false;
            this.HoldButton.IsEnabled = false;
            this.ResetCurrentScore();
        }

        //reseteaza complet totul si opreste tura
        public void Reset()
        {
            this.EndTurn();
            this.currentScore = 0;
            this.PCurrentScore.Text = Convert.ToString(this.currentScore);
            this.score = 0;
            this.PScore.Text = Convert.ToString(this.score);
        }
        //seteaza numele 
        public void SetName(string n1)
        {
            this.PName.Text = n1;
        }
    }
}
