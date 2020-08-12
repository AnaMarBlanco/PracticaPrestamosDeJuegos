using PracticaPrestamosDeJuegos.UI.Registros;
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

namespace PracticaPrestamosDeJuegos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RAmigosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RegistroAmigos ra = new RegistroAmigos();
            ra.ShowDialog();
        }

        private void RJuegosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Registrojuegos rj = new Registrojuegos();
            rj.ShowDialog();
        }

        private void RPrestamosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            registroPrestamos rp = new registroPrestamos();
            rp.ShowDialog();
        }
    }
}
