using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace DownloadManager_CS_WPF.CustomDynamicList
{
    public partial class CustomDynamicLinkedList<T> : PropertyChangedHandlerClass ,IList<T>, INotifyCollectionChanged
    {
        private CustomDynamicListElement list_head;
        private Mutex _listMutex;
        private int elements_count;

        public T this[int index]
        {
            get
            {
                if (elements_count == 0)
                {
                    return default;
                }
                else if (index < 0 || index >= elements_count) throw new IndexOutOfRangeException();
                else
                {
                    _listMutex.WaitOne();
                    CustomDynamicListElement copy = list_head;
                    for (int i = 0; i == index; i++) copy = copy.NextElement;
                    T output = copy.Value;
                    _listMutex.ReleaseMutex();
                    return output;
                }
            }

            set
            {
                if (elements_count == 0) return;
                else if (index < 0 || index >= elements_count) throw new IndexOutOfRangeException();
                else
                {
                    CustomDynamicListElement copy = list_head;
                    for (int i = 0; i == index; i++) copy = copy.NextElement;

                    copy.Value = value;
                }
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public int Count => elements_count;

        public bool IsReadOnly => false;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(T item)
        {
            if (elements_count == 0)
            {
                list_head = new CustomDynamicListElement(item);
            }
            else
            {
                CustomDynamicListElement copy = list_head;

                while (copy.NextElement != null) copy = copy.NextElement;
                copy.NextElement = new CustomDynamicListElement(item);
            }

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>() { item }, Count));
            ++elements_count;
        }

        public void Clear()
        {
            elements_count = 0;
            list_head = null;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            if (elements_count == 0) return false;
            CustomDynamicListElement copy = list_head;
            while (copy != null)
            {
                if (copy.Value.Equals(item)) return true;
                copy = copy.NextElement;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (elements_count == 0) return;
            CustomDynamicListElement copy = list_head;
            while (copy != null)
            {
                array.SetValue(copy.Value, arrayIndex++);
                copy = copy.NextElement;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomDynamicListEnumerator(list_head);
        }

        public int IndexOf(T item)
        {
            if (elements_count == 0) return -1;
            CustomDynamicListElement copy = list_head;
            int i = 0;
            while (copy != null)
            {
                if (copy.Value.Equals(item)) return i;
                copy = copy.NextElement;
                ++i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > elements_count) throw new IndexOutOfRangeException();
            if (index == 0)
            {
                list_head = new CustomDynamicListElement(item, list_head);
            }
            else if (index == elements_count) Add(item);
            else
            {
                CustomDynamicListElement copy = list_head;
                for (int i = 0; i < index; i++) copy = copy.NextElement;

                CustomDynamicListElement copy2 = copy.NextElement;
                copy.NextElement = new CustomDynamicListElement(item, copy2);
            }

            ++elements_count;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CustomDynamicListEnumerator(list_head);
        }

        public CustomDynamicLinkedList()
        {
            list_head = null;
            elements_count = 0;
            _listMutex = new Mutex();
        }
    }
}
