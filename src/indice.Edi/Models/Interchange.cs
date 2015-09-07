using System.Collections.Generic;

namespace indice.Edi.Models
{
    public class Interchange : Segment
    {
        private readonly IList<Group> _Groups;
        private readonly IList<Message> _Messages;

        public Interchange() {
            _Groups = new List<Group>();
            _Messages = new List<Message>();
        }

        public IList<Group> Groups {
            get { return _Groups; }
        }

        public IList<Message> Messages {
            get { return _Messages; }
        }
    }
}
