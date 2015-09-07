using System.Collections.Generic;

namespace indice.Edi.Models
{
    public class Segment
    {
        private readonly IList<Element> _Elements;
        public Segment() {
            _Elements = new List<Element>();
        }

        public IList<Element> Elements {
            get {
                return _Elements;
            }
        }
    }
}
