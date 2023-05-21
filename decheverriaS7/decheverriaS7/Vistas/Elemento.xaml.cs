using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace decheverriaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        public int idSeleccionado;
        private SQLiteAsyncConnection connection;
        IEnumerable<Estudiantes> RActualizar;
        IEnumerable<Estudiantes> REliminar;
        public Elemento(int id, string nombre, string usuario, string contrasena)
        {
            InitializeComponent();
            txtId.Text = id.ToString();
            txtNombre.Text = nombre;
            txtUsuario.Text = usuario;
            txtContrasena.Text = contrasena;
            connection = DependencyService.Get<DataBase>().GetConnection();
            idSeleccionado = id;
        }
        public static IEnumerable<Estudiantes> Eliminar(SQLiteConnection db, int id)
        {
            return db.Query<Estudiantes>("DELETE FROM Estudiantes where id = ?", id);
        }

        public static IEnumerable<Estudiantes> Actualizar(SQLiteConnection db, string nombre, string usuario, string contrasena, int id)
        {
            return db.Query<Estudiantes>("UPDATE Estudiantes set Nombre = ?, Usuario = ?, Contrasena = ? where id = ?", nombre, usuario, contrasena,id);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                RActualizar = Actualizar(db, txtNombre.Text, txtUsuario.Text, txtContrasena.Text, idSeleccionado);
                Navigation.PushAsync(new ConsultaRegistros());
            }
            catch (Exception ex)
            {
                DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                REliminar = Eliminar(db, idSeleccionado);
                Navigation.PushAsync(new ConsultaRegistros());
            }
            catch (Exception ex)
            {
                DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }
    }
}