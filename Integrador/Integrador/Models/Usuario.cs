using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Usuario
    {
        string ID;
        string nombre;
        string apellido_P;
        string apellido_M;
        string fecha_N;
        int t_Usuario;
        string t_Usuario_l;
        string email;
        string pregunta;
        string respuesta;
        string imagen;
        string password;
        string manager;

        public string ID1 { get => ID; set => ID = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido_P { get => apellido_P; set => apellido_P = value; }
        public string Apellido_M { get => apellido_M; set => apellido_M = value; }
        public string Fecha_N { get => fecha_N; set => fecha_N = value; }
        public int T_Usuario { get => t_Usuario; set => t_Usuario = value; }
        public string T_Usuario_l { get => t_Usuario_l; set => t_Usuario_l = value; }
        public string Email { get => email; set => email = value; }
        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
        public string Imagen { get => imagen; set => imagen = value; }
        public string Password { get => password; set => password = value; }
        public string Manager { get => manager; set => manager = value; }
    }
}