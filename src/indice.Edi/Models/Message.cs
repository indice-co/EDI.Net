using System.Collections.Generic;

namespace indice.Edi.Models
{
    public class Message : Segment
    {
        private readonly IList<Segment> _Segments;

        public Message() {
            _Segments = new List<Segment>();
        }

        public IList<Segment> Segments {
            get { return _Segments; }
        }
    }
}
