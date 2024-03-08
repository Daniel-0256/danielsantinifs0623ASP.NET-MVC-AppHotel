using AppHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppHotel.Controllers
{
    public class PrenotazioniController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prenotazioni", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Prenotazione prenotazione = new Prenotazione
                    {
                        IdPrenotazione = Convert.ToInt32(rdr["IdPrenotazione"]),
                        DataPrenotazione = Convert.ToDateTime(rdr["DataPrenotazione"]),
                        DataInizio = Convert.ToDateTime(rdr["DataInizio"]),
                        DataFine = Convert.ToDateTime(rdr["DataFine"]),
                        Caparra = rdr["Caparra"] != DBNull.Value ? Convert.ToDecimal(rdr["Caparra"]) : 0,
                        Tariffa = Convert.ToDecimal(rdr["Tariffa"]),
                        Dettagli = rdr["Dettagli"].ToString(),
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        IdCamera = Convert.ToInt32(rdr["IdCamera"])
                    };
                    prenotazioni.Add(prenotazione);
                }
            }
            return View(prenotazioni);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Prenotazione prenotazione = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@IdPrenotazione", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    prenotazione = new Prenotazione
                    {
                        IdPrenotazione = Convert.ToInt32(rdr["IdPrenotazione"]),
                        DataPrenotazione = Convert.ToDateTime(rdr["DataPrenotazione"]),
                        DataInizio = Convert.ToDateTime(rdr["DataInizio"]),
                        DataFine = Convert.ToDateTime(rdr["DataFine"]),
                        Caparra = rdr["Caparra"] != DBNull.Value ? Convert.ToDecimal(rdr["Caparra"]) : 0,
                        Tariffa = Convert.ToDecimal(rdr["Tariffa"]),
                        Dettagli = rdr["Dettagli"].ToString(),
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        IdCamera = Convert.ToInt32(rdr["IdCamera"])
                    };
                }
            }
            if (prenotazione == null)
            {
                return HttpNotFound();
            }
            return View(prenotazione);
        }

        public ActionResult Create()
        {
            List<SelectListItem> clienti = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdCliente, Nome, Cognome FROM Clienti", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    clienti.Add(new SelectListItem
                    {
                        Value = rdr["IdCliente"].ToString(),
                        Text = rdr["Cognome"].ToString() + " " + rdr["Nome"].ToString()
                    });
                }
                con.Close();
            }

            List<SelectListItem> camere = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdCamera, NumeroCamera FROM Camere", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    camere.Add(new SelectListItem
                    {
                        Value = rdr["IdCamera"].ToString(),
                        Text = rdr["NumeroCamera"].ToString()
                    });
                }
                con.Close();
            }

            ViewBag.Clienti = new SelectList(clienti, "Value", "Text");
            ViewBag.Camere = new SelectList(camere, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DataPrenotazione,DataInizio,DataFine,Caparra,Tariffa,Dettagli,IdCliente,IdCamera")] Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlCommand = "INSERT INTO Prenotazioni (DataPrenotazione, DataInizio, DataFine, Caparra, Tariffa, Dettagli, IdCliente, IdCamera) VALUES (@DataPrenotazione, @DataInizio, @DataFine, @Caparra, @Tariffa, @Dettagli, @IdCliente, @IdCamera)";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                    cmd.Parameters.AddWithValue("@DataInizio", prenotazione.DataInizio);
                    cmd.Parameters.AddWithValue("@DataFine", prenotazione.DataFine);
                    cmd.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                    cmd.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);
                    cmd.Parameters.AddWithValue("@Dettagli", prenotazione.Dettagli ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCliente", prenotazione.IdCliente);
                    cmd.Parameters.AddWithValue("@IdCamera", prenotazione.IdCamera);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
            return View(prenotazione);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Prenotazione prenotazione = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@IdPrenotazione", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    prenotazione = new Prenotazione
                    {
                        IdPrenotazione = Convert.ToInt32(rdr["IdPrenotazione"]),
                        DataPrenotazione = Convert.ToDateTime(rdr["DataPrenotazione"]),
                        DataInizio = Convert.ToDateTime(rdr["DataInizio"]),
                        DataFine = Convert.ToDateTime(rdr["DataFine"]),
                        Caparra = rdr["Caparra"] != DBNull.Value ? Convert.ToDecimal(rdr["Caparra"]) : 0,
                        Tariffa = Convert.ToDecimal(rdr["Tariffa"]),
                        Dettagli = rdr["Dettagli"].ToString(),
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        IdCamera = Convert.ToInt32(rdr["IdCamera"])
                    };
                }
            }
            if (prenotazione == null)
            {
                return HttpNotFound();
            }
            return View(prenotazione);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string sqlDeleteDetails = "DELETE FROM DettagliServizi WHERE IdPrenotazione = @IdPrenotazione";
                SqlCommand cmdDeleteDetails = new SqlCommand(sqlDeleteDetails, con);
                cmdDeleteDetails.Parameters.AddWithValue("@IdPrenotazione", id);
                cmdDeleteDetails.ExecuteNonQuery();

                string sqlCommand = "DELETE FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@IdPrenotazione", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CheckOut(int id)
        {
            CheckOutViewModel checkOutInfo = new CheckOutViewModel();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string prenotazioneSql = "SELECT DataInizio, DataFine, Tariffa, Caparra FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                SqlCommand cmd = new SqlCommand(prenotazioneSql, con);
                cmd.Parameters.AddWithValue("@IdPrenotazione", id);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        checkOutInfo.DataInizio = Convert.ToDateTime(rdr["DataInizio"]);
                        checkOutInfo.DataFine = Convert.ToDateTime(rdr["DataFine"]);
                        checkOutInfo.Tariffa = Convert.ToDecimal(rdr["Tariffa"]);
                        checkOutInfo.Caparra = Convert.ToDecimal(rdr["Caparra"]);
                    }
                }

                int giorniDiSoggiorno = (checkOutInfo.DataFine - checkOutInfo.DataInizio).Days;
                checkOutInfo.TotaleSoggiorno = giorniDiSoggiorno * checkOutInfo.Tariffa;

                string serviziSql = "SELECT SUM(sa.Prezzo * ds.Quantita) AS TotaleServizi FROM DettagliServizi ds INNER JOIN ServiziAggiuntivi sa ON ds.IdServizio = sa.IdServizio WHERE ds.IdPrenotazione = @IdPrenotazione";
                SqlCommand cmdServizi = new SqlCommand(serviziSql, con);
                cmdServizi.Parameters.AddWithValue("@IdPrenotazione", id);
                object result = cmdServizi.ExecuteScalar();
                checkOutInfo.TotaleServizi = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;

                checkOutInfo.TotaleDaPagare = checkOutInfo.TotaleSoggiorno + checkOutInfo.TotaleServizi - checkOutInfo.Caparra;
            }

            return View(checkOutInfo);
        }
    }
}