﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using BL;
using BussinesEntities;

namespace ProyectoFinal
{
    public class PromocionesController : Controller
    {
        private SitcomEntities db = new SitcomEntities();
        
        public UsuarioEntity usuarioActual;
        public UsuariosManager um = new UsuariosManager();
        private PromocionesManager pm = new PromocionesManager();
        private PersonasManager perm = new PersonasManager();


        public ActionResult ObtenerPromocion(int idPromocion)
        {
            ObtenerUsuarioActual();

            if (usuarioActual.idPerfil == 0)
            {
                Session["ReturnUrl"] = "../Promociones/ObtenerPromocion/?idPromocion="+idPromocion;
                return RedirectToAction("Login", "Usuarios");
            }

           PromocionesEntity p = pm.getPromocionById(idPromocion);
           ViewBag.Mensaje = "";
           return View(p);
        }

        public ActionResult OtorgarPromocion(int idPromocion)
        {
             ObtenerUsuarioActual();

             int result = pm.otorgarPromocion(usuarioActual.idUsuario, idPromocion);

            if(result == 0)
            {
                PromocionesOtorgadasUsuario pro = pm.getUltimaPromocionOtorgada(usuarioActual.idUsuario);
                ViewBag.IdUsuario = usuarioActual.idUsuario;
                return View("PromocionOtorgada", pro);
            }
            else
            {
                string mensaje = "";

                switch (result)
                {
                    case 1: mensaje = "El usuario ya tiene el numero maximo de promociones otorgadas.";
                        break;

                    case 2: mensaje = "El usuario ya posee una promoción vigente asociada a este negocio.";
                        break;

                    case 3: mensaje = "El usuario NO es un usuario turista.";
                        break;

                    default:
                        break;
                }

                PromocionesEntity p = pm.getPromocionById(idPromocion);

                ViewBag.Mensaje = mensaje;

                return View("ObtenerPromocion",p);

            }

             return View();
        }


        public ActionResult FinalizarPromocion(int idPromocion, int idNegocio)
        {
            int result = pm.finalizarPromocion(idPromocion);

            return PromocionesNeg(idNegocio);
        }


        public ActionResult PromocionesNeg(int? idNegocio)
        {
            ObtenerUsuarioActual();
            
            int esTurista = 0;
            if (usuarioActual.idPerfil == 1)
                esTurista = 1;

           List<PromocionesNegocioEntity> p = (List<PromocionesNegocioEntity>)pm.getPromociones(idNegocio, esTurista);

           ViewBag.IdNegocio = idNegocio;
           ViewBag.Perfil = usuarioActual.idPerfil;
           ViewBag.idPerfil = usuarioActual.idPerfil;

           return View("PromocionesNeg",p);
        }

