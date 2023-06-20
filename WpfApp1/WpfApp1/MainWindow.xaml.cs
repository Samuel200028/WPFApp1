using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<Persona> personas;
        private ObservableCollection<Persona> personas;
        bool elim = false, act = false;

        public MainWindow()
        {
            InitializeComponent();

            //personas = new List<Persona>();
            Persona p = new Persona();
            personas = new ObservableCollection<Persona>();

            personas.Add(new Persona
            {
                Id = 1,
                Nombre = "Samuel",
                Edad = 23,
                Correo = "samuel@gmail.com"
            });
            personas.Add(new Persona
            {
                Id = 2,
                Nombre = "Samuel2",
                Edad = 23,
                Correo = "samuel2@gmail.com"
            });
            this.dataGrid.ItemsSource = personas;
        }

        class Persona
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Edad { get; set; }
            public String Correo { get; set; }
        }

        private void Guardar(object sender, RoutedEventArgs e)
        {
            String nombre = Nombre.Text, idT = Id.Text, edadT = Edad.Text;
            String correo = Correo.Text;


            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(idT) || string.IsNullOrEmpty(edadT) || string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Faltan algunos datos");
            }
            else
            {
                if (!int.TryParse(idT, out _) || !int.TryParse(edadT, out _))
                {
                    MessageBox.Show("La casilla Id o Edad no es un entero");
                }
                else
                {
                    int id = int.Parse(Id.Text);
                    int edad = int.Parse(Edad.Text);

                    Persona nPersona = new Persona
                    {
                        Id = id,
                        Nombre = nombre,
                        Edad = edad,
                        Correo = correo
                    };
                    if (act == true)
                    {

                        DataGridCellInfo selectedCell = dataGrid.SelectedCells.FirstOrDefault();
                        if (selectedCell.Item != null)
                        {
                            int idParaEliminar = (int)selectedCell.Item.GetType().GetProperty("Id").GetValue(selectedCell.Item);
                            Persona personaParaEliminar = personas.FirstOrDefault(p => p.Id == idParaEliminar);

                            //int nombreBuscado = 2;

                            Persona personaActualizar = personas.FirstOrDefault(p => p.Id == idParaEliminar);

                            if (personaActualizar != null)
                            {
                                personaActualizar.Nombre = Nombre.Text;
                                personaActualizar.Edad = int.Parse(Edad.Text); ;
                                personaActualizar.Id = int.Parse(Id.Text); ;
                                personaActualizar.Correo = Correo.Text;
                            }
                        }
                        //MessageBox.Show("Datos actualizados");
                    }
                    else
                    {
                        personas.Add(nPersona);
                    }
                    act = false;
                }
            }
            Nuevo(sender, e);
        }

        private void Nuevo(object sender, RoutedEventArgs e)
        {
            Nombre.Text = string.Empty;
            Edad.Text = string.Empty;
            Id.Text = string.Empty;
            Correo.Text = string.Empty;
            act = false;
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {


            if(elim == true)
            {
                DataGridCellInfo selectedCell = dataGrid.SelectedCells.FirstOrDefault();
                if (selectedCell.Item != null)
                {
                    //int indiceParaEliminar = (int)selectedCell.Item.GetType().GetProperty("Id").GetValue(selectedCell.Item);
                    //if (indiceParaEliminar >= 0 && indiceParaEliminar < personas.Count)
                    //{
                    //    personas.RemoveAt(indiceParaEliminar);
                    //}
                    int idParaEliminar = (int)selectedCell.Item.GetType().GetProperty("Id").GetValue(selectedCell.Item);
                    Persona personaParaEliminar = personas.FirstOrDefault(p => p.Id == idParaEliminar);

                    if (personaParaEliminar != null)
                    {
                        MessageBoxResult resE = MessageBox.Show("¿Estás seguro de que deseas eliminar este elemento?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (resE == MessageBoxResult.Yes)
                        {
                            personas.Remove(personaParaEliminar);
                        }
                    }
                    elim = false;
                }
            }
            else
            {
                MessageBox.Show("No has seleccionado una celda");
            }

        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGridCellInfo selectedCell = dataGrid.SelectedCells.FirstOrDefault();
            if (selectedCell.Item != null)
            {
                object cellValue = selectedCell.Item.GetType().GetProperty("Id").GetValue(selectedCell.Item);
                elim = true; 
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedIndex = dataGrid.SelectedIndex;

            DataGridCellInfo selectedCell = dataGrid.SelectedCells.FirstOrDefault();
            if (selectedCell.Item != null)
            {
                int edad = ((int) selectedCell.Item.GetType().GetProperty("Edad").GetValue(selectedCell.Item));
                Edad.Text = edad.ToString();
                int number = (int) selectedCell.Item.GetType().GetProperty("Id").GetValue(selectedCell.Item);
                Id.Text = number.ToString();
                String nombre = (String)selectedCell.Item.GetType().GetProperty("Nombre").GetValue(selectedCell.Item);
                Nombre.Text = nombre;
                String correo = (String)selectedCell.Item.GetType().GetProperty("Correo").GetValue(selectedCell.Item);
                Correo.Text = correo;

                act = true;

            }
        }





        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            
           
        }
    }
}
