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
using System.Windows.Media.Animation;
using System.Collections;
using Xceed.Wpf.Toolkit;
namespace WormRaceInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random(DateTime.Now.Second);
        enum GameStates { selectName, SelectWorm, SelectBet, ReadyToStart, RaceInProgress, RaceEnd };
        GameStates CurrentGameState1;
        GameStates CurrentGameState2;
        GameStates CurrentGameState3;
        Worm winnerWorm;
        Worm selectedWorm1, selectedWorm2, selectedWorm3;

        int betAmount1, betAmount2, betAmount3;
        StringBuilder WinnerTimes;
        int WormCompletedCount;
        Dictionary<String, int> betfactorsMap;
        Storyboard sb = new Storyboard();
        DoubleAnimation da;
        public static int bettor1 = 50;
        public static int bettor2 = 50;
        public static int bettor3 = 50;
        enum selectedbettor { joe, bob, al };
        selectedbettor bettor;
        string forname = "";
        public MainWindow()
        {
            InitializeComponent();
            WinnerTimes = new StringBuilder();
            WormCompletedCount = 0;
            CurrentGameState1 = CurrentGameState2 = CurrentGameState3 = GameStates.SelectWorm;

            switch (bettor)
            {
                case selectedbettor.joe:
                    forname = "Joe";
                    break;
                case selectedbettor.bob:
                    forname = "Bob";
                    break;
                case selectedbettor.al:
                    forname = "Al";
                    break;
            }
            textBox1.Text = "Select Your Player and Worm to Place Your Bet for " + forname + "\n Hover on worm to get details";

            betfactorsMap = new Dictionary<string, int>();
            betfactorsMap.Add(YellowWorm.Name, 2);
            betfactorsMap.Add(RedWorm.Name, 2);
            betfactorsMap.Add(WoodWorm.Name, 2);
            betfactorsMap.Add(GreenWorm.Name, 2);
            //maxamount = rnd.Next(1, 50);
            betdoller.Maximum = bettor1;
            betsMesage.Content = "Max bet is : $ " + bettor1;
            FirstBet.Content = "Joe hasn't placed a bet and has $" + bettor1;
            SecondBet.Content = "Bob hasn't placed a bet and has $" + bettor2;
            ThirdBet.Content = "Al hasn't placed a bet and has $" + bettor3;

        }

        //start button handler
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (0 == Start.Content.ToString().CompareTo("Reset"))
            {
                ResetGame();
                Start.Content = "Start";
                return;
            }

           

            if (CurrentGameState1 != GameStates.ReadyToStart && CurrentGameState2 != GameStates.ReadyToStart && CurrentGameState3 != GameStates.ReadyToStart)
            {
                String s = "Game is not ready to Start!, Please Follow The Message Below";
                System.Windows.MessageBox.Show(s);
                return;
            }



            da = new DoubleAnimation(0, 750, TimeSpan.FromSeconds(rnd.Next(10, 15) % 15));
            da.AccelerationRatio = rnd.NextDouble();
            da.Completed += da_Completed;
            da.Name = "YellowWorm";
            YellowWorm.BeginAnimation(Canvas.LeftProperty, da);

            da = new DoubleAnimation(0, 750, TimeSpan.FromSeconds(rnd.Next(10, 15) % 15));
            da.AccelerationRatio = rnd.NextDouble();
            da.Completed += da_Completed;
            da.Name = "RedWorm";
            RedWorm.BeginAnimation(Canvas.LeftProperty, da);

            da = new DoubleAnimation(0, 750, TimeSpan.FromSeconds(rnd.Next(10, 15) % 15));
            da.AccelerationRatio = rnd.NextDouble();
            da.Completed += da_Completed;
            da.Name = "WoodWorm";
            WoodWorm.BeginAnimation(Canvas.LeftProperty, da);

            da = new DoubleAnimation(0, 750, TimeSpan.FromSeconds(rnd.Next(10, 15) % 15));
            da.AccelerationRatio = rnd.NextDouble();
            da.Completed += da_Completed;
            da.Name = "GreenWorm";
            GreenWorm.BeginAnimation(Canvas.LeftProperty, da);

        }

        void da_Completed(object sender, EventArgs e)
        {
            AnimationClock ac = (AnimationClock)sender;
            DoubleAnimation d = (DoubleAnimation)ac.Timeline;
            StringBuilder s = new StringBuilder(d.Name);
            s.Append(" Completed Race in");
            s.Append(d.Duration.ToString());
            s.Append("sec");
            textBox1.Text = s.ToString();


            WinnerTimes.Append(d.Name);
            WinnerTimes.Append("=");
            WinnerTimes.Append(d.Duration.TimeSpan.Seconds.ToString());
            WinnerTimes.Append("sec");
            WinnerTimes.Append("\n");

            WormCompletedCount++;


            if (WormCompletedCount == 1)
            {
                winnerWorm = (Worm)GameGrid.FindName(d.Name);
            }

            if (WormCompletedCount == 4)
            {
                CurrentGameState1 = CurrentGameState2 = CurrentGameState3 = GameStates.RaceEnd;
                DisplayStateMessage();
            }



        }

        //bet
        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    if (CurrentGameState != GameStates.SelectBet)
        //    {
        //        System.Windows.MessageBox.Show("You cannot place bet. please follow message below");
        //        return;
        //    }

        //    Button b = (Button)sender;

        //    switch (b.Name)
        //    {
        //        case "bet1":
        //            betAmount = 25;
        //            break;
        //        case "bet2":
        //            betAmount = 50;
        //            break;
        //        case "bet3":
        //            betAmount = 75;
        //            break;
        //        case "bet4":
        //            betAmount = 100;
        //            break;
        //    }

        //    CurrentGameState = GameStates.ReadyToStart;
        //    DisplayStateMessage();
        //}

        private void worm1_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            if (bets1.IsChecked == true)
            {
                if (CurrentGameState1 != GameStates.SelectWorm)
                {
                    System.Windows.MessageBox.Show("You cannot select worm now. please follow message below");
                    return;
                }
            }
            else if (bets2.IsChecked == true)
            {
                if (CurrentGameState2 != GameStates.SelectWorm)
                {
                    System.Windows.MessageBox.Show("You cannot select worm now. please follow message below");
                    return;
                }
            }
            else if (bets3.IsChecked == true)
            {
                if (CurrentGameState3 != GameStates.SelectWorm)
                {
                    System.Windows.MessageBox.Show("You cannot select worm now. please follow message below");
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select your player first to initiate game.");
                return;
            }

            Worm w = (Worm)sender;
            switch (bettor)
            {
                case selectedbettor.joe:
                    forname = "Joe";
                    CurrentGameState1 = GameStates.SelectBet;
                    selectedWorm1 = w;
                    w.IsEnabled = false;
                    break;
                case selectedbettor.bob:
                    forname = "Bob";
                    CurrentGameState2 = GameStates.SelectBet;
                    selectedWorm2 = w;
                    w.IsEnabled = false;
                    break;
                case selectedbettor.al:
                    forname = "Al";
                    CurrentGameState3 = GameStates.SelectBet;
                    selectedWorm3 = w;
                    w.IsEnabled = false;
                    break;
            }

            StringBuilder txt = new StringBuilder("You Placed Bet on Worm");
            txt.Append(w.Name);
            txt.Append(" for " + forname);
            textBox1.Text = txt.ToString();
            DisplayStateMessage();
        }

        private void worm1_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Worm w = (Worm)sender;
            StringBuilder txt = new StringBuilder("This is ");
            txt.Append(w.Name);
            if (w.IsEnabled == false)
            {
                if (w.Name == selectedWorm1.Name)
                {
                    txt.Append(" already selected for Joe");
                    textBox1.Text = txt.ToString();
                    return;
                }
                else if (w.Name == selectedWorm2.Name)
                {
                    txt.Append(" already selected for Bob");
                    textBox1.Text = txt.ToString();
                    return;
                }
                else if (w.Name == selectedWorm3.Name)
                {
                    txt.Append(" already selected for Al");
                    textBox1.Text = txt.ToString();
                    return;
                }
                else
                {
                    txt.Append(", You will get your Bet x ");
                    txt.Append(betfactorsMap[w.Name].ToString());
                    textBox1.Text = txt.ToString();
                    return;
                }
            }

            w.border.BorderThickness = new Thickness(2, 2, 2, 2);
            w.border.BorderBrush = Brushes.LightCoral;

            txt.Append(", You will get your Bet x ");
            txt.Append(betfactorsMap[w.Name].ToString());
            textBox1.Text = txt.ToString();
        }

        private void worm1_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Worm w = (Worm)sender;
            w.border.BorderBrush = Brushes.Transparent;
            DisplayStateMessage();
        }

        private void DisplayStateMessage()
        {
            if (CurrentGameState1 == GameStates.RaceEnd && CurrentGameState2 == GameStates.RaceEnd && CurrentGameState3 == GameStates.RaceEnd)
            {
                StringBuilder s = new StringBuilder(WinnerTimes.ToString());
                s.Append("Winner is :");
                s.Append(winnerWorm.Name);
                s.Append("\nBet Placement was below:");
                s.Append("\nJoe : " + selectedWorm1.Name + ", Amount : $" + betAmount1);
                s.Append("\nBob : " + selectedWorm2.Name + ", Amount : $" + betAmount2);
                s.Append("\nAl : " + selectedWorm3.Name + ", Amount : $" + betAmount3);
                textBox1.Foreground = Brushes.Green;
                s.Append(", Click Reset to play again");
                textBox1.Text = s.ToString();
                Start.Content = "Reset";
                if (0 == winnerWorm.Name.CompareTo(selectedWorm1.Name))
                {
                    s = new StringBuilder();
                    bettor1 -= betAmount1;
                    bettor1 += (betAmount1 * betfactorsMap[selectedWorm1.Name]);
                    s.Append("Joe Won and now has $" + bettor1);
                    

                    FirstBet.Content = s.ToString();
                    bettor2 -= betAmount2;
                    SecondBet.Content = "Bob lost and now has $" + bettor2;
                    bettor3 -= betAmount3;
                    ThirdBet.Content = "Al lost and now has $" + bettor3;
                }
                else if (0 == winnerWorm.Name.CompareTo(selectedWorm2.Name))
                {
                    s = new StringBuilder();
                    bettor2 -= betAmount2;
                    bettor2 += (betAmount2 * betfactorsMap[selectedWorm2.Name]);
                    s.Append("Bob Won and now has $" + bettor2);

                    SecondBet.Content = s.ToString();
                    bettor1 -= betAmount1;
                    FirstBet.Content = "Joe lost and now has $" + bettor1;
                    bettor3 -= betAmount3;
                    ThirdBet.Content = "Al lost and now has $" + bettor3;
                }
                else if (0 == winnerWorm.Name.CompareTo(selectedWorm3.Name))
                {
                    s = new StringBuilder();
                    bettor3 -= betAmount3;
                    bettor3 += (betAmount3 * betfactorsMap[selectedWorm3.Name]);
                    s.Append("Al Won and now has $" + bettor3);

                    ThirdBet.Content = s.ToString();
                    bettor1 -= betAmount1;
                    FirstBet.Content = "Joe lost and now has $" + bettor1;
                    bettor2 -= betAmount2;
                    SecondBet.Content = "Bob lost and now has $" + bettor2;
                }
                else
                {
                    s.Append("\n\n");
                    s.Append("Sorry! Better Luck Next Time\t");
                    s.Append("All players are lost.");
                    textBox1.Foreground = Brushes.Red;
                    s.Append(", Click Reset to play again");
                    textBox1.Text = s.ToString();
                    
                        bettor1 -= betAmount1;
                        FirstBet.Content = "Joe lost and now has $" + bettor1;
                        bettor2 -= betAmount2;
                        SecondBet.Content = "Bob lost and now has $" + bettor2;
                        bettor3 -= betAmount3;
                        ThirdBet.Content = "Al lost and now has $" + bettor3;
                }
                if (bettor1 == 0)
                {
                    FirstBet.Content = "Joe lost all money, BUSTED !";
                    bets1.IsEnabled = false;
                }
                if (bettor2 == 0)
                {
                    SecondBet.Content = "Bob lost all money, BUSTED !";
                    bets2.IsEnabled = false;
                }
                if (bettor3 == 0)
                {
                    ThirdBet.Content = "Al lost all money, BUSTED !";
                    bets3.IsEnabled = false;
                }

            }
            else if (bets1.IsChecked == true)
            {
                switch (CurrentGameState1)
                {
                    case GameStates.SelectWorm:
                        {
                            textBox1.Text = "\n Select Your Worm By Clicking on Worm for joe ";
                        }
                        break;
                    case GameStates.SelectBet:
                        {
                            StringBuilder s = new StringBuilder("\n You have selected worm : ");
                            s.Append(selectedWorm1.Name);
                            s.Append("\n Now Select Amount You Want to Bet for joe");
                            textBox1.Text = s.ToString();
                            betdoller.IsEnabled = true;
                            buttonConfirm.Visibility = Visibility.Visible;
                            betdoller.Value = 0;
                        }
                        break;
                    case GameStates.ReadyToStart:
                        {
                            StringBuilder s = new StringBuilder("\n Your have selected Worm:");
                            s.Append(selectedWorm1.Name);
                            s.Append(", bet amount of player joe is ");
                            s.Append(": $" + betAmount1);
                            textBox1.Text = s.ToString();
                        }
                        break;
                }
            }
            else if (bets2.IsChecked == true)
            {
                switch (CurrentGameState2)
                {
                    case GameStates.SelectWorm:
                        {
                            textBox1.Text = "\n Select Your Worm By Clicking on Worm for Bob ";
                        }
                        break;
                    case GameStates.SelectBet:
                        {
                            StringBuilder s = new StringBuilder("\n You have selected worm : ");
                            s.Append(selectedWorm2.Name);
                            s.Append("\n Now Select Amount You Want to Bet for Bob");
                            textBox1.Text = s.ToString();
                            betdoller.IsEnabled = true;
                            buttonConfirm.Visibility = Visibility.Visible;
                            betdoller.Value = 0;
                        }
                        break;
                    case GameStates.ReadyToStart:
                        {
                            StringBuilder s = new StringBuilder("\n Your have selected Worm:");
                            s.Append(selectedWorm2.Name);
                            s.Append(", bet amount of player bob is ");
                            s.Append(": $" + betAmount2);
                            textBox1.Text = s.ToString();
                        }
                        break;
                }
            }
            else if (bets3.IsChecked == true)
            {
                switch (CurrentGameState3)
                {
                    case GameStates.SelectWorm:
                        {
                            textBox1.Text = "\n Select Your Worm By Clicking on Worm for Bob ";
                        }
                        break;
                    case GameStates.SelectBet:
                        {
                            StringBuilder s = new StringBuilder("\n You have selected worm : ");
                            s.Append(selectedWorm3.Name);
                            s.Append("\n Now Select Amount You Want to Bet for Bob");
                            textBox1.Text = s.ToString();
                            betdoller.IsEnabled = true;
                            buttonConfirm.Visibility = Visibility.Visible;
                            betdoller.Value = 0;
                        }
                        break;
                    case GameStates.ReadyToStart:
                        {
                            StringBuilder s = new StringBuilder("\n Your have selected Worm:");
                            s.Append(selectedWorm3.Name);
                            s.Append(", bet amount of player Al is ");
                            s.Append(": $" + betAmount3);
                            textBox1.Text = s.ToString();
                        }
                        break;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select your player first to initiate game.");
                return;
            }


        }

        private void ResetGame()
        {
            sb.Children.Remove(da);
            DoubleAnimation da1 = new DoubleAnimation(750, 0, TimeSpan.FromSeconds(1));
            YellowWorm.BeginAnimation(Canvas.LeftProperty, da1);
            RedWorm.BeginAnimation(Canvas.LeftProperty, da1);
            WoodWorm.BeginAnimation(Canvas.LeftProperty, da1);
            GreenWorm.BeginAnimation(Canvas.LeftProperty, da1);


            WormCompletedCount = 0;
            CurrentGameState1 = GameStates.SelectWorm;
            CurrentGameState2 = GameStates.SelectWorm;
            CurrentGameState3 = GameStates.SelectWorm;
            YellowWorm.IsEnabled = RedWorm.IsEnabled = WoodWorm.IsEnabled = GreenWorm.IsEnabled = true;
            WinnerTimes = new StringBuilder();
            winnerWorm = null;
            textBox1.Text = "Start again with new bets. \n and Proceed further.";
            WinnerTimes.Clear();

            betfactorsMap = new Dictionary<string, int>();
            betfactorsMap.Add(YellowWorm.Name, rnd.Next(3, 6));
            betfactorsMap.Add(RedWorm.Name, rnd.Next(3, 6));
            betfactorsMap.Add(WoodWorm.Name, rnd.Next(3, 6));
            betfactorsMap.Add(GreenWorm.Name, rnd.Next(3, 6));

            if (bettor1 < betAmount1)
                betAmount1 = bettor1;
            if (bettor2 < betAmount2)
                betAmount2 = bettor2;
            if (bettor3 < betAmount3)
                betAmount3 = bettor3;


        }


        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (bets1.IsChecked == true)
            {
                if (CurrentGameState1 != GameStates.SelectBet)
                {
                    String s = "Game is not ready to Start!, Please Follow The Message Below";
                    System.Windows.MessageBox.Show(s);
                    return;
                }
            }
            else if (bets2.IsChecked == true)
            {
                if (CurrentGameState2 != GameStates.SelectBet)
                {
                    String s = "Game is not ready to Start!, Please Follow The Message Below";
                    System.Windows.MessageBox.Show(s);
                    return;
                }
            }
            else if (bets3.IsChecked == true)
            {
                if (CurrentGameState3 != GameStates.SelectBet)
                {
                    String s = "Game is not ready to Start!, Please Follow The Message Below";
                    System.Windows.MessageBox.Show(s);
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select your player first to initiate game.");
                return;
            }

            switch (bettor)
            {
                case selectedbettor.joe:
                    CurrentGameState1 = GameStates.ReadyToStart;

                    break;
                case selectedbettor.bob:
                    CurrentGameState2 = GameStates.ReadyToStart;
                    break;
                case selectedbettor.al:
                    CurrentGameState3 = GameStates.ReadyToStart;
                    break;
            }
            buttonConfirm.Visibility = Visibility.Hidden;
            DisplayStateMessage();
            if (CurrentGameState1 == GameStates.ReadyToStart && CurrentGameState2 == GameStates.ReadyToStart && CurrentGameState3 == GameStates.ReadyToStart)
            {
                StringBuilder s = new StringBuilder("Your players and worms are selected");
                s.Append(", bet amount of players are as below.");
                s.Append("\nBet Placement was below:");
                s.Append("\nJoe : " + selectedWorm1.Name + ", Amount : $" + betAmount1);
                s.Append("\nBob : " + selectedWorm2.Name + ", Amount : $" + betAmount2);
                s.Append("\nAl : " + selectedWorm3.Name + ", Amount : $" + betAmount3);
                s.Append("\n Click Start");
                textBox1.Text = s.ToString();
            }
        }

        private void ListBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void bets1_Checked(object sender, RoutedEventArgs e)
        {

            switch (((RadioButton)sender).Name)
            {
                case "bets1":
                    if (CurrentGameState1 == GameStates.SelectWorm)
                        betdoller.IsEnabled = false;
                    else if (CurrentGameState1 == GameStates.ReadyToStart)
                    {
                        betdoller.IsEnabled = false;
                        buttonConfirm.Visibility = Visibility.Hidden;
                    }
                    else if (CurrentGameState1 == GameStates.SelectBet)
                    {
                        buttonConfirm.Visibility = Visibility.Visible;
                    }
                    bettor = selectedbettor.joe;
                    if (betdoller != null) betdoller.Maximum = bettor1;
                    if (betsMesage != null) betsMesage.Content = "Max bet is : $ " + bettor1;
                    break;
                case "bets2":
                    if (CurrentGameState2 == GameStates.SelectWorm)
                        betdoller.IsEnabled = false;
                    else if (CurrentGameState1 == GameStates.ReadyToStart)
                    {
                        betdoller.IsEnabled = false;
                        buttonConfirm.Visibility = Visibility.Hidden;
                    }
                    else if (CurrentGameState1 == GameStates.SelectBet)
                    {
                        buttonConfirm.Visibility = Visibility.Visible;
                    }
                    bettor = selectedbettor.bob;
                    if (betdoller != null) betdoller.Maximum = bettor2;
                    if (betsMesage != null) betsMesage.Content = "Max bet is : $ " + bettor2;
                    break;
                case "bets3":
                    if (CurrentGameState3 == GameStates.SelectWorm)
                        betdoller.IsEnabled = false;
                    else if (CurrentGameState1 == GameStates.ReadyToStart)
                    {
                        betdoller.IsEnabled = false;
                        buttonConfirm.Visibility = Visibility.Hidden;
                    }
                    else if (CurrentGameState1 == GameStates.SelectBet)
                    {
                        buttonConfirm.Visibility = Visibility.Visible;
                    }
                    bettor = selectedbettor.al;
                    if (betdoller != null) betdoller.Maximum = bettor3;
                    if (betsMesage != null) betsMesage.Content = "Max bet is : $ " + bettor3;
                    break;
            }
        }

        private void betdoller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (bets1.IsChecked == true)
            {
                if (CurrentGameState1 != GameStates.SelectBet)
                {
                    String s = "Game is not ready to Start!, Please Follow The Message Below";
                    System.Windows.MessageBox.Show(s);
                    return;
                }
            }
            else if (bets2.IsChecked == true)
            {
                if (CurrentGameState2 != GameStates.SelectBet)
                {
                    String s = "Game is not ready to Start!, Please Follow The Message Below";
                    System.Windows.MessageBox.Show(s);
                    return;
                }
            }
            else if (bets3.IsChecked == true)
            {
                if (CurrentGameState3 != GameStates.SelectBet)
                {
                    String s = "Game is not ready to Start!, Please Follow The Message Below";
                    System.Windows.MessageBox.Show(s);
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select your player first to initiate game.");
                return;
            }

            switch (bettor)
            {
                case selectedbettor.joe:
                    betAmount1 = (int)betdoller.Value;
                    FirstBet.Content = "Joe bets $" + betdoller.Value + " on " + selectedWorm1.Name;

                    break;
                case selectedbettor.bob:
                    betAmount2 = (int)betdoller.Value;
                    SecondBet.Content = "Bob bets $" + betdoller.Value + " on " + selectedWorm2.Name;
                    break;
                case selectedbettor.al:
                    betAmount3 = (int)betdoller.Value;
                    ThirdBet.Content = "Al bets $" + betdoller.Value + " on " + selectedWorm3.Name;
                    break;
            }

        }

        private void beton_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
