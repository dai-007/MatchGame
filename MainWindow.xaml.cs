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


namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int secondsElapsed;
        int matchesFound;
        TextBlock lastTextBlockClicked;
        bool findingMatch = false; // keep track whether it is a match or not
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            timeBlock.Text = (secondsElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timeBlock.Text = timeBlock.Text + " - Play Again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalMatch = new List<string>()
            {
                "🐒","🐒",
                "🐯","🐯",
                "🐸","🐸",
                "🦓","🦓",
                "🐲","🐲",
                "🦬","🦬",
                "🐢","🐢",
                "🐀","🐀",
            };
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalMatch.Count);
                    string nextAnimal = animalMatch[index];
                    textBlock.Text = nextAnimal;
                    animalMatch.RemoveAt(index);
                }
            }
            timer.Start();
            secondsElapsed = 0;
            matchesFound = 0;
        }
        
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // This reset the game if all 8 pairs matched
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
