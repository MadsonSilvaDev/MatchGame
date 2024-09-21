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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetUpGame();
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
                int index = random.Next(aninalEmoji.Count); //escolhe um número aleatório entre 0 e o número do emoji da lista e o chama de "index"
                string nextEmoji = aninalEmoji[index]; //usa o número aleatório chamado "index" para obter um emoji aleatório na lista
                textBlock.Text = nextEmoji; //atualiza o Textblock com o emoji aleatório na lista
                aninalEmoji.RemoveAt(index); //remove o emoji aleatório da lista
            }
        }
    }
}