using AppHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppHotel.Controllers
{
    public class DettagliServiziController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            List<DettaglioServizio> dettagliServizi = new List<DettaglioServizio>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DettagliServizi";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DettaglioServizio dettaglioServizio = new DettaglioServizio
                    {
                        IdDettaglio = Convert.ToInt32(rdr["IdDettaglio"]),
                        IdPrenotazione = Convert.ToInt32(rdr["IdPrenotazione"]),
                        IdServizio = Convert.ToInt32(rdr["IdServizio"]),
                        Data = Convert.ToDateTime(rdr["Data"]),
                        Quantita = Convert.ToInt32(rdr["Quantita"])
                    };
                    dettagliServizi.Add(dettaglioServizio);
                }
            }
            return View(dettagliServizi);
        }

        public ActionResult Create()
        {
            ViewBag.Prenotazioni = new SelectList(GetPrenotazioniSelectList(), "Value", "Text");
            ViewBag.ServiziAggiuntivi = new SelectList(GetServiziAggiuntiviSelectList(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrenotazione,IdServizio,Data,Quantita")] DettaglioServizio dettaglioServizio)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlCommand = "INSERT INTO DettagliServizi (IdPrenotazione, IdServizio, Data, Quantita) VALUES (@IdPrenotazione, @IdServizio, @Data, @Quantita)";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@IdPrenotazione", dettaglioServizio.IdPrenotazione);
                    cmd.Parameters.AddWithValue("@IdServizio", dettaglioServizio.IdServizio);
                    cmd.Parameters.AddWithValue("@Data", dettaglioServizio.Data);
                    cmd.Parameters.AddWithValue("@Quantita", dettaglioServizio.Quantita);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Prenotazioni = new SelectList(GetPrenotazioniSelectList(), "Value", "Text");
            ViewBag.ServiziAggiuntivi = new SelectList(GetServiziAggiuntiviSelectList(), "Value", "Text");
            return View(dettaglioServizio);
        }

        private IEnumerable<SelectListItem> GetPrenotazioniSelectList()
        {
            var prenotazioniList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdPrenotazione, Dettagli FROM Prenotazioni", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    prenotazioniList.Add(new SelectListItem
                    {
                        Value = rdr["IdPrenotazione"].ToString(),
                        Text = rdr["Dettagli"].ToString()
                    });
                }
            }
            return prenotazioniList;
        }

        private IEnumerable<SelectListItem> GetServiziAggiuntiviSelectList()
        {
            var serviziList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdServizio, Descrizione FROM ServiziAggiuntivi", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    serviziList.Add(new SelectListItem
                    {
                        Value = rdr["IdServizio"].ToString(),
                        Text = rdr["Descrizione"].ToString()
                    });
                }
            }
            return serviziList;
        }

    }
}