using System;
using System.Collections.Generic;


namespace otus_architecture_lab1
{
    class SimpleServiceLocator : IDisposable
    {
        #region Variables

        private static SimpleServiceLocator instance = null;
        private static readonly object lockObj = new object();

        private Dictionary<Type, object> services = new Dictionary<Type, object>();

        #endregion



        #region Properties

        public static SimpleServiceLocator Instance {
            get
            {
                if(instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new SimpleServiceLocator();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion



        #region Class lyfecicle

        private SimpleServiceLocator()
        {
        }


        public void Dispose()
        {
            lock (lockObj)
            {
                foreach (var service in services)
                {
                    (service.Value as IDisposable).Dispose();
                }
                services.Clear();
                instance = null;
            }
        }

        #endregion



        #region Methods

        public T GetService<T>() where T : class, IDisposable
        {
            T result = null;

            Type type = typeof(T);
            lock (lockObj)
            {
                if (services.ContainsKey(type))
                {
                    result = services[type] as T;
                }
            }

            if (result == null)
            {
                throw new Exception($"Unknown service for type {type.Name}");
            }

            return result;
        }


        public void RegisterService<T>(object service)
             where T : class, IDisposable
        {
            Type type = typeof(T);

            lock (lockObj)
            {
                if (services.ContainsKey(type))
                {
                    services[type] = service;
                }
                else
                {
                    services.Add(type, service);
                }
            }
        }

        #endregion
    }
}
