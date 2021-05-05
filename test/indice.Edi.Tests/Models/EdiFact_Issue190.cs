using System.Collections.Generic;
using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models
{
    public class EdiFact_Issue190
    {
        [EdiSegment(Description = "Traveller information"), EdiPath("TIF")]
        public class TIF
        {
            public List<GivenName> Names { get; set; }
        }
        
        [EdiSegment(Description = "Traveller information"), EdiPath("TIF")]
        public class BUGGEDTIF
        {
            [EdiValue("X(3)", Description = "C322-6353", Path = "TIF/0/1")]          // Added prop only
            public string Category { get; set; }
        
            public List<GivenName> Names { get; set; }
        }

        [EdiElement, EdiPath("TIF/1..*")]
        public class GivenName
        {
            [EdiValue("X(48)", Description = "C324-9942", Path = "TIF/*/0")]
            public string Name { get; set; }

            public GivenName() { }

            public GivenName(string name) => Name = name;

            public static implicit operator GivenName(string s) => new GivenName { Name = s };

            public static implicit operator string(GivenName n) => n.Name;
        }
    }
}