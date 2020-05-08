//-----------------------------------------------------------------------
// <copyright file="CollectionTemplate.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This class is used by the collections that share common methods for adding, removing, and retrieving objects regardless of type.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    /// <summary>
    /// A generic template of common methods used by each collection.
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
        /// Gets the collection.
        /// </summary>
        /// <returns></returns>
        public List<T> Collection { get; } = new List<T>();

        /// <summary>
        /// Adds an object to the collection.
        /// </summary>
        /// <param name="object"></param>
        public virtual void Add(T @object)
        {
            Collection.Add(@object);
        }

        /// <summary>
        /// Removes an object from the collection.
        /// </summary>
        /// <param name="object"></param>
        public void Remove(T @object)
        {
            Collection.Remove(@object);
        }

        /// <summary>
        /// Gets the number of objects in the collection.
        /// </summary>
        public int Count
        {
            get { return Collection.Count; }
        }

        /// <summary>
        /// Gets a particular object from the collection.
        /// </summary>
        /// <param name="objectToFind">The object to find in the collection.</param>
        /// <returns>The object that has been found in the collection.</returns>
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
        /// Gets an object from the collection based on its name.
        /// </summary>
        /// <param name="name">The name of the object to find.</param>
        /// <returns>The object that has been found based on its name.</returns>
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
