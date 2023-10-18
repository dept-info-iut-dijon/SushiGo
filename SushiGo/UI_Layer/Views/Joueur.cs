using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.Views
{
    public class Joueur
    {

        private string name;
        private string role;

        public string Name { get => name; set => name = value; }
        public string Role { get => role; set => role = value; }

        public Joueur(string name, string role = "joueur")
        {
            this.name = name;
            this.role = role;
        }
    }
}
