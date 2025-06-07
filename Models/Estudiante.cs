using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichardTipantuñaComplementario.Models
{
    public class Estudiante
    {
        public int Cod_Estudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Curso { get; set; }
        public string Paralelo { get; set; }
        public float Nota_Final { get; set; }
    }
}