        public ActionResult ValidarPromocion()
        {
            ObtenerUsuarioActual();
            
            ViewBag.Perfil = usuarioActual.idPerfil;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarPromo(string CODIGO)
        {
            ObtenerUsuarioActual();

            PromocionesOtorgadasUsuario p = pm.getPromocionOtorgada(CODIGO);


            ViewBag.Perfil = usuarioActual.idPerfil;

            if (p == null || p.ESTADO.Equals("UTILIZADA") || p.ESTADO.Equals("VENCIDA"))
            {

                string mensaje = "El código ingresado NO es válido";
                ViewBag.Mensaje = mensaje;
                ViewBag.Status = 0;
                return View("ValidarPromocion");
            }
            else
            {
                string mensaje = "Código validado correctamente!";

                Persona per = perm.GetPersonaByIdUsuario(p.USUARIO);

                ViewBag.Persona = per;

                ViewBag.Mensaje = mensaje;
                ViewBag.Status = 1;
                return View("ValidarPromocion", p);
            }           
        }

        public ActionResult PromocionesUsuario()
        {
            ObtenerUsuarioActual();

            List<PromocionesOtorgadasUsuario> p = (List<PromocionesOtorgadasUsuario>)pm.getPromocionesOtorgadas(usuarioActual.idUsuario);

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.idPerfil = usuarioActual.idPerfil;
            return View(p);
        }

        public ActionResult RegUsoPromocion(string codigo)
        {
            ObtenerUsuarioActual();

            int result = pm.regUsoPromocion(codigo);

            ViewBag.Perfil = usuarioActual.idPerfil;
            
            if (result != 0)
            {
                string mensaje = "El uso de la promocion NO se pudo registrar.";
                ViewBag.Mensaje = mensaje;
                ViewBag.Status = 0;
                return View("ValidarPromocion");
            }
            else
            {
                string mensaje = "El uso de la promocion se registro correctamente!";
                ViewBag.Mensaje = mensaje;
                ViewBag.Status = 1;
                return View("ValidarPromocion");
            }

        }

        // GET: /Promociones/Create
        public ActionResult NuevaPromocion(int idNegocio)
        {
            ViewBag.IdNegocio = idNegocio;
            return View();
        }

        public ActionResult NuevaPromocionE(PromocionesEntity pro, string mensaje)
        {
            ViewBag.IdNegocio = pro.idNegocio;
            ViewBag.Mensaje = mensaje;
            return View("NuevaPromocion",pro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaPromocion([Bind(Include = "titulo,descripcion,fechaVencimiento,diasBeneficio,ofertaMaxima,idNegocio")] PromocionesEntity promociones)
        {
            if (ModelState.IsValid)
            {
                int resultado = pm.addPromocion(promociones);

                if(resultado == 0)
                {
                    return PromocionesNeg(promociones.idNegocio);
                }
                else
                {
                    string mensaje = "";

                    switch (resultado)
                    {
                        case 1: mensaje = "El negocio ya registro la cantidad maxima de promociones vigentes.";
                            break;

                        case 2: mensaje = "La fecha de vencimiento supera los 180 dias.";
                            break;

                        case 3: mensaje = "Los dias de beneficio superan los 90 dias.";
                            break;

                        default: mensaje = "Error dando de alta la promocion";
                            break;
                    }


                    PromocionesEntity pe = new PromocionesEntity()
                    {
                        titulo = promociones.titulo,
                        descripcion = promociones.descripcion,
                        diasBeneficio = promociones.diasBeneficio,
                        fechaVencimiento = promociones.fechaVencimiento,
                        ofertaMaxima = promociones.ofertaMaxima,
                        idNegocio = promociones.idNegocio
                    };

                    return NuevaPromocionE(promociones, mensaje);

                }
            }
            else
            {
                return NuevaPromocion((int)promociones.idNegocio);
                    
            }
        }

        // POST: /Promociones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idPromocion,fechaAlta,fechaVencimiento,titulo,descripcion,diasBeneficio,idNegocio")] Promociones promociones)
        {
            if (ModelState.IsValid)
            {
                db.Promociones.Add(promociones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idNegocio = new SelectList(db.Negocio, "idNegocio", "nombre", promociones.idNegocio);
            return View(promociones);
        }

        // GET: /Promociones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promociones promociones = db.Promociones.Find(id);
            if (promociones == null)
            {
                return HttpNotFound();
            }
            ViewBag.idNegocio = new SelectList(db.Negocio, "idNegocio", "nombre", promociones.idNegocio);
            return View(promociones);
        }

        // POST: /Promociones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idPromocion,fechaAlta,fechaVencimiento,titulo,descripcion,diasBeneficio,idNegocio")] Promociones promociones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promociones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idNegocio = new SelectList(db.Negocio, "idNegocio", "nombre", promociones.idNegocio);
            return View(promociones);
        }

        // GET: /Promociones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promociones promociones = db.Promociones.Find(id);
            if (promociones == null)
            {
                return HttpNotFound();
            }
            return View(promociones);
        }

        // POST: /Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promociones promociones = db.Promociones.Find(id);
            db.Promociones.Remove(promociones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
    }
}
