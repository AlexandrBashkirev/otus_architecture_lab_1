using System;
using System.Collections.Generic;


namespace otus_architecture_lab1
{
    class SimpleServiceLocator
    {
        #region Variables

        private static SimpleServiceLocator instance = null;
        private static readonly object lockObj = new object();

        private Dictionary<Type, object> objects = new Dictionary<Type, object>();

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

        #endregion



        #region Methods

        public T GetService<T>() where T : class
        {
            Type type = typeof(T);
            if (objects.ContainsKey(type))
            {
                return objects[type] as T;
            }

            Console.WriteLine($"Unknown service for type {type.Name}");

            return null;
        }


        public void RegisterService<T>(object service)
        {
            Type type = typeof(T);
            if (objects.ContainsKey(type))
            {
                objects[type] = service;
            }
            else
            {
                objects.Add(type, service);
            }
        }

        #endregion
    }
}
