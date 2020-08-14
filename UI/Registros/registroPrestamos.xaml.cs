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
    /// Interaction logic for registroPrestamos.xaml
    /// </summary>
    public partial class registroPrestamos : Window
    {
        Prestamos Prestamo = new Prestamos();
        public registroPrestamos()
        {
            InitializeComponent();
            Prestamo = new Prestamos();
            AmigoComboBox.ItemsSource = AmigosBLL.GetList();
            AmigoComboBox.SelectedValuePath = "AmigoId";
            AmigoComboBox.DisplayMemberPath = "Nombres";
            JuegoComboBox.ItemsSource = JuegosBLL.GetList();
            JuegoComboBox.SelectedValuePath = "JuegoId";
            JuegoComboBox.DisplayMemberPath = "Descripcion";
            this.DataContext = Prestamo;
        }

        private void Limpiar()
        {
            Prestamo = new Prestamos();
            this.DataContext = Prestamo;
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Prestamo;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            paso = PrestamosBLL.Guardar(Prestamo);
            
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Prestamos existe = PrestamosBLL.Buscar(Prestamo.PrestamoId);

            if (existe == null)
            {
                MessageBox.Show("No existe la Prestamo en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                PrestamosBLL.Eliminar(Prestamo.PrestamoId);
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                Prestamo.Detalles.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }

            if(DetalleDataGrid.SelectedIndex==0)
            {
                MessageBox.Show("Debe seleccionar la fila a remover", "Exito", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool ValidarJuego()
        {
            bool paso = true;
            int id = Convert.ToInt32(JuegoComboBox.SelectedIndex);
            id = id + 1;
            
            Juegos juego = JuegosBLL.Buscar(id);

            if(Convert.ToInt32(JuegoComboBox.SelectedIndex+1)<=0 )
            {
                paso = false;
                AgregarFilaButton.IsEnabled = false;
                MessageBox.Show("Debe haber almenos 1 Juego elegido", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if (CantidadTextBox.Text.Length==0)
            {
                paso = false;
                AgregarFilaButton.IsEnabled = false;
                MessageBox.Show("Debe Elejir una cantidad Apropiada", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if (Convert.ToInt32(CantidadTextBox.Text)>juego.Existencia)
            {
                paso = false;
                AgregarFilaButton.IsEnabled = false;
                MessageBox.Show("La cantidad que escogio es mayor a la existente", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if (!Regex.IsMatch(CantidadTextBox.Text,@"^[0-9]+$"))
            {
                paso = false;
                AgregarFilaButton.IsEnabled = false;
                MessageBox.Show("Debe agregar una cantidad", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            return paso;
        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidarJuego())
            { return; }
            Juegos juego = JuegosBLL.Buscar(Convert.ToInt32(JuegoComboBox.SelectedIndex +1));
            if(juego!=null)
            {
                
                PrestamosDetalle prd;
                Prestamo.Detalles.Add(
                    prd = new PrestamosDetalle
                    {
                        PrestamoId = Prestamo.PrestamoId,
                        JuegoId = JuegoComboBox.SelectedIndex + 1,
                        Cantidad = Convert.ToInt32(CantidadTextBox.Text),
                        Descripcion = juego.Descripcion
                    }
                    ) ;
                Prestamo.CantidadJuegos += prd.Cantidad;

                juego.Existencia -= prd.Cantidad;
                CantidadJuegosTextBox.Text = Prestamo.CantidadJuegos.ToString();
                
                Cargar();
            }
            
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Prestamos encontrado = PrestamosBLL.Buscar(Prestamo.PrestamoId);

            if (encontrado != null)
            {
                Prestamo = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Prestamo no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
