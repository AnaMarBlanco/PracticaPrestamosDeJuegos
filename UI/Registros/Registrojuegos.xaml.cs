using PracticaPrestamosDeJuegos.BLL;
using PracticaPrestamosDeJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaPrestamosDeJuegos.UI.Registros
{
    /// <summary>
    /// Interaction logic for Registrojuegos.xaml
    /// </summary>
    /// 
    public partial class Registrojuegos : Window
    {
        Juegos Juego = new Juegos();
        public Registrojuegos()
        {
            InitializeComponent();
            Juego = new Juegos();
            this.DataContext = Juego;
        }

        private bool validar()
        {
            bool paso = true;

            if(!Regex.IsMatch(JuegoIdTextBox.Text,@"^[0-9]+$"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("En este campo solo se permiten numeros", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

           

            if (DescripcionTextBox.Text.Length==0)
            {
                GuardarButton.IsEnabled = false;
                MessageBox.Show("La descripcion no puede estar vacia", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdTextBox.Focus();
            }
            if (PrecioTextBox.Text.Length == 0 || Convert.ToDouble(PrecioTextBox.Text)<0)
            {
                GuardarButton.IsEnabled = false;
                MessageBox.Show("El precio no puede estar vacio o ser menor o igual a" +
                    "0", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdTextBox.Focus();
            }

            return paso;
        }

        private void Limpiar()
        {
            Juego = new Juegos();
            this.DataContext = Juego;

        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if(!validar())
            {
                return;
            }

            bool paso = JuegosBLL.Guardar(Juego);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccion exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else { MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var juego = JuegosBLL.Buscar(Convert.ToInt32(JuegoIdTextBox.Text));
            if(juego != null)
            {
                Juego = juego;
                MessageBox.Show("Juego encontrado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                Juego = new Juegos();
                MessageBox.Show("Este juego no existe!", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = Juego;
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = JuegosBLL.Eliminar(Convert.ToInt32(JuegoIdTextBox.Text));

            if (paso)
            {
                Juego = new Juegos();
                MessageBox.Show("eliminado correctamente", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error no se pudo Eliminar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
