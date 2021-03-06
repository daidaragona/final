﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using DAL;
using BussinesEntities;
using System.Drawing;
using System.IO;

namespace ProyectoFinal.Controllers
{
    public class ReservasController : Controller
    {
        UsuarioEntity usuarioActual;
        SitcomEntities db = new SitcomEntities();
        NegociosManager nm = new NegociosManager();
        UsuariosManager um = new UsuariosManager();
        PersonasManager pm = new PersonasManager();
        ReservasManager rm = new ReservasManager();
        HotelManager hm = new HotelManager();

        // GET: /Reservas/
        public ActionResult Index()
        {
            return View();
        }


        public String AnularReserva(int id)
        {

            hm.reserva_Anular(id);
           
            return "ok";
        }

        public String CheckInReserva(int id)
        {
            hm.reserva_CheckIn(id);
            return "ok";
        }

        public String CheckOutReserva(int id)
        {
            hm.reserva_CheckOut(id);
            return "ok";
        }

        public String Actualizar_Comentarios_Leidos(int id)
        {
            hm.actualizar_Comentarios_Leidos(id);
            return "ok";
        }


        public ActionResult ListadoPlanoReserva(string dias, string mes, int idNegocio, int year)
        {
            var fecha_desde = Convert.ToDateTime("01/" + mes + "/" + year);

            var fecha_hasta = Convert.ToDateTime(dias + "/" + mes + "/" + year);

            var listado = hm.listadoReservasPlaning(fecha_desde, fecha_hasta, idNegocio, true);

            ViewBag.dias = Convert.ToInt32(dias);
            ViewBag.mes = Convert.ToInt32(mes);
            ViewBag.anio = Convert.ToInt32(year);

            return PartialView("ListadoPlanoReservas", listado);
        }



        public ActionResult PlanoReserva(string dias, string mes, int idNegocio, int year)
        {
            var fecha_desde = Convert.ToDateTime("01/" + mes + "/" + year);

            var fecha_hasta = Convert.ToDateTime(dias + "/" + mes + "/" + year);

            var listado = hm.calendario(fecha_desde,fecha_hasta, idNegocio);
            var planoReserva = hm.planoReserva(fecha_desde, fecha_hasta, idNegocio);

            Session["PLANO_RESERVA"] = planoReserva;

            ViewBag.dias = Convert.ToInt32(dias);
            ViewBag.mes = Convert.ToInt32(mes);
            ViewBag.anio = Convert.ToInt32(year);

            return PartialView("PlanoReservas", listado);
        }

        public ActionResult BuscarHabitacionesDisponibles(string fecha_desde, string fecha_hasta, string idNegocio)
        {

            var listado = hm.buscarDisponibilidadHabitaciones(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), Convert.ToInt32(idNegocio));

            return PartialView("BuscarHabitacionesDisponibles", listado);
        }

        public ActionResult BuscarCliente(string buscar, int idNegocio)
        {

            var listado = hm.buscarClientes(buscar, idNegocio);

            return PartialView("BuscarCliente", listado);
        }

        public string RegistrarReserva(int? idPersona, int? idNegocio, int? idSolicitud, int tipo)
        {

            var greListSession = Session["AgregarReservaList"] as List<GestionReservaEntities>;

            decimal id = hm.reserva_i(idPersona, idNegocio, idSolicitud);

            foreach (var item in greListSession)
            {

                if(tipo == 2){
                    hm.disponibilidad_i(item.FechaDesde, item.FechaHasta, null, item.IdHabitacion, Convert.ToInt32(id));
                }else{
                    hm.disponibilidad_i(item.FechaDesde, item.FechaHasta, item.IdHabitacion, null, Convert.ToInt32(id));
                }
                
            };


            return Convert.ToString(id);
        }

        [HttpGet]
        public ActionResult ListadoDeComentario(int idSolicitud)
        {
            var listado = hm.consultarComentariosSolicitud(Convert.ToInt32(idSolicitud));

            return PartialView("ListadoConsultaReserva", listado);
        }

