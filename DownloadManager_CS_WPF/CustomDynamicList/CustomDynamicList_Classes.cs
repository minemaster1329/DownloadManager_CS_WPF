using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Diagnostics;

namespace DownloadManager_CS_WPF.CustomDynamicList
{
    public partial class CustomDynamicLinkedList<T> : PropertyChangedHandlerClass, IList<T>, INotifyCollectionChanged
    {
        private class CustomDynamicListElement : PropertyChangedHandlerClass
        {
            CustomDynamicListElement _nextElement;
            T _value;

            public CustomDynamicListElement(T value, CustomDynamicListElement next = null)
            {
                _value = value;
                _nextElement = null;
            }

            public CustomDynamicListElement NextElement
            {
                get => _nextElement;
                set
                {
                    _nextElement = value;
                    OnPropertyChanged();
                }
            }

            public T Value
            {
                get => _value;
                set
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        private class CustomDynamicListEnumerator : IEnumerator<T>
        {
            int position = -1;
            CustomDynamicListElement _list_head;
            CustomDynamicListElement _currentElement;
            T _currentElementValue;
            public T Current => _currentElementValue;

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_currentElement is null && _list_head is null) return false;
                if (_currentElement != null && _currentElement.NextElement != null)
                {
                    _currentElement = _currentElement.NextElement;
                    _currentElementValue = _currentElement.Value;
                    ++position;
                    return true;
                }

                else if (_currentElement is null && _list_head != null)
                {
                    _currentElement = _list_head;
                    _currentElementValue = _list_head.Value;
                    ++position;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                position = 0;
                _currentElement = _list_head;
            }

            public CustomDynamicListEnumerator(CustomDynamicListElement list_head)
            {
                _list_head = list_head;
                _currentElement = null;
                position = -1;
                _currentElementValue = default(T);
            }
        }
    }
}
