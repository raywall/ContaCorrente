using System;
using System.Collections.Generic;
using System.Reflection;

namespace ContaCorrente.DataAccess
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Efetua a conversão dos dados recebidos para o objeto solicitado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T ConvertModel<T>(this System.Data.SqlClient.SqlDataReader reader) where T : new()
        {
            var model = new T();

            Type modelType = model.GetType();
            IList<PropertyInfo> properties = new List<PropertyInfo>(modelType.GetProperties());

            for (int i = 0; i < properties.Count; i++)
                properties[i].SetValue(model, reader[i]);

            return model;
        }
    }
}
