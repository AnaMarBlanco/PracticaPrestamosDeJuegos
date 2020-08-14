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
    /// Interaction logic for ConsultaPrestamos.xaml
    /// </summary>
    public partial class ConsultaPrestamos : Window
    {
        public string[] combo = new string[] { "Todo", "PrestamoId","AmigoId", "Observacion", "Cantidad"};
        public ConsultaPrestamos()
        {
            InitializeComponent();
            FiltroComboBox.ItemsSource = combo;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Prestamos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PrestamosBLL.GetList(e => true);
                        break;

                    case 1:
                        listado = PrestamosBLL.GetList(e => e.PrestamoId == Convert.ToInt32(CriterioTextBox.Text));
                        break;

                    case 2:
                        listado = PrestamosBLL.GetList(e => e.AmigoId == Convert.ToInt32(CriterioTextBox.Text));
                        break;

                    case 3:
                        listado = PrestamosBLL.GetList(e => e.Observacion.Contains(CriterioTextBox.Text));
                        break;

                    case 4:
                        listado = PrestamosBLL.GetList(e => e.CantidadJuegos == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = PrestamosBLL.GetList(c => true);
            }

            if (DesdeDataPicker.SelectedDate != null)
                listado = PrestamosBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = PrestamosBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
