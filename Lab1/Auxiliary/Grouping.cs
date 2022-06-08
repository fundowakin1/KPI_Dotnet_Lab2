using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.Auxiliary
{
    public class Grouping<TKey, TValue> : IGrouping<TKey, TValue>
    {
        public IEnumerable<TValue> Values { get; set; }
        public TKey Key { get; set; }

        public Grouping() { }
        public Grouping(IGrouping<TKey, TValue> grouping)
        {
            if (grouping == null)
                throw new ArgumentNullException(nameof(grouping));
            Key = grouping.Key;
            Values = grouping;
        }
        public IEnumerator<TValue> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
