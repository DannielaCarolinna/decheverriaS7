using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace decheverriaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection connection;
        public Registro()
        {
            InitializeComponent();
            connection = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var datos = new Estudiantes
                {
                    Nombre = txtNombre.Text,
                    Usuario = txtUsuario.Text,
                    Contrasena = txtConstrasena.Text
                };
                connection.InsertAsync(datos);
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtConstrasena.Text = "";
            }
            catch (Exception ex) 
            {
                DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }
    }
}