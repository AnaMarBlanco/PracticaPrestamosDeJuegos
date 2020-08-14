using Microsoft.EntityFrameworkCore.Internal;
using PracticaPrestamosDeJuegos.BLL;
using PracticaPrestamosDeJuegos.DAL;
using PracticaPrestamosDeJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    /// Interaction logic for RegistroAmigos.xaml
    /// </summary>
    public partial class RegistroAmigos : Window
    {
        Amigos Amigo = new Amigos();
        public RegistroAmigos()
        {
            InitializeComponent();
            Amigo = new Amigos();
            this.DataContext = Amigo;
        }

        private bool validar()
        {
            bool paso = true;

            if (!Regex.IsMatch(AmigoIdTextBox.Text, @"^[0-9]+$"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("En este campo solo se permiten numeros", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                AmigoIdTextBox.Focus();
                GuardarButton.IsEnabled = true;

            }

            if(new Contexto().Amigos.Any(p => p.Celular == CelularTextBox.Text) && Convert.ToInt32(AmigoIdTextBox.Text)==0)
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Ya existe un Amigo con este numero", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CelularTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if(new Contexto().Amigos.Any(p=>p.Nombres == NombresTextBox.Text) && Convert.ToInt32(AmigoIdTextBox.Text) == 0)
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Este nombre ya esxiste", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                NombresTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if (!Regex.IsMatch(TelefonoTextBox.Text, @"^(809|829|849)+[0-9]{7}"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Numero invalido", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if (!Regex.IsMatch(CelularTextBox.Text, @"^(809|829|849)+[0-9]{7}"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("ENumero invalido", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CelularTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }



            if (TelefonoTextBox.Text==CelularTextBox.Text)
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("El numero del celular y el telefono no pueden ser iguales", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CelularTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if(new EmailAddressAttribute().IsValid(EmailTextBox.Text)== false)
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Este email noes valido", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                EmailTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

           

            if (NombresTextBox.Text.Length == 0)
            {
                GuardarButton.IsEnabled = false;
                MessageBox.Show("El nombre no puede estar vacio", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                AmigoIdTextBox.Focus();
            }
            if (!Regex.IsMatch(NombresTextBox.Text, @"^[A-Za-z ]+$"))
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Los nombres solo tienen textos ", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                NombresTextBox.Focus();
                GuardarButton.IsEnabled = true;

            }

            if (NombresTextBox.Text=="Mamaguevo")
            {
                paso = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Aqui no aceptamos comediantes crack", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                AmigoIdTextBox.Focus();
                GuardarButton.IsEnabled = true;

            }



            return paso;
        }

        private void Limpiar()
        {
            Amigo = new Amigos();
            this.DataContext = Amigo;

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

            bool paso = AmigosBLL.Guardar(Amigo);

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
            var amigo = AmigosBLL.Buscar(Convert.ToInt32(AmigoIdTextBox.Text));
            if (amigo != null)
            {
                Amigo = amigo;
                MessageBox.Show("Amigo encontrado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                Amigo = new Amigos();
                MessageBox.Show("Este Amigo no existe!", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = Amigo;
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = AmigosBLL.Eliminar(Convert.ToInt32(AmigoIdTextBox.Text));

            if (paso)
            {
                Amigo = new Amigos();
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
