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
    
    public partial class TipoTramite
    {
        public TipoTramite()
        {
            this.Tramite = new HashSet<Tramite>();
        }
    
        public int idTipoTramite { get; set; }
        public string nombreTipoTramite { get; set; }
        public string descripcion { get; set; }
    
        public virtual ICollection<Tramite> Tramite { get; set; }
    }
}
