using System.Collections.Generic;

namespace indice.Edi.Models
{
    public class Group : Segment
    {
        private readonly IList<Message> _Messages;

        public Group() {
            _Messages = new List<Message>();
        }

        public IList<Message> Messages {
            get { return _Messages; }
        }
    }
}
