using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// Elements are considered to be groups of values otherwise known as groups of components. 
    /// One can use this attribute to deserialize into a complex class that resides inside a segment. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiElementAttribute : EdiStructureAttribute
    {
       
    }
}
