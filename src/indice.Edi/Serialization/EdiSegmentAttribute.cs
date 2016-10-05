using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// <see cref="EdiSegmentAttribute"/> marks a propery/class to be deserialized for a given segment. Used in conjunction with EdiPath
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiSegmentAttribute : EdiStructureAttribute
    {


    }
}
