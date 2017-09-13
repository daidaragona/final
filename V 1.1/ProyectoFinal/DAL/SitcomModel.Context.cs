﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SitcomEntities : DbContext
    {
        public SitcomEntities()
            : base("name=SitcomEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Caracteristica> Caracteristica { get; set; }
        public virtual DbSet<CaracteristicasHospedaje> CaracteristicasHospedaje { get; set; }
        public virtual DbSet<CasaDptoOCabana> CasaDptoOCabana { get; set; }
        public virtual DbSet<CategoriaCaracteristicas> CategoriaCaracteristicas { get; set; }
        public virtual DbSet<CategoriaHospedaje> CategoriaHospedaje { get; set; }
        public virtual DbSet<Comercio> Comercio { get; set; }
        public virtual DbSet<Complejo> Complejo { get; set; }
        public virtual DbSet<Disponibilidad> Disponibilidad { get; set; }
        public virtual DbSet<Domicilio> Domicilio { get; set; }
        public virtual DbSet<EstadoReserva> EstadoReserva { get; set; }
        public virtual DbSet<EstadoTramite> EstadoTramite { get; set; }
        public virtual DbSet<FotosNegocio> FotosNegocio { get; set; }
        public virtual DbSet<FotosUsuario> FotosUsuario { get; set; }
        public virtual DbSet<Habitacion> Habitacion { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Localidad> Localidad { get; set; }
        public virtual DbSet<LugarHospedaje> LugarHospedaje { get; set; }
        public virtual DbSet<Negocio> Negocio { get; set; }
        public virtual DbSet<Paginas> Paginas { get; set; }
        public virtual DbSet<PaginasXPerfil> PaginasXPerfil { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Perfiles> Perfiles { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }
        public virtual DbSet<TipoCaracteristica> TipoCaracteristica { get; set; }
        public virtual DbSet<TipoComplejo> TipoComplejo { get; set; }
        public virtual DbSet<TipoDeNegocio> TipoDeNegocio { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoHabitacion> TipoHabitacion { get; set; }
        public virtual DbSet<TipoLugarHospedaje> TipoLugarHospedaje { get; set; }
        public virtual DbSet<TipoTramite> TipoTramite { get; set; }
        public virtual DbSet<Tramite> Tramite { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    
        public virtual int cambiarCasaODptoNuevoComplejo(Nullable<int> idNegocioNuevo)
        {
            var idNegocioNuevoParameter = idNegocioNuevo.HasValue ?
                new ObjectParameter("idNegocioNuevo", idNegocioNuevo) :
                new ObjectParameter("idNegocioNuevo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("cambiarCasaODptoNuevoComplejo", idNegocioNuevoParameter);
        }
    
        public virtual ObjectResult<cambiarHabitacionesNuevoHotel_Result> cambiarHabitacionesNuevoHotel(Nullable<int> idNegocioNuevo)
        {
            var idNegocioNuevoParameter = idNegocioNuevo.HasValue ?
                new ObjectParameter("idNegocioNuevo", idNegocioNuevo) :
                new ObjectParameter("idNegocioNuevo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<cambiarHabitacionesNuevoHotel_Result>("cambiarHabitacionesNuevoHotel", idNegocioNuevoParameter);
        }
    
        public virtual int getDisponibilidadCasaDpto(Nullable<int> idCasa, Nullable<int> anio, Nullable<int> mes)
        {
            var idCasaParameter = idCasa.HasValue ?
                new ObjectParameter("idCasa", idCasa) :
                new ObjectParameter("idCasa", typeof(int));
    
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getDisponibilidadCasaDpto", idCasaParameter, anioParameter, mesParameter);
        }
    
        public virtual int getDisponibilidadComplejo(Nullable<int> idComplejo, Nullable<int> cantidadCasasSolicitadas, Nullable<int> cantidadPersonasSolicitadas, Nullable<int> anio, Nullable<int> mes)
        {
            var idComplejoParameter = idComplejo.HasValue ?
                new ObjectParameter("idComplejo", idComplejo) :
                new ObjectParameter("idComplejo", typeof(int));
    
            var cantidadCasasSolicitadasParameter = cantidadCasasSolicitadas.HasValue ?
                new ObjectParameter("cantidadCasasSolicitadas", cantidadCasasSolicitadas) :
                new ObjectParameter("cantidadCasasSolicitadas", typeof(int));
    
            var cantidadPersonasSolicitadasParameter = cantidadPersonasSolicitadas.HasValue ?
                new ObjectParameter("cantidadPersonasSolicitadas", cantidadPersonasSolicitadas) :
                new ObjectParameter("cantidadPersonasSolicitadas", typeof(int));
    
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getDisponibilidadComplejo", idComplejoParameter, cantidadCasasSolicitadasParameter, cantidadPersonasSolicitadasParameter, anioParameter, mesParameter);
        }
    
        public virtual int getDisponibilidadHabitacion(Nullable<int> idHabitacion, Nullable<int> anio, Nullable<int> mes)
        {
            var idHabitacionParameter = idHabitacion.HasValue ?
                new ObjectParameter("idHabitacion", idHabitacion) :
                new ObjectParameter("idHabitacion", typeof(int));
    
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getDisponibilidadHabitacion", idHabitacionParameter, anioParameter, mesParameter);
        }
    
        public virtual int getDisponibilidadHotel(Nullable<int> idHotel, Nullable<int> cantidadHabitacionesSolicitadas, Nullable<int> cantidadPersonasSolicitadas, Nullable<int> anio, Nullable<int> mes)
        {
            var idHotelParameter = idHotel.HasValue ?
                new ObjectParameter("idHotel", idHotel) :
                new ObjectParameter("idHotel", typeof(int));
    
            var cantidadHabitacionesSolicitadasParameter = cantidadHabitacionesSolicitadas.HasValue ?
                new ObjectParameter("cantidadHabitacionesSolicitadas", cantidadHabitacionesSolicitadas) :
                new ObjectParameter("cantidadHabitacionesSolicitadas", typeof(int));
    
            var cantidadPersonasSolicitadasParameter = cantidadPersonasSolicitadas.HasValue ?
                new ObjectParameter("cantidadPersonasSolicitadas", cantidadPersonasSolicitadas) :
                new ObjectParameter("cantidadPersonasSolicitadas", typeof(int));
    
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getDisponibilidadHotel", idHotelParameter, cantidadHabitacionesSolicitadasParameter, cantidadPersonasSolicitadasParameter, anioParameter, mesParameter);
        }
    
        public virtual int ConsultarLugaresHospedaje(Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta, Nullable<int> cantPersonas, Nullable<int> cantHabitaciones, string tipoHospedaje)
        {
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("fechaDesde", fechaDesde) :
                new ObjectParameter("fechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("fechaHasta", fechaHasta) :
                new ObjectParameter("fechaHasta", typeof(System.DateTime));
    
            var cantPersonasParameter = cantPersonas.HasValue ?
                new ObjectParameter("cantPersonas", cantPersonas) :
                new ObjectParameter("cantPersonas", typeof(int));
    
            var cantHabitacionesParameter = cantHabitaciones.HasValue ?
                new ObjectParameter("cantHabitaciones", cantHabitaciones) :
                new ObjectParameter("cantHabitaciones", typeof(int));
    
            var tipoHospedajeParameter = tipoHospedaje != null ?
                new ObjectParameter("tipoHospedaje", tipoHospedaje) :
                new ObjectParameter("tipoHospedaje", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ConsultarLugaresHospedaje", fechaDesdeParameter, fechaHastaParameter, cantPersonasParameter, cantHabitacionesParameter, tipoHospedajeParameter);
        }
    }
}
