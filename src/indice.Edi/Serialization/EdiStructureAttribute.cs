using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// Base class for all structure difining attributes 
    /// </summary>
    public abstract class EdiStructureAttribute : EdiAttribute
    {
        private bool _Mandatory;
        private string _Description;

        /// <summary>
        /// Indicates that the current structure (Segment or Element) is mandatory or optional. By default this is false.
        /// </summary>
        public bool Mandatory {
            get { return _Mandatory; }
            set { _Mandatory = value; }
        }

        /// <summary>
        /// Helps by annotating the current member with a <see cref="Description"/>. Only for reference and documentation.
        /// </summary>
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
