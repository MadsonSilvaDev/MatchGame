using System.Text;
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
        int tenthesOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthesOfSecondsElapsed++;
            timeTextBlock.Text = (tenthesOfSecondsElapsed / 10f).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> aninalEmoji = new List<string>() //cria uma lista de 8 pares de emojis
            {
                "🐋","🐋", "🐒","🐒", "🦥","🦥", "🦒","🦒", "🦆","🦆", "🦋","🦋", "🐘","🐘", "🐢","🐢",
            };
            
            Random random = new Random(); //cria um novo gerador de números aleatórios

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) //localiza cada TextBlock na grade principal e repete a declaração para cada um
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(aninalEmoji.Count); //escolhe um número aleatório entre 0 e o número do emoji da lista e o chama de "index"
                    string nextEmoji = aninalEmoji[index]; //usa o número aleatório chamado "index" para obter um emoji aleatório na lista
                    textBlock.Text = nextEmoji; //atualiza o Textblock com o emoji aleatório na lista
                    aninalEmoji.RemoveAt(index); //remove o emoji aleatório da lista
                }
                
            }

            timer.Start();
            tenthesOfSecondsElapsed = 0;
            matchesFound = 0;

        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false; //controla se o jogador clicou ou não no primeiro animal e agora tenta encontrar a combinção

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false) //o jogador clicou no primeiro animal em um par, então esse animal fica invisível e controla seu TextBlock caso precise ficar visível de novo
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text) //o jogador encontrou uma combinção! Ele torna o segundo animal no par invisível (e não clicável) também e redefine findingMatch para que o próximo animal clicado seja o primeiro par de novo
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else //o jogador clicou em um animal que não combina, então ele torna visível o primeiro animal clicado de novo e redefine findingMatch
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8) //redefine o jogo se os oitos pares combinados forem encontrados (do contrário, não faz nada porque o jogo continua)
            {
                SetUpGame();
            }
        }
    }
}