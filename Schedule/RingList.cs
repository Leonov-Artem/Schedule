using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Schedule
{
    public class RingList<T>
    {
        LinkedList<T> _list;
        LinkedListNode<T> _current;

        public RingList(T[] array)
        {
            _list = new LinkedList<T>(array);
            _current = _list.First;
        }

        public T Next
        {
            get
            {
                var value = _current.Value;

                if (_current.Next != null)
                    _current = _current.Next;
                else
                    _current = _list.First;

                return value;
            }
        }

        public T Current
        {
            get => _current.Value;
        }

        public T Previous
        {
            get
            {
                if (_current.Previous != null)
                    _current = _current.Previous;
                else
                    _current = _list.Last;

                return _current.Value;
            }
        }

        public void Reset()
        {
            _current = _list.First;
        }
    }
}
