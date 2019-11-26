namespace DAL.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Entities;
    public class RepositoryMapper<T> : Repository<T, Object>
    {
        protected override IList<T> MapearEntidad(IDataReader reader)
        {
            IList<T> Result = new List<T>();

            if (!reader.IsClosed)
            {
                while (reader.Read())
                {
                    T Entidad = Reflection.CreateObject<T>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        String columnName = reader.GetName(i);
                        Object value = GetValue(reader, columnName);
                        if (value != null)
                            Reflection.SetValueProperty(Entidad, GetPropertyName(columnName), value);
                    }
                    Result.Add(Entidad);
                }
            }

            return Result;
        }

        /// <summary>
        ///  Obtiene el valor para cierto campo en el SQL dataReader      
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="nameField"></param>
        /// <returns>si no lo encuentra sera DBNull.Value </returns>
        private Object GetValue(IDataReader sdr, string nameField)
        {
            try
            {
                Object value = sdr[nameField];
                return value == DBNull.Value ? null : value;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        private Hashtable hash = new Hashtable();

        private String GetPropertyName(String fieldName)
        {
            if (fieldName.Length > 1)
            {
                if (hash.Contains(fieldName))
                    return (String)hash[fieldName];
                else
                {
                    String name = String.Format("{0}{1}", fieldName.Substring(0, 1).ToUpper(), fieldName.Substring(1));
                    hash[fieldName] = name;
                    return name;
                }

            }
            return fieldName;
        }



        /// <summary>
        /// Procedimiento almacenado que devuelve un valor de tipo texto, se usa para parametros de output con tipo texto
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="parametros">parámetros del procedimiento almacenado</param>
        /// <returns></returns>
        protected Result<string> ExecSPReturnMessage(string nombreProcedimiento, params KeyValuePair<string, object>[] parametros)
        {
            String spName = String.Format("{0}", nombreProcedimiento);
            return ExecSPReturnMessage(spName, parametros, true);
        }

        protected Result<int> ExecSPReturnValue(string nombreProcedimiento, params KeyValuePair<string, object>[] parametros)
        {
            String spName = String.Format("{0}", nombreProcedimiento);
            return ExcecSPReturnValue(spName, parametros);
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que retorna una lista de objetos tipo T
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado a ejecutar</param>
        protected Result<IList<T>> ExecSPReturnList(String nombreProcedimiento, params KeyValuePair<string, object>[] parametros)
        {
            String spName = String.Format("{0}", nombreProcedimiento);
            return ExecSPReturnListR(spName, parametros);
        }

        protected Result<IList<T>> ExecSPReturnListAndValue(String nombreProcedimiento, params KeyValuePair<string, object>[] parametros)
        {
            String spName = String.Format("{0}", nombreProcedimiento);
            return ExecSPReturnListR(spName, parametros);
        }
    }
}
