using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using HabboHotel.Models;

namespace HabboHotel.Controllers
{
    public class MonitoraPrenotazioniController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        // GET: MonitorPrenotazioni
        public ActionResult Index()
        {
            // Controlla se l'utente è loggato
            if (Session["AdminLogged"] == null || (string)Session["AdminLogged"] != "true")
            {
                
                return RedirectToAction("Login", "Home"); 
            }

            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT P.IdPrenotazione, P.DataPrenotazione, P.SoggiornoDa, P.SoggiornoA, 
                                P.Tariffa, C.Cognome, C.Nome, CAM.Descrizione, PE.TipoPensione
                                FROM Prenotazioni P
                                INNER JOIN Clienti C ON P.IdCliente = C.IdCliente
                                INNER JOIN Camere CAM ON P.IdCamera = CAM.IdCamera
                                INNER JOIN Pensioni PE ON P.IdPensione = PE.IdPensione";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prenotazione = new Prenotazione()
                            {
                                // Nota: Assicurati che i tipi di dati siano corretti, in particolare per le date
                                PrenotazioneId = reader.GetInt32(reader.GetOrdinal("IdPrenotazione")),
                                DataPrenotazione = reader.GetString(reader.GetOrdinal("DataPrenotazione")), // Assicurati che questi siano DateTime
                                SoggiornoDa = reader.GetString(reader.GetOrdinal("SoggiornoDa")),
                                SoggiornoA = reader.GetString(reader.GetOrdinal("SoggiornoA")),
                                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                                Cliente = new Cliente()
                                {
                                    Cognome = reader.GetString(reader.GetOrdinal("Cognome")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                },
                                Camera = new Camera()
                                {
                                    Descrizione = reader.GetString(reader.GetOrdinal("Descrizione")),
                                },
                                Pensione = new Pensione()
                                {
                                    TipoPensione = reader.GetString(reader.GetOrdinal("TipoPensione")),
                                }
                            };
                            prenotazioni.Add(prenotazione);
                        }
                    }
                }
            }
            return View(prenotazioni);
        }
    }
}
