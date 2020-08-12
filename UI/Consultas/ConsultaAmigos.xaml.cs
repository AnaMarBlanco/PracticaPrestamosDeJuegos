using PracticaPrestamosDeJuegos.BLL;
using PracticaPrestamosDeJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaPrestamosDeJuegos.UI.Consultas
{
    /// <summary>
    /// Interaction logic for ConsultaAmigos.xaml
    /// </summary>
    public partial class ConsultaAmigos : Window
    {
        public string[] combo = new string[] {"Todo", "ID", "Nombres", "Direccion", "Telefono","Celular", "Email"};
        public ConsultaAmigos()
        {
            InitializeComponent();
            FiltroComboBox.ItemsSource = combo;
        }
        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Amigos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = AmigosBLL.GetList(e => true);
                        break;

                    case 1:
                        listado = AmigosBLL.GetList(e => e.AmigoId == Convert.ToInt32(CriterioTextBox.Text));
                        break;

                    case 2:
                        listado = AmigosBLL.GetList(e => e.Nombres.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;

                    case 3:
                        listado = AmigosBLL.GetList(e => e.Direccion.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                    case 4:
                        listado = AmigosBLL.GetList(e => e.Telefono.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;

                    case 5:
                        listado = AmigosBLL.GetList(e => e.Celular.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                    case 6:
                        listado = AmigosBLL.GetList(e => e.Email.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                }
            }
            else
            {
                listado = AmigosBLL.GetList(c => true);
            }

            if (DesdeDataPicker.SelectedDate != null)
                listado = AmigosBLL.GetList(c => c.FechaNacimiento.Date >= DesdeDataPicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = AmigosBLL.GetList(c => c.FechaNacimiento.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
