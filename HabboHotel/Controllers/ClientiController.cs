using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HabboHotel.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace HabboHotel.Controllers
{
    public class ClientiController : Controller
    {
        // GET: Clienti/Registra
        public ActionResult Registra()
        {
            if (Session["AdminLogged"] == null || (string)Session["AdminLogged"] != "true")
            {
                return RedirectToAction("Login", "Home"); 
            }

            return View(new Cliente()); 
        }

        // POST: Clienti/Registra
        [HttpPost]
        public ActionResult Registra(Cliente cliente)
        {
            if (Session["AdminLogged"] == null || (string)Session["AdminLogged"] != "true")
            {
                return RedirectToAction("Login", "Home"); 
            }

            if (ModelState.IsValid)
            {
                
                string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Clienti (Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare, CodiceFiscale) VALUES (@Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare, @CodiceFiscale)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@Citta", cliente.Citta);
                        cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);
                        cmd.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                ViewBag.SuccessMessage = "Cliente registrato con successo";
                return View(new Cliente()); 
            }

            return View(cliente);
        }
    }
}
