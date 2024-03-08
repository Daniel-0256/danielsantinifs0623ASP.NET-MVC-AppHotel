using AppHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppHotel.Controllers
{
    public class ServiziAggiuntiviController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public ActionResult Index()
        {
            List<ServizioAggiuntivo> serviziAggiuntivi = new List<ServizioAggiuntivo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ServiziAggiuntivi", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ServizioAggiuntivo servizio = new ServizioAggiuntivo
                    {
                        IdServizio = Convert.ToInt32(rdr["IdServizio"]),
                        Descrizione = rdr["Descrizione"].ToString(),
                        Prezzo = Convert.ToDecimal(rdr["Prezzo"]),
                    };
                    serviziAggiuntivi.Add(servizio);
                }
                con.Close();
            }
            return View(serviziAggiuntivi);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Descrizione,Prezzo")] ServizioAggiuntivo servizio)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlCommand = "INSERT INTO ServiziAggiuntivi (Descrizione, Prezzo) VALUES (@Descrizione, @Prezzo)";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@Descrizione", servizio.Descrizione);
                    cmd.Parameters.AddWithValue("@Prezzo", servizio.Prezzo);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
            return View(servizio);
        }
    }
}