using System;
using System.Collections;
using System.Collections.Generic;

namespace Koopakiller.Linq
{
    public sealed class RingEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    {
        #region Fields

        private readonly IEnumerator<T> _enumerator;

        private bool _lastMoveNextWasFalse = true;
        private bool _iterationStarted;
        private bool _useCache;

        private List<T> _cachedList;
        private int _currentCachedItemIndex = -1;
        private bool _cacheBuildComplete;

        #endregion

        #region .ctor

        public RingEnumerator(IEnumerable<T> source) : this(source?.GetEnumerator()) { }

        public RingEnumerator(IEnumerator<T> enumerator)
        {
            if (enumerator == null)
            {
                throw new ArgumentNullException(nameof(enumerator), $"{nameof(enumerator)} cannot be null.");
            }
            this._enumerator = enumerator;
        }

        #endregion

        #region Properties

        public bool UseCache
        {
            get { return this._useCache; }
            set
            {
                if (this._iterationStarted)
                {
                    throw new InvalidOperationException("The enumeration has already started.");
                }
                this._useCache = value;
            }
        }

        #endregion

        #region IEnumerator

        object IEnumerator.Current => this.Current;

        public T Current
        {
            get
            {
                if (this.UseCache && this._cacheBuildComplete)
                {
                    return this._cachedList[this._currentCachedItemIndex];
                }
                else
                {
                    return this._enumerator.Current;
                }
            }
        }

        public void Reset()
        {
            this._currentCachedItemIndex = -1;
            this._cacheBuildComplete = false;
            this._iterationStarted = false;
            this._enumerator.Reset();
        }

        public bool MoveNext()
        {
            if (!this._iterationStarted && this.UseCache)
            {
                this._cacheBuildComplete = false;
                this._cachedList = new List<T>();
            }

            if (this.UseCache && this._cacheBuildComplete)
            {
                ++this._currentCachedItemIndex;
                if (this._currentCachedItemIndex >= this._cachedList.Count)
                {
                    this._currentCachedItemIndex = 0;
                }
                return true;
            }
            else
            {
                this._iterationStarted = true;
                if (this._enumerator.MoveNext())
                {
                    this._lastMoveNextWasFalse = false;
                    if (this.UseCache)
                    {
                        this._cachedList.Add(this.Current);
                    }
                    return true;
                }

                if (this.UseCache)
                {
                    this._cacheBuildComplete = true;
                    this._currentCachedItemIndex = 0;
                    return this._cachedList.Count > 0;
                }
                else
                {
                    if (this._lastMoveNextWasFalse)
                    {
                        return false;
                    }
                    this._lastMoveNextWasFalse = true;
                    this._enumerator.Reset();
                    this._enumerator.MoveNext();
                    return true;
                }
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this._enumerator.Dispose();
        }

        #endregion

        #region IEnumerable

        public IEnumerator<T> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion
    }
}
