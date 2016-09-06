using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
    public enum EdiStructureType
    {
        None = 0,
        Interchange = 1,
        Group = 2,
        Message = 3,
        SegmentGroup = 4,
        Segment = 5,
        Element = 6
    }
}