        [HttpPost]
        public ActionResult GuardarConsulta(FormCollection collection)
        {
            var nro_reserva = collection["nro_reserva"];
            var consultareserva = collection["consultareserva"];
            var filereserva = collection["filereserva"];
            var comentarioCliente = collection["comentarioCliente"];

            bool cliente = false;

            if(comentarioCliente == "1"){
                cliente = true;
            };

            Random rnd = new Random();
            var file = Request.Files["filereserva"];

            string nombre_imagen = "";

            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);

                nombre_imagen = Convert.ToString(rnd.Next(300)) + ".jpg";

                filename = Path.Combine(Server.MapPath("~/Content/img"), nombre_imagen);

                file.SaveAs(filename);
            }

            var result = hm.comentariosSolicitud_i(consultareserva, nombre_imagen, Convert.ToInt32(nro_reserva), cliente);

            var listado = hm.consultarComentariosSolicitud(Convert.ToInt32(nro_reserva));


            return PartialView("ListadoConsultaReserva", listado);
        }

        public ActionResult ReservasUsuario()
        {
            ObtenerUsuarioActual();
  
            ViewBag.Perfil = usuarioActual.idPerfil;

            ViewBag.idPerfil = usuarioActual.idPerfil;
            ViewBag.UsuarioActual = usuarioActual;

            var listado = hm.consultarListadoReservasPorPersona(Convert.ToInt32(usuarioActual.idPersona));


            return View(listado);
        }


        public ActionResult AgregarReserva(string fecha_desde, string fecha_hasta, int? habitacion, string accion, string referencia, string habitacion_text)
        {

            if (accion == "agregar")
            {
                GestionReservaEntities gre = new GestionReservaEntities();
                List<GestionReservaEntities> greList = new List<GestionReservaEntities>();

                gre.FechaDesde = Convert.ToDateTime(fecha_desde);
                gre.FechaHasta = Convert.ToDateTime(fecha_hasta);
                gre.HabitacionText = habitacion_text;
                gre.IdHabitacion = habitacion;

                if (Session["AgregarReservaList"] != null)
                {
                    var greListSession = Session["AgregarReservaList"] as List<GestionReservaEntities>;
         
                    var agregar = true;
                    foreach (var item in greListSession)
                    {
                        if (habitacion_text == item.HabitacionText)
                        {
                            agregar = false;
                        }
                    };

                    if (agregar)
                    {
                        greListSession.Add(gre);
                    }

                    Session["AgregarReservaList"] = greListSession;
                }
                else
                {
                    greList.Add(gre);
                    Session["AgregarReservaList"] = greList;
                }

            };


            if(accion == "eliminar"){

                var greListSession = Session["AgregarReservaList"] as List<GestionReservaEntities>;
                List<GestionReservaEntities> newList = new List<GestionReservaEntities>();
                
                foreach (var item in greListSession)
                {
                    if (referencia != item.HabitacionText)
                    {
                        newList.Add(item);
                    }
                };

                Session["AgregarReservaList"] = newList;

            }

    
            return PartialView("AgregarReserva");
        }

        public ActionResult SolicitarModulo(int? idNegocio)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById((int)idNegocio);
            Persona p = pm.GetPersonaById((int)usuarioActual.idPersona);

            ViewBag.Persona = p;

            return View(neg);
        }
        [HttpPost]
        public ActionResult SolicitarModulo(bool aceptaCondiciones, int idNegocio)
        {
            ObtenerUsuarioActual();
            if(aceptaCondiciones == true)
            {
                rm.SolicitarModuloReservas(idNegocio, usuarioActual);
                return RedirectToAction("Index","Home");
            }
            else
            {                
                NegocioEntity neg = nm.GetNegocioById(idNegocio);

                Persona p = pm.GetPersonaById(usuarioActual.idUsuario);
                ViewBag.Persona = p;

                ViewBag.Error = 1;
                return View("SolicitarModulo",neg);
            }
        }
        public ActionResult EvalSolicitudModulo(int id, int idTramite)
        {
            NegocioEntity neg = nm.GetNegocioById(id);
            Persona p = pm.GetPersonaById((int)neg.Usuarios.idPersona);

            ViewBag.Persona = p;
            ViewBag.Tramite = idTramite;

            return View(neg);
        }
        public ActionResult GestionReservas(int idNegocio)
        {
            NegocioEntity neg = nm.GetNegocioById(idNegocio);

            return View(neg);
        }
        public ActionResult SolicitarReserva(int id)
        {
            ObtenerUsuarioActual();
            if(usuarioActual.idPersona != null)
            {
                NegocioEntity neg = nm.GetNegocioById(id);
                Persona p = pm.GetPersonaById((int)usuarioActual.idPersona);

                ViewBag.idTipoLugarHospedaje = neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje;
                ViewBag.idNegocio = neg.idNegocio;
                ViewBag.Persona = p;

                ViewBag.estado = "";

                return View();
            }
            else
            {
                return RedirectToAction("DatosPersonales", "Persona", new { returnUrl = "../Reservas/SolicitarReserva/" + id });
            }
        }
        [HttpPost]
        public ActionResult SolicitarReserva(SolicitudEntity solicitud, string fechaDesde, string fechaHasta, int idNegocio)
        {
            ObtenerUsuarioActual();
            solicitud.fechaDesde = Convert.ToDateTime(fechaDesde);
            solicitud.fechaHasta = Convert.ToDateTime(fechaHasta);
            solicitud.idNegocio = idNegocio;
            solicitud.idUsuarioSolicitante = usuarioActual.idUsuario;

            rm.AddSolicitudReserva(solicitud);

            NegocioEntity neg = nm.GetNegocioById(idNegocio);
            Persona p = pm.GetPersonaById((int)usuarioActual.idPersona);

            ViewBag.idTipoLugarHospedaje = neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje;
            ViewBag.idNegocio = neg.idNegocio;
            ViewBag.Persona = p;

            ViewBag.estado = "ok";


            return View();
        }
        public ActionResult SolicitudesReserva(int idNegocio, int tipo)
        {
            List<SolicitudEntity> solicitudes = rm.GetSolicitudesReserva(idNegocio);
            ViewBag.idNegocio = idNegocio;
            ViewBag.tipo = tipo;

            return View(solicitudes);
        }

 

        public ActionResult VerSolicitudReserva(int? idSolicitud, int? idNegocio, int tipo)
        {
            SolicitudEntity sol = new SolicitudEntity();

            Session["AgregarReservaList"] = null;


            ViewBag.datos_solicitud = "display:none";
            ViewBag.datos_cliente   = "display:none";
            ViewBag.disabled = "";
            ViewBag.idSolicitud = idSolicitud;
            ViewBag.idNegocio = idNegocio;
            ViewBag.tipo = tipo;

            if (idSolicitud != null && idSolicitud > 0)
            {
                ViewBag.datos_solicitud = "display:inline";
                sol = rm.GetSolicitudReservaById(Convert.ToInt32(idSolicitud));
                Persona p = pm.GetPersonaById((int)sol.Usuarios.idPersona);
                NegocioEntity neg = nm.GetNegocioById((int)sol.idNegocio);

                ViewBag.Persona = p;
                ViewBag.idPersona = p.idPersona;
                ViewBag.idTipoLugarHospedaje = neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje;

                ViewBag.disabled = "disabled";
            }
            else
            {
                ViewBag.datos_cliente = "display:inline";
            }

            return View(sol);
        }
        public ActionResult TusHabitaciones(int idLugarHospedaje)
        {
            int idHotel = int.Parse(db.Hotel.Where(h => h.idLugarHospedaje == idLugarHospedaje).Select(h => h.idHotel).FirstOrDefault().ToString());
            return RedirectToAction("Index", "Habitaciones", new { idHotel = idHotel });
        }
        public ActionResult TusDptosOCabanas(int idLugarHospedaje)
        {
            int idComplejo = int.Parse(db.Complejo.Where(c => c.idLugarHospedaje == idLugarHospedaje).Select(c => c.idComplejo).FirstOrDefault().ToString());
            return RedirectToAction("Index", "DptoOCabana", new { idComplejo = idComplejo });
        }
        public bool ValidarPermisoVista(string controlador, string vista) //METODO ÚNICO DEL CONTROLADOR PARA VALIDAR PERMISO DE LA VISTA (LLAMA AL MANEJADOR).
        {
            ObtenerUsuarioActual();
            return um.ValidarPermisoVista(usuarioActual, controlador, vista);
        }
        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
	}
}