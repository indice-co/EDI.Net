using System.Collections.Generic;

namespace indice.Edi.Models
{
    public class Element
    {
        private readonly IList<Component> _Components;
        public Element() {
            _Components = new List<Component>();
        }

        public IList<Component> Components {
            get {
                return _Components;
            }
        }
    }
}
