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
    /// Interaction logic for ConsultaJuegos.xaml
    /// </summary>
    public partial class ConsultaJuegos : Window
    {
        public string[] combo = new string[] {"Todo", "JuegoId", "Descripcion", "Precio" ,"Existencia"};
        public ConsultaJuegos()
        {
            InitializeComponent();
            FiltroComboBox.ItemsSource = combo;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Juegos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = JuegosBLL.GetList(e => true);
                        break;

                    case 1:
                        listado = JuegosBLL.GetList(e => e.JuegoId == Convert.ToInt32(CriterioTextBox.Text));
                        break;

                    case 2:
                        listado = JuegosBLL.GetList(e => e.Descripcion.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;

                    case 3:
                        listado = JuegosBLL.GetList(e => e.Precio == Convert.ToDecimal(CriterioTextBox.Text));
                        break;
                    case 4:
                        listado = JuegosBLL.GetList(e => e.Existencia==Convert.ToInt32(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = JuegosBLL.GetList(c => true);
            }

            if (DesdeDataPicker.SelectedDate != null)
                listado = JuegosBLL.GetList(c => c.FechaCompra.Date >= DesdeDataPicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = JuegosBLL.GetList(c => c.FechaCompra.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
    
}
