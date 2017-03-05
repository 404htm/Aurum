using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Aurum.Core
{
    [DataContract(Namespace = "", IsReference = false)]
    public class StoreableSet<T> : Storeable<StoreableSet<T>>, IList<T>
    {
        [DataMember]
        private IList<T> Items { get; set; }

        public StoreableSet(IList<T> list)
        {
            this.Items = list;
        }

        public T this[int index]
        {
            get { return ((IList<T>)Items)[index]; }
            set { ((IList<T>)Items)[index] = value; }
        }

        public int Count { get { return ((IList<T>)Items).Count; }}

        public bool IsReadOnly { get { return ((IList<T>)Items).IsReadOnly; }}

        public void Add(T item) { ((IList<T>)Items).Add(item); }

        public void Clear() { ((IList<T>)Items).Clear(); }

        public bool Contains(T item) { return ((IList<T>)Items).Contains(item); }

        public void CopyTo(T[] array, int arrayIndex) { ((IList<T>)Items).CopyTo(array, arrayIndex); }

        public IEnumerator<T> GetEnumerator() { return ((IList<T>)Items).GetEnumerator(); }

        public int IndexOf(T item) { return ((IList<T>)Items).IndexOf(item); }

        public void Insert(int index, T item) { ((IList<T>)Items).Insert(index, item); }

        public bool Remove(T item) { return ((IList<T>)Items).Remove(item); }

        public void RemoveAt(int index) { ((IList<T>)Items).RemoveAt(index); }

        IEnumerator IEnumerable.GetEnumerator() { return ((IList<T>)Items).GetEnumerator(); }
    }
}
