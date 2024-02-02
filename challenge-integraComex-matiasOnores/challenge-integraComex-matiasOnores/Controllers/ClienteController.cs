using Antlr.Runtime.Misc;
using challenge_integraComex_matiasOnores.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        private ClientesDB _clientesDB = new ClientesDB();
        // GET: Cliente
        [HttpGet]
        public ActionResult Index()
        {
            List<Cliente> clientes;
            try
            {
                clientes = ObtenerClientes();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(clientes);
        }

        private List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                DataTable datosClientes = _clientesDB.ObtenerClientes();

                if (datosClientes != null && datosClientes.Rows.Count > 0)
                {
                    foreach (DataRow row in datosClientes.Rows)
                    {
                        Cliente cliente = CrearCliente(row);
                        clientes.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return clientes;
        }

        private Cliente CrearCliente(DataRow row)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente.Id = Convert.ToInt32(row["Id"]);
                cliente.CUIT = Convert.ToInt64(row["CUIT"]);
                cliente.RazonSocial = row["RazonSocial"].ToString();
                cliente.Telefono = row["Telefono"].ToString();
                cliente.Direccion = row["Direccion"].ToString();
                cliente.Activo = Convert.ToBoolean(row["Activo"]);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return cliente;
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente nuevo)
        {
            bool creado = false;
            string mensaje = "";
            JsonResult resultado;
            try
            {

                if (ModelState.IsValid)
                {
                    creado = _clientesDB.CrearCliente(nuevo);
                    mensaje = creado ? "¡Cliente creado exitosamente" : "Ocurrió un error al crear el cliente";
                }
                else
                {
                    mensaje = "El modelo no es válido";
                }

                resultado = Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado = Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return resultado;
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Cliente cliente = null;
            try
            {
                DataTable datosCliente = _clientesDB.ObtenerClientePorId(id);
                if (datosCliente != null && datosCliente.Rows.Count > 0)
                {
                    foreach (DataRow row in datosCliente.Rows)
                    {
                        cliente = CrearCliente(row);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            bool modificado = false;
            string mensaje = "";
            JsonResult resultado;

            try
            {
                if (ModelState.IsValid)
                {
                    modificado = _clientesDB.ModificarCliente(cliente);

                    mensaje = modificado ? "Cliente modificado exitosamente" : "Ocurrió un error al modificar el cliente";
                }
                else
                {
                    mensaje = "El modelo no es válido";
                }

                resultado = Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado = Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return resultado;
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Cliente cliente = null;
            try
            {
                DataTable datosCliente = _clientesDB.ObtenerClientePorId(id);
                if (datosCliente != null && datosCliente.Rows.Count > 0)
                {
                    foreach (DataRow row in datosCliente.Rows)
                    {
                        cliente = CrearCliente(row);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView(cliente);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool eliminado = false;
            string mensaje = "";
            JsonResult resultado;

            try
            {
                if (ModelState.IsValid)
                {
                    eliminado = _clientesDB.EliminarCliente(id);
                    mensaje = eliminado ? "Cliente eliminado exitosamente" : "Ocurrió un error al eliminar el cliente";
                }

                resultado = Json(new { success = eliminado, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado = Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return resultado;

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