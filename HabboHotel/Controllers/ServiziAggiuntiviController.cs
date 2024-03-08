using HabboHotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HabboHotel.Controllers
{
    
    public class ServiziAggiuntiController : Controller
    {
        public string connString = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var conn = new SqlConnection(connString);
            conn.Open();
            var selectPrenotazioni = new SqlCommand("SELECT * FROM Prenotazioni", conn);
            var readerPrenotazioni = selectPrenotazioni.ExecuteReader();

            var prenotazioni = new List<Prenotazione>();
            if (readerPrenotazioni.HasRows)
            {
                while (readerPrenotazioni.Read())
                {
                    var prenotazione = new Prenotazione()
                    {
                        PrenotazioneId = (int)readerPrenotazioni["IdPrenotazione"],
                        DataPrenotazione = "Id Prenotazione: " + readerPrenotazioni["IdPrenotazione"].ToString() + " | Pensione: " + readerPrenotazioni["IdPensione"].ToString() + " | Cliente: " + readerPrenotazioni["IdCliente"].ToString() + " | Camera: " + readerPrenotazioni["IdCamera"].ToString()
                    };
                    prenotazioni.Add(prenotazione);
                }
            }

            readerPrenotazioni.Close();

            var selectServizi = new SqlCommand("SELECT * FROM ServiziAggiuntivi", conn);
            var readerServizi = selectServizi.ExecuteReader();

            var servizi = new List<ServizoAggiuntivo>();
            if (readerServizi.HasRows)
            {
                while (readerServizi.Read())
                {
                    var servizio = new ServizoAggiuntivo()
                    {
                        ServizioAggiuntivoId = (int)readerServizi["IdServizio"],
                        TipoServizio = readerServizi["TipoServizio"].ToString() + " " + readerServizi["PrezzoServizio"].ToString(),
                    };
                    servizi.Add(servizio);
                }
            }

            readerServizi.Close();

            ViewBag.Prenotazioni = prenotazioni;
            ViewBag.Servizi = servizi;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(StoricoServiziAggiuntivi storico)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(connString);
                conn.Open(); 

                
                string selectServizio = "SELECT PrezzoServizio FROM ServiziAggiuntivi WHERE IdServizio = @IdServizio";
                SqlCommand cmdServizio = new SqlCommand(selectServizio, conn);
                cmdServizio.Parameters.AddWithValue("@IdServizio", storico.StoricoServiziAggiuntiviId);
                var reader2 = cmdServizio.ExecuteReader();
                decimal prezzoServizio = 0;
                if (reader2.HasRows)
                {
                    if (reader2.Read())
                    {
                        prezzoServizio = (decimal)reader2["PrezzoServizio"];
                    }
                }
                reader2.Close();

                string insertServizio = "INSERT INTO StoricoServiziAggiuntivi (IdPrenotazione, IdServizio, DataServizio, PrezzoTotale) VALUES (@IdPrenotazione, @IdServizio, @DataServizio, @PrezzoTotale)";
                SqlCommand cmdInsert = new SqlCommand(insertServizio, conn);
                cmdInsert.Parameters.AddWithValue("@IdPrenotazione", storico.PrenotazioneId);
                cmdInsert.Parameters.AddWithValue("@IdServizio", storico.StoricoServiziAggiuntiviId);
                cmdInsert.Parameters.AddWithValue("@DataServizio", storico.DataServizio);
                cmdInsert.Parameters.AddWithValue("@PrezzoTotale", prezzoServizio);
                cmdInsert.ExecuteNonQuery();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }
    }
}