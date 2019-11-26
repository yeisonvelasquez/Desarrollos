namespace DAL.Util
{
    using System;
    using System.Reflection;

    public static class Reflection
    {
        public static T CreateObject<T>(params Object[] parameters)
        {
            Type type = typeof(T);

            Type[] typesParameters = new Type[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                typesParameters[i] = parameters[i].GetType();
            }

            ConstructorInfo cInfo = type.GetConstructor(typesParameters);
            Object ObjectCreated = cInfo.Invoke(parameters);

            return (T)ObjectCreated;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nameProperty"></param>
        /// <param name="value"></param>
        /// <returns>verdadero si en la propiedad es posible escribir, falso en caso contrario</returns>
        public static bool SetValueProperty(Object obj, String nameProperty, Object value)
        {
            Type type = obj.GetType();
            PropertyInfo pInfo = type.GetProperty(nameProperty);
            return pInfo != null ? SetValueProperty(obj, pInfo, value) : false;
        }
        /// <summary>
        /// Este es mas rapido si ya se tiene la propiedad
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetValueProperty(Object obj, PropertyInfo pInfo, Object value)
        {
            if (pInfo.CanWrite)
            {
                pInfo.SetValue(obj, value, null);
                return true;
            }
            return false;
        }

        public static T GetValueProperty<T>(Object obj, String nameProperty)
        {
            return (T)getValuePropertyG(obj, nameProperty);
        }

        private static Object getValuePropertyG(Object obj, String nameProperty)
        {
            Type type = obj.GetType();
            PropertyInfo pInfo = type.GetProperty(nameProperty);
            if (pInfo.CanRead)
            {
                Object value = pInfo.GetValue(obj, null);
                return value;
            }
            return null;
        }
    }
}
