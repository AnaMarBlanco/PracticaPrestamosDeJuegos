using PracticaPrestamosDeJuegos.BLL;
using PracticaPrestamosDeJuegos.Entidades;
using PracticaPrestamosDeJuegoss.BLL;
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
    /// Interaction logic for RegistroEntradas.xaml
    /// </summary>
    public partial class RegistroEntradas : Window
    {
        Entradas Entrada = new Entradas();
        public RegistroEntradas()
        {
            InitializeComponent();
            JuegoIdComboBox.ItemsSource = JuegosBLL.GetList();
            JuegoIdComboBox.SelectedValuePath = "JuegoId";
            JuegoIdComboBox.DisplayMemberPath = "Descripcion";
            Entrada = new Entradas();
            this.DataContext = Entrada;
        }

        private bool validar()
        {
            bool paso = true;

            if (!Regex.IsMatch(EntradaIdTextBox.Text, @"^[0-9]+$"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("En este campo solo se permiten numeros", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                EntradaIdTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }



            if (ExistenciaTextBox.Text.Length == 0 || Convert.ToInt32(ExistenciaTextBox.Text) <= 0 || !Regex.IsMatch(ExistenciaTextBox.Text, @"^[0-9]+$"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("En este campo debe ingresar un valor numerico mayor a 0", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                ExistenciaTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }




            return paso;
        }

        private void Limpiar()
        {
            Entrada = new Entradas();
            this.DataContext = Entrada;

        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!validar())
            {
                return;
            }

            bool paso = EntradasBLL.Guardar(Entrada);

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
            var juego = EntradasBLL.Buscar(Convert.ToInt32(EntradaIdTextBox.Text));
            if (juego != null)
            {
                Entrada = juego;
                MessageBox.Show("Juego encontrado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                Entrada = new Entradas();
                MessageBox.Show("Este Entrada no existe!", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = Entrada;
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = EntradasBLL.Eliminar(Convert.ToInt32(EntradaIdTextBox.Text));

            if (paso)
            {
                Entrada = new Entradas();
                Limpiar();
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
