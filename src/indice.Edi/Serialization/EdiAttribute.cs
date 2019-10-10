using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// Base class for <see cref="EdiAttribute"/>s 
    /// </summary>
    public abstract class EdiAttribute : Attribute
    {
        /// <summary>
        /// String representation of this attribute.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Regex.Replace(GetType().Name, "Edi(.*)Attribute", "$1");
        }
    }
}
