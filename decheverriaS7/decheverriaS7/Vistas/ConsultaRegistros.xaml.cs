using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace decheverriaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistros : ContentPage
    {
        private SQLiteAsyncConnection connection;
        private ObservableCollection<Estudiantes> tablaEstudiante;
        public ConsultaRegistros()
        {
            InitializeComponent();
            connection = DependencyService.Get<DataBase>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
            ObtenerEstudiantes();
        }

        public async void ObtenerEstudiantes()
        {
            var ResultEstudiantes = await connection.Table<Estudiantes>().ToListAsync();
            tablaEstudiante = new ObservableCollection<Estudiantes>(ResultEstudiantes);
            ListaEstudiantes.ItemsSource = tablaEstudiante;

        }
        


        private void ListaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objetoEstudiante = (Estudiantes)e.SelectedItem;
            var ItemId = objetoEstudiante.Id.ToString();
            int id = Convert.ToInt32(ItemId);
            string nombre = objetoEstudiante.Nombre.ToString();
            string usuario = objetoEstudiante.Usuario.ToString();
            string contrasena = objetoEstudiante.Contrasena.ToString();
            Navigation.PushAsync(new Elemento(id, nombre, usuario, contrasena));
            
        }

        private void btSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}