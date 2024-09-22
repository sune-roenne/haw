using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ThousandAcreWoods.SVGTools.Util;
internal static class XmlNodeListExtensions
{

    public static List<TOut> AsList<TOut>(this XmlNodeList nodeList) where TOut : class
    {
        var enumer = new EnumerableImpl<TOut>(nodeList.GetEnumerator());
        var list = new List<TOut>(enumer); 
        return list;
    }

    private class EnumerableImpl<T> : IEnumerable<T> where T : class
    {

        private IEnumerator _nodeListEnumerator;

        public EnumerableImpl(IEnumerator nodeListEnumerator)
        {
            _nodeListEnumerator = nodeListEnumerator;
        }

        public IEnumerator<T> GetEnumerator() => Enumerate();

        public IEnumerator<T> Enumerate()
        {
            while (_nodeListEnumerator.MoveNext())
            {
                var next = _nodeListEnumerator.Current as T;
                if (next != null)
                    yield return next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var enumer = Enumerate();
            while(enumer.MoveNext())
                yield return enumer.Current;
        }
    }
}
