using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using HabboHotel.Models;

namespace HabboHotel.Controllers
{
    public class PrenotazioniController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        // GET: Prenotazioni/InserisciPrenotazione
        public ActionResult InserisciPrenotazione()
        {
            if (Session["AdminLogged"] == null || (string)Session["AdminLogged"] != "true")
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.IdPensione = GetSelectListItems("SELECT IdPensione, TipoPensione FROM Pensioni");
            ViewBag.IdCliente = GetSelectListItems("SELECT IdCliente, Cognome + ' ' + Nome + ' - ' + CodiceFiscale AS Descrizione FROM Clienti");
            ViewBag.IdCamera = GetSelectListItems("SELECT IdCamera, CASE WHEN Tipologia = 0 THEN CAST(IdCamera AS VARCHAR) + ' - Singola' ELSE CAST(IdCamera AS VARCHAR) + ' - Doppia' END AS Descrizione FROM Camere");

            return View();
        }

        // POST: Prenotazioni/InserisciPrenotazione
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserisciPrenotazione(Prenotazione prenotazione)
        {
            if (Session["AdminLogged"] == null || (string)Session["AdminLogged"] != "true")
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string insertQuery = @"INSERT INTO Prenotazioni (DataPrenotazione, SoggiornoDa, SoggiornoA, IdPensione, IdCliente, IdCamera, Tariffa)
                                           VALUES (@DataPrenotazione, @SoggiornoDa, @SoggiornoA, @IdPensione, @IdCliente, @IdCamera, @Tariffa)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                        cmd.Parameters.AddWithValue("@SoggiornoDa", prenotazione.SoggiornoDa);
                        cmd.Parameters.AddWithValue("@SoggiornoA", prenotazione.SoggiornoA);
                        cmd.Parameters.AddWithValue("@IdPensione", prenotazione.IdPensione);
                        cmd.Parameters.AddWithValue("@IdCliente", 1);
                        cmd.Parameters.AddWithValue("@IdCamera", 2);
                        cmd.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            ViewBag.SuccessMessage = "Prenotazione inserita con successo";
                        }
                    }
                }
            }

            // Ricarica le SelectList in caso di errore di validazione
            ViewBag.IdPensione = GetSelectListItems("SELECT IdPensione, TipoPensione FROM Pensioni");
            ViewBag.IdCliente = GetSelectListItems("SELECT IdCliente, Cognome + ' ' + Nome + ' - ' + CodiceFiscale AS Descrizione FROM Clienti");
            ViewBag.IdCamera = GetSelectListItems("SELECT IdCamera, CASE WHEN Tipologia = 0 THEN CAST(IdCamera AS VARCHAR) + ' - Singola' ELSE CAST(IdCamera AS VARCHAR) + ' - Doppia' END AS Descrizione FROM Camere");

            return View(prenotazione);
        }

        private IEnumerable<SelectListItem> GetSelectListItems(string query)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Value = reader[0].ToString(),
                                Text = reader[1].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }
    }
}
