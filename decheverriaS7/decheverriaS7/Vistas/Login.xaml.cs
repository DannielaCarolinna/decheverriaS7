using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace decheverriaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection connection;
        public Login()
        {
            InitializeComponent();
            connection = DependencyService.Get<DataBase>().GetConnection();
        }
        public static IEnumerable<Estudiantes> Select_Where(SQLiteConnection db, string usuario,  string contrasena)
        {
            return db.Query<Estudiantes>("Select * FROM Estudiantes Where Usuario =? and Contrasena =?", usuario, contrasena);
        }
    
        private void btnInicio_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                db.CreateTable<Estudiantes>();
                IEnumerable<Estudiantes> resultado = Select_Where(db, TxtUsuario.Text, TxtContrasena.Text);
                if(resultado.Count() > 0) 
                {
                    Navigation.PushAsync(new ConsultaRegistros());
                }
                else
                {
                    DisplayAlert("ALERTA", "Usuario/Contrasena incorrectos", "Cerrar");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnRegistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}