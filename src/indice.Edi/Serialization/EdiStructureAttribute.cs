using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    public abstract class EdiStructureAttribute : EdiAttribute
    {
        private bool _Mandatory;
        private string _Description;

        public bool Mandatory {
            get { return _Mandatory; }
            set { _Mandatory = value; }
        }

        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
