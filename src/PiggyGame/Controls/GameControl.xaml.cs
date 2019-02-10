using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PiggyGame.Controls
{
    public partial class GameControl : UserControl
    {
        /**********************************************************************************/

        #region global scores
        //valoarea la care sa se declare castigatorul
        private static int max = 30;
        //numele playerilor
        private string nm1;
        private string nm2;
        //scorul globapl pt pl 1
        private int p1s = 0;
        //scorul globapl pt pl 2
        private int p2s = 0;
        #endregion

        #region current player
        //playerul curent 
        private int cp = 0;
        //schimba playerul curent
        private void nextPlayer()
        {
            this.players[this.cp].AddScore();
            if (this.checkWinner())
            {
                return;
            }

            if (this.cp == 0)
            {
                this.cp = 1;
                this.P1.EndTurn();
                this.P2.StartTurn();
            }
            else
            {
                this.cp = 0;
                this.P2.EndTurn();
                this.P1.StartTurn();
            }
        }
        //array cu cei doi playeri
        private PlayerControl[] players;
        #endregion

        #region constructor
        public GameControl()
        {
            InitializeComponent();
            this.onLoad();
        }
        //ascunde butonul de new game pana cand exista un castigator
        //seteaza primul player ca ii randul lui
        private void onLoad()
        {
            this.events();
            this.NewGameTab.Visibility = Visibility.Collapsed;

            this.players = new PlayerControl[2];

            this.players[0] = this.P1;
            this.players[1] = this.P2;

            this.P1.StartTurn();
            this.P2.EndTurn();
        }
        //eventruile pentru clickuri pe butoanel
        //eventryl pentru cand animatia se termina
        //poate da click pe bunto de roll doar daca animatia este completa
        private void events()
        {
            this.Dicer.OnFinsihRoll = new EventHandler(OnFinsihRoll);

            this.P1.OnRoll = new RoutedEventHandler(PlayerOneRoll);
            this.P1.OnHold = new RoutedEventHandler(PlayerOneHold);

            this.P2.OnRoll = new RoutedEventHandler(PlayerTwoRoll);
            this.P2.OnHold = new RoutedEventHandler(PlayerTwoHold);

            this.ResetButton.Click += OnResetGame;
        }
        //daca da pe butonul de sus pe albastru de new game dispare si inpece joc nou
        //apare doar cand exista castigator
        private void OnResetGame(object sender, RoutedEventArgs e)
        {
            this.P1.Reset();
            this.P2.Reset();

            this.players[this.cp].StartTurn();
            this.NewGameTab.Visibility = Visibility.Collapsed;
        }
        //seteaza numele cleor doi player si sus si jos
        public void SetNames(string n1, string n2)
        {
            this.nm1 = n1;
            this.nm2 = n2;

            this.P1.SetName(n1);
            this.P2.SetName(n2);

            this.P1Name.Text = n1;
            this.P2Name.Text = n2;
        }
        #endregion


        #region event handlers
        private bool canRoll = true;
        //eventurile pentru cand da pe roll pt fiecare player
        private void PlayerOneRoll(object sender, RoutedEventArgs e)
        {
            if (!this.canRoll)
                return;

            this.canRoll = false;
            this.Dicer.Roll();
        }
        private void PlayerTwoRoll(object sender, RoutedEventArgs e)
        {
            if (!this.canRoll)
                return;

            this.canRoll = false;
            this.Dicer.Roll();
        }
        //eventurile pentru cand da pe hold pt fiecare player
        private void PlayerOneHold(object sender, RoutedEventArgs e)
        {
            this.nextPlayer();
        }
        private void PlayerTwoHold(object sender, RoutedEventArgs e)
        {
            this.nextPlayer();
        }
        //eventul pentru cand se termina aplicatia
        private void OnFinsihRoll(object sender, EventArgs e)
        {
            if (rules())
            {
                this.players[this.cp].ResetCurrentScore();
                this.nextPlayer();
                this.canRoll = true;
                return;
            }

            this.players[this.cp].AddCurrentScore(this.Dicer.Rolls[0] + this.Dicer.Rolls[1]);
            this.canRoll = true;
        }
        #endregion

        #region winner&loser
        //regulile pentru cand playerul curent pierde scorul
        //daca da dubla
        //sau suma zarurilor este 7
        private bool rules()
        {
            if (this.Dicer.Rolls[0] == this.Dicer.Rolls[1])
                return true;

            if (this.Dicer.Rolls[0] + this.Dicer.Rolls[1] == 7)
                return true;

            return false;
        }
        //verifica daca este castigator si opreste jocul daca este
        private bool checkWinner()
        {
            if (players[this.cp].Score >= max)
            {
                this.endGame();
                return true;
            }

            return false;
        }
        //afiseaza castigatorul si butonul de joc nou
        private void endGame()
        {
            if(this.cp == 0)
            {
                this.p1s += 1;
                this.P1Score.Text = Convert.ToString(this.p1s);
                this.winner.Text = nm1;
            }
            else
            {
                this.p2s += 1;
                this.P2Score.Text = Convert.ToString(this.p2s);
                this.winner.Text = nm2;
            }

            this.P1.EndTurn();
            this.P2.EndTurn();

            this.NewGameTab.Visibility = Visibility.Visible;
        }
        #endregion

        /**********************************************************************************/

    }
}
