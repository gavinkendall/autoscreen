//-----------------------------------------------------------------------
// <copyright file="CollectionTemplate.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionTemplate<T> : IEnumerable<T>
    {
        /// <summary>
        /// Returns the enumerator for the collection.
        /// </summary>
        /// <returns>A list of objects of a generic type.</returns>
        public List<T>.Enumerator GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)Collection).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> Collection { get; } = new List<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public virtual void Add(T @object)
        {
            Collection.Add(@object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void Remove(T @object)
        {
            Collection.Remove(@object);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return Collection.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToFind"></param>
        /// <returns></returns>
        public T Get(T @objectToFind)
        {
            foreach (T @object in Collection)
            {
                if (@object.Equals(@objectToFind))
                {
                    return @object;
                }
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetByName(string name)
        {
            foreach (T @object in Collection)
            {
                Type t = @object.GetType();

                if (t.GetProperty("Name").GetValue(@object, null).Equals(name))
                {
                    return @object;
                }
            }

            return default;
        }
    }
}
