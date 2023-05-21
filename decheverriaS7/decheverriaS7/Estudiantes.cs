using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace decheverriaS7
{
    public class Estudiantes
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Usuario { get; set; }
        [MaxLength(50)]
        public string Contrasena { get; set; }
    }
}
