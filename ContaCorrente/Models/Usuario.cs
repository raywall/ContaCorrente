using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContaCorrente.Models
{
    public class Usuario
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Ativo { get; set; }
    }
}