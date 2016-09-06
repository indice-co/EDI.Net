using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiSegmentAttribute : EdiStructureAttribute
    {


    }
}
