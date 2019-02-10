using PiggyGame.Objects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PiggyGame.Controls
{
    public partial class DiceControl : UserControl
    {

        #region dependency props
        //proprietatea size care dicteaza marimea canvasului care este zarul
        //si marimea bordurii care contine zarul + grosimea bordurii x 2
        public static readonly DependencyProperty SizeProperty =
         DependencyProperty.Register("Size", typeof(double), typeof(DiceControl), new
            PropertyMetadata(60.0, new PropertyChangedCallback(OnSizeChanged)));
        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
        #endregion

        //constructorul
        public DiceControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.initAnimation();
        }

        #region props
        //daca poate sa dea cu zarul sau nu
        private bool canRoll = true;
        public bool CanRoll
        {
            get
            {
                return this.canRoll;
            }
            set
            {
                this.canRoll = value;
            }
        }

        //animatiile pentru fiecare zar
        DoubleAnimation Aa, Ab;
        //matricea de transformare folosita la roatia zarului
        RotateTransform Ta, Tb;

        //valorile zarurilor la un moment dat
        //se schimba de fiecare data dupa ce animatia de rotatie este completa
        private int[] rolls = new int[2];
        public int[] Rolls
        {
            get
            {
                return this.rolls;
            }
        }
        #endregion

        //initializaeaza animatiile si matricile pt rotatie
        void initAnimation()
        {
            //animatie de tip double
            //de la 0 grade la 90 de grade
            //dureaza 0.3 secunde
            Aa = new DoubleAnimation(0, 90, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            //se repeta de doua ori
            Aa.RepeatBehavior = new RepeatBehavior(2);
            //animatie de tip double
            //de la 0 grade la -90 de grade
            //dureaza 0.3 secunde
            Ab = new DoubleAnimation(0, -90, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            //se repeta de doua ori
            Ab.RepeatBehavior = new RepeatBehavior(2);

            //defineste matricle de transformare
            //pentru rotatie in functie de datele 
            //date la definirea bordurii care contine zarul
            //centrul X si centrul Y
            Ta = (RotateTransform)this.DiceContainerOne.RenderTransform;
            Tb = (RotateTransform)this.DiceContainerTwo.RenderTransform;

            //event pentru cand animatia este completa
            Aa.Completed += animationCompleted;
        }

        #region events
        //event handler declarat aici definit in GameControl
        public EventHandler OnFinsihRoll;
        //eventul in sine care executa handlerul de mai sus
        //cand se termina animatias
        private void animationCompleted(object sender, EventArgs e)
        {
            //daca nu poate da cu zarul iese afara
            if (this.OnFinsihRoll is null)
                return;

            this.canRoll = true;
            //seteaza valorile zarului si deseneaza fetele
            this.rolls[0] = DiceGenerator.Roll(this.DiceOne);
            this.rolls[1] = DiceGenerator.Roll(this.DiceTwo);

            this.OnFinsihRoll(sender, e);
        }
        #endregion

        //cand da cu zarul
        public void Roll()
        {
            if (!this.canRoll)
                return;

            //sterge fetele vechi
            this.DiceOne.Children.Clear();
            this.DiceTwo.Children.Clear();
            this.canRoll = false;
            //incepe animatia
            this.Ta.BeginAnimation(RotateTransform.AngleProperty, Aa);
            this.Tb.BeginAnimation(RotateTransform.AngleProperty, Ab);
        }
    }
}
