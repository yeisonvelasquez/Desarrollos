namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Entities;
    using System.Transactions;

    public abstract class Repository<T, K>
    {
        protected string connectionString;
        protected Result<string> ExecSPReturnMessage(string nombreProcedimiento, KeyValuePair<string, object>[] parametros, bool msgReturn)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        for (int p = 0; parametros != null && p < parametros.Length; p++)
                        {

                            cmd.Parameters.AddWithValue(parametros[p].Key, parametros[p].Value);
                        }
                        try
                        {
                            con.Open();
                        }
                        catch (SqlException exc)
                        {
                            string fuente = (typeof(T)).Name;
                            return new Result<string>(string.Empty, exc.Message);
                        }

                        //Retorno de Valor para Diligenciar Formulario
                        ///<sumary>
                        ///Agrego mi parametro de Salida dependiendo de si estoy consultando o ingresando información
                        ///<sumary>
                        if (msgReturn)
                        {

                            SqlParameter pMessage = cmd.Parameters.Add("pMessage", SqlDbType.NVarChar, 256);
                            pMessage.Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            result = cmd.Parameters["pMessage"].Value.ToString();
                        }
                        else
                        {
                            result = cmd.ExecuteNonQuery().ToString();
                            con.Close();
                        }
                    }
                }

                return new Result<string>(String.Empty, result);
            }
            catch (SqlException oExc)
            {
                string fuente = (typeof(T)).Name;
                return new Result<string>(string.Empty, oExc.Message);
            }

        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que retorna una lista de objetos tipo T
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="parametros">parámetros del procedimiento almacenado</param>
        protected Result<IList<T>> ExecSPReturnListR(string nombreProcedimiento, KeyValuePair<string, object>[] parametros)
        {
            IList<T> result = null;  
            SqlDataReader dataReader = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        for (int p = 0; parametros != null && p < parametros.Length; p++)
                        {
                            cmd.Parameters.AddWithValue(parametros[p].Key, parametros[p].Value);
                        }
                        SqlParameter pMessage = cmd.Parameters.Add("pMessage", SqlDbType.NVarChar, 256);
                        pMessage.Direction = ParameterDirection.Output;
                        try
                        {
                            con.Open();
                        }
                        catch (Exception sx)
                        {
                            string fuente = (typeof(T)).Name;
                            return new Result<IList<T>>(result, sx.Message);
                        }

                        dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        result = MapearEntidad(dataReader);

                        con.Close();
                    }
                }

                return new Result<IList<T>>(result, String.Empty);
            }
            catch (SqlException oEx)
            {
                string fuente = (typeof(T)).Name;
                return new Result<IList<T>>(null, oEx.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que retorna un valor numérico.
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="parametros">Parámetros del procedimiento almacenado</param>
        protected Result<int> ExcecSPReturnValue(string nombreProcedimiento, KeyValuePair<string, object>[] parametros)
        {
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            for (int p = 0; parametros != null && p < parametros.Length; p++)
                            {

                                cmd.Parameters.AddWithValue(parametros[p].Key, parametros[p].Value);
                            }
                            try
                            {
                                con.Open();
                            }
                            catch (SqlException sx)
                            {
                                return new Result<int>(result, sx.Message);

                            }
                            result = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    scope.Complete();
                }

                return new Result<int>(result, String.Empty);
            }
            catch (SqlException oExc)
            {
                return new Result<int>(0, oExc.Message);
            }

        }
        protected virtual IList<T> MapearEntidad(System.Data.IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
