//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Localidad
    {
        public Localidad()
        {
            this.Domicilio = new HashSet<Domicilio>();
        }
    
        public int idLocalidad { get; set; }
        public string nombreLocalidad { get; set; }
        public Nullable<int> idProvincia { get; set; }
        public Nullable<bool> esCercana { get; set; }
    
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual Provincia Provincia { get; set; }
    }
}
