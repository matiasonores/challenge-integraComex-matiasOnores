using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace challenge_integraComex_matiasOnores.Models
{
    public class ClientesDB
    {
        private string _connectionString;
        private readonly SqlConnection _connection;

        private SqlCommand _command;

        public ClientesDB()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            _connection = new SqlConnection(_connectionString);
        }
        private void AbrirConexion()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private void CerrarConexion()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
        
        private void SetStoredProcedure(string nombreStoredProcedure)
        {
            try
            {
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandText = nombreStoredProcedure;
                _command.CommandType = CommandType.StoredProcedure;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SetQuery(string query)
        {
            try
            {
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandText = query;
                _command.CommandType = CommandType.Text;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SetParameter(string parametro, object valor)
        {
            try
            {
                _command.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable EjecutarComando()
        {
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();
                SqlDataAdapter adapter = new SqlDataAdapter(_command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CerrarConexion();
            }

            return dt;
        }
        public DataTable ObtenerClientes()
        {
            DataTable dt;
            try
            {
                string nombreStoredProcedure = "ObtenerClientes";
                SetStoredProcedure(nombreStoredProcedure);
                dt = EjecutarComando();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerClientes(): " + ex.Message);
            }

            return dt;
        }

        public DataTable ObtenerClientePorId(int id)
        {
            DataTable dt;
            try
            {
                string nombreStoredProcedure = "ObtenerClientePorId";
                SetStoredProcedure(nombreStoredProcedure);
                SetParameter("@id", id);
                dt = EjecutarComando();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerClientePorId("+ id +"): " + ex.Message);
            }

            return dt;
        }

        public bool CrearCliente(Cliente cliente)
        {
            bool creado = false;
            try
            {
                string nombreStoredProcedure = "CrearCliente";
                SetStoredProcedure(nombreStoredProcedure);
                SetParameter("@cuit", cliente.CUIT);
                SetParameter("@razonSocial", cliente.RazonSocial);
                SetParameter("@telefono", cliente.Telefono);
                SetParameter("@direccion", cliente.Direccion);
                SetParameter("@activo", cliente.Activo);
                DataTable resultado = EjecutarComando();

                creado = resultado != null && resultado.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en CrearCliente: " + ex.Message);
            }

            return creado;

        }

        public bool ModificarCliente(Cliente cliente)
        {
            bool modificado = false;
            try
            {
                string nombreStoredProcedure = "ModificarCliente";
                SetStoredProcedure(nombreStoredProcedure);
                SetParameter("@id", cliente.Id);
                SetParameter("@cuit", cliente.CUIT);
                SetParameter("@razonSocial", cliente.RazonSocial);
                SetParameter("@telefono", cliente.Telefono);
                SetParameter("@direccion", cliente.Direccion);
                SetParameter("@activo", cliente.Activo);
                DataTable resultado = EjecutarComando();

                modificado = resultado != null && resultado.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ModificarCliente: " + ex.Message);
            }
            return modificado;
        }

        public bool EliminarCliente(int id)
        {
            bool eliminado = false;
            try
            {
                string nombreStoredProcedure = "EliminarCliente";
                SetStoredProcedure(nombreStoredProcedure);
                SetParameter("@id", id);
                DataTable resultado = EjecutarComando();

                eliminado = resultado != null && resultado.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en EliminarCliente: " + ex.Message);
            }

            return eliminado;
        }



    }
}