using hmm.Page;
using System.Windows;

namespace hmm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PageNav.Content = new Page1();
        }
    }
}
