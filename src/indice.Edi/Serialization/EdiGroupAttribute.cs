using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiGroupAttribute : EdiAttribute
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
