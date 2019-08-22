using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HnefataflAI.Commons.DataTypes
{
    /// <summary>
    /// thread safe implementation of a HashSet
    /// using a ReaderWriterLockSlim
    /// Courtesy of https://github.com/Bardaky
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentHashSet<T> : IDisposable, IEnumerable<T>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly HashSet<T> _hashSet = new HashSet<T>();

        public ConcurrentHashSet()
        {

        }

        /// <summary>
        /// create new concurrent HashSet and initializes the source collection
        /// write locks the HashSet for the duration of the initialization
        /// </summary>
        /// <param name="collection">collection to initialize the HashSet with</param>
        public ConcurrentHashSet(IEnumerable<T> collection)
        {
            try
            {
                _lock.EnterWriteLock();

                _hashSet = new HashSet<T>(collection);
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// add an item to the HashSet
        /// write locks the HashSet for the duration of the action
        /// </summary>
        /// <param name="item"></param>
        /// <returns>returns true if action was successful</returns>
        public bool Add(T item)
        {
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Add(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// clears the HashSet
        /// write locks the HashSet for the duration of the action
        /// </summary>
        public void Clear()
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.Clear();
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// check if the HashSet contains the item
        /// read locks the HashSet for the duration of the action
        /// </summary>
        /// <param name="item">item to check in the HashSet for</param>
        /// <returns>returns true if the HashSet contains the item</returns>
        public bool Contains(T item)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.OfType<T>().Contains(item);
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// remove the item from the HashSet
        /// write locks the HashSet for the duration of the action
        /// </summary>
        /// <param name="item"></param>
        /// <returns>returns true if the action was successful</returns>
        public bool Remove(T item)
        {
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Remove(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// count and return the amount of items in the HashSet
        /// read locks the HashSet during the action
        /// </summary>
        /// <returns>returns the amount of items in the HashSet</returns>
        public int Count
        {
            get
            {
                try
                {
                    _lock.EnterReadLock();
                    return _hashSet.Count;
                }
                finally
                {
                    if (_lock.IsReadLockHeld)
                        _lock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// returns the raw source collection
        /// read locks the HashSet during the action
        /// </summary>
        /// <returns>returns the raw HashSet</returns>
        public HashSet<T> Raw
        {
            get
            {
                try
                {
                    _lock.EnterReadLock();
                    return _hashSet;
                }
                finally
                {
                    if (_lock.IsReadLockHeld)
                        _lock.ExitReadLock();
                }
            }
        }

        #region Dispose

        /// <summary>
        /// dispose of the lock
        /// </summary>
        public void Dispose()
        {
            if (_lock != null)
                _lock.Dispose();
        }

        #endregion

        #region Enumerate

        /// <summary>
        /// get the enumerator of the HashSet
        /// read locks the HashSet during the action
        /// </summary>
        /// <returns>return a SafeEnumerator of the HashSet</returns>
        public IEnumerator<T> GetEnumerator()
        {
            try
            {
                _lock.EnterReadLock();
                return new SafeEnumerator<T>(_hashSet.GetEnumerator(), _lock);
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// get the enumerator of the HashSet
        /// read locks the HashSet during the action
        /// </summary>
        /// <returns>return a SafeEnumerator of the HashSet</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            try
            {
                _lock.EnterReadLock();
                return (this as IEnumerable<T>).GetEnumerator();
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }

        #endregion
    }

    /// <summary>
    /// thread safe IEnumerator implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeEnumerator<T> : IEnumerator<T>
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;
        private readonly IEnumerator<T> _innerCollection;

        /// <summary>
        /// create a new thread safe IEnumerator
        /// enters read lock
        /// </summary>
        /// <param name="innerCollection">IEnumerator<T> of the source</param>
        /// <param name="readerWriterLock">ReaderWriterLock to use</param>
        public SafeEnumerator(IEnumerator<T> innerCollection, ReaderWriterLockSlim readerWriterLock)
        {
            _innerCollection = innerCollection;
            _readerWriterLock = readerWriterLock;
            _readerWriterLock.EnterReadLock();
        }

        /// <summary>
        /// exit the read lock
        /// </summary>
        public void Dispose()
        {
            _readerWriterLock.ExitReadLock();
        }

        /// <summary>
        /// move to the next item in the IEnumerator<T>
        /// </summary>
        /// <returns>returns true if the action was successful</returns>
        public bool MoveNext()
        {
            return _innerCollection.MoveNext();
        }

        /// <summary>
        /// reset the IEnumerator<T>
        /// </summary>
        public void Reset()
        {
            _innerCollection.Reset();
        }

        /// <summary>
        /// get the current item of the IEnumerator<T>
        /// </summary>
        /// <returns>returns the current item of the IEnumerator<T></returns>
        public T Current
        {
            get { return _innerCollection.Current; }
        }

        /// <summary>
        /// get the current item of the IEnumerator<T>
        /// </summary>
        /// <returns>returns the current item of the IEnumerator<T></returns>
        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
