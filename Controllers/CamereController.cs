using AppHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppHotel.Controllers
{
    public class CamereController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // GET: Camere
        public ActionResult Index()
        {
            List<Camera> camere = new List<Camera>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Camere", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Camera camera = new Camera
                    {
                        IdCamera = Convert.ToInt32(rdr["IdCamera"]),
                        NumeroCamera = rdr["NumeroCamera"].ToString(),
                        Descrizione = rdr["Descrizione"].ToString(),
                        Tipologia = rdr["Tipologia"].ToString()
                    };
                    camere.Add(camera);
                }
                con.Close();
            }
            return View(camere);
        }

        // GET: Camere/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Camere/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumeroCamera,Descrizione,Tipologia")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlCommand = "INSERT INTO Camere (NumeroCamera, Descrizione, Tipologia) VALUES (@NumeroCamera, @Descrizione, @Tipologia)";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@NumeroCamera", camera.NumeroCamera);
                    cmd.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                    cmd.Parameters.AddWithValue("@Tipologia", camera.Tipologia);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }

            return View(camera);
        }
    }
}