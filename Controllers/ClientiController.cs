using AppHotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppHotel.Controllers
{
    public class ClientiController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            List<Cliente> clienti = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clienti", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        CodiceFiscale = rdr["CodiceFiscale"].ToString(),
                        Cognome = rdr["Cognome"].ToString(),
                        Nome = rdr["Nome"].ToString(),
                        Citta = rdr["Citta"].ToString(),
                        Provincia = rdr["Provincia"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Cellulare = rdr["Cellulare"].ToString()
                    };
                    clienti.Add(cliente);
                }
                con.Close();
            }
            return View(clienti);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Clienti WHERE IdCliente= @IdCliente";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@IdCliente", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        CodiceFiscale = rdr["CodiceFiscale"].ToString(),
                        Cognome = rdr["Cognome"].ToString(),
                        Nome = rdr["Nome"].ToString(),
                        Citta = rdr["Citta"].ToString(),
                        Provincia = rdr["Provincia"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Cellulare = rdr["Cellulare"].ToString()
                    };
                }
                con.Close();
            }
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodiceFiscale,Cognome,Nome,Citta,Provincia,Email,Telefono,Cellulare")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlCommand = "INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    cmd.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Citta", cliente.Citta);
                    cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Clienti WHERE IdCliente = @IdCliente";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@IdCliente", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        CodiceFiscale = rdr["CodiceFiscale"].ToString(),
                        Cognome = rdr["Cognome"].ToString(),
                        Nome = rdr["Nome"].ToString(),
                        Citta = rdr["Citta"].ToString(),
                        Provincia = rdr["Provincia"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Cellulare = rdr["Cellulare"].ToString()
                    };
                }
                con.Close();
            }
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,CodiceFiscale,Cognome,Nome,Citta,Provincia,Email,Telefono,Cellulare")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlCommand = "UPDATE Clienti SET CodiceFiscale = @CodiceFiscale, Cognome = @Cognome, Nome = @Nome, Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare WHERE IdCliente = @IdCliente";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    cmd.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Citta", cliente.Citta);
                    cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Clienti WHERE IdCliente = @IdCliente";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@IdCliente", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                        CodiceFiscale = rdr["CodiceFiscale"].ToString(),
                        Cognome = rdr["Cognome"].ToString(),
                        Nome = rdr["Nome"].ToString(),
                        Citta = rdr["Citta"].ToString(),
                        Provincia = rdr["Provincia"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Cellulare = rdr["Cellulare"].ToString()
                    };
                }
                con.Close();
            }
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE FROM Clienti WHERE IdCliente = @IdCliente";
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@IdCliente", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult RicercaPerCodiceFiscale(string codiceFiscale)
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = @"SELECT P.* FROM Prenotazioni P 
                            INNER JOIN Clienti C ON P.IdCliente = C.IdCliente 
                            WHERE C.CodiceFiscale = @CodiceFiscale";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prenotazione prenotazione = new Prenotazione
                            {
                                IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                DataInizio = Convert.ToDateTime(reader["DataInizio"]),
                                DataFine = Convert.ToDateTime(reader["DataFine"]),
                                Caparra = reader["Caparra"] != DBNull.Value ? Convert.ToDecimal(reader["Caparra"]) : 0,
                                Tariffa = Convert.ToDecimal(reader["Tariffa"]),
                                Dettagli = reader["Dettagli"] != DBNull.Value ? reader["Dettagli"].ToString() : null,
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                IdCamera = Convert.ToInt32(reader["IdCamera"])
                            };
                            prenotazioni.Add(prenotazione);
                        }
                    }
                }
            }
            return PartialView("_RisultatiRicerca", prenotazioni);
        }
    }
}