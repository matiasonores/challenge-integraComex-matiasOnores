using challenge_integraComex_matiasOnores.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace challenge_integraComex_matiasOnores.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        [HttpGet]
        public ActionResult Index()
        {
            //List<Cliente> clientes = new List<Cliente>();

            var clientes = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    CUIT = 12345678901,
                    RazonSocial = "Empresa A",
                    Telefono = "123456789",
                    Direccion = "Calle 123",
                    Activo = true
                },
                new Cliente
                {
                    Id = 2,
                    CUIT = 98765432109,
                    RazonSocial = "Empresa B",
                    Telefono = "987654321",
                    Direccion = "Avenida 456",
                    Activo = false
                }
                // Puedes agregar más clientes aquí si lo deseas
            };
            return View(clientes);
        }
        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente nuevo)
        {
            if (ModelState.IsValid)
            {
                //using (SqlConnection connection = new SqlConnection(""))
                //{
                //    connection.Open();
                //    using (SqlCommand cmd = new SqlCommand("NombreDeTuStoredProcedure", connection))
                //    {
                //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //        cmd.Parameters.AddWithValue("@Parametro1", nuevo.CUIT);
                //        cmd.Parameters.AddWithValue("@Parametro2", nuevo.RazonSocial);
                //        cmd.Parameters.AddWithValue("@Parametro3", nuevo.Direccion);
                //        cmd.Parameters.AddWithValue("@Parametro4", nuevo.Telefono);
                //        cmd.Parameters.AddWithValue("@Parametro5", nuevo.Activo);

                //        cmd.ExecuteNonQuery();
                //    }
                //}
                //return RedirectToAction("Index", "Cliente");
            }

            //return View(nuevo);

            return Json(new { success = true, message = "Cliente creado exitosamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            Cliente cliente = new Cliente
            {
                Id = 2,
                CUIT = 98765432109,
                RazonSocial = "Empresa B",
                Telefono = "987654321",
                Direccion = "Avenida 456",
                Activo = false
            };
            return View(cliente);

        }

        public ActionResult Delete(int id)
        {

            return View();

        }

        [HttpPost]
        public async Task<string> GetNombreByCuitAsync(string cuit)
        {
            var retorno = "";

            try
            {
                string endpoint = "https://sistemaintegracomex.com.ar/Account/GetNombreByCuit?cuit=" + cuit;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cookie", "ASP.NET_SessionId=jkl53zqtqajie4vee051xzta");
                HttpResponseMessage response = await client.PostAsync(endpoint, new StringContent("", null, "text/plain"));
                response.EnsureSuccessStatusCode();
                client.Dispose();
                retorno = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                retorno = ex.Message;
            }

            return retorno;
        }
    }
}