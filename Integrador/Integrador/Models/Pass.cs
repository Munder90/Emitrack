using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Pass
    {
        string id;
        string apass;
        string npass1;
        string npass2;

        public string Id { get => id; set => id = value; }
        public string Apass { get => apass; set => apass = value; }
        public string Npass1 { get => npass1; set => npass1 = value; }
        public string Npass2 { get => npass2; set => npass2 = value; }
    }
}