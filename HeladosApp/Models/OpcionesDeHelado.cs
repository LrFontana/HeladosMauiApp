using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.Models
{
    public partial class OpcionesDeHelado : ObservableObject
    {
        // Propiedades.
        public string Sabor {  get; set; }
        public string Agregado { get; set; }

        [ObservableProperty]
        private bool _estaSeleccionado;
    }
}
