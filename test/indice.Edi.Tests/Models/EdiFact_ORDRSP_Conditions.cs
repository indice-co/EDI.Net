using indice.Edi.Serialization;
using System;
using System.Collections.Generic;

namespace indice.Edi.Tests.Models
{


    public class Interchange_ORDRSP
    {
        public Message_ORDRSP Message { get; set; }
    }

    [EdiMessage]
    public class Message_ORDRSP
    {
        [EdiCondition("Z01", Path = "IMD/1/0")]
        [EdiCondition("Z10", Path = "IMD/1/0")]
        public List<IMD> IMD_List { get; set; }

        [EdiCondition("Z01", "Z10", CheckFor = EdiConditionCheckType.NotEqual, Path = "IMD/1/0")]
        public IMD IMD_Other { get; set; }

        /// <summary>
        /// Item Description
        /// </summary>
        [EdiSegment, EdiPath("IMD")]
        public class IMD
        {
            [EdiValue(Path = "IMD/0")]
            public string FieldA { get; set; }

            [EdiValue(Path = "IMD/1")]
            public string FieldB { get; set; }

            [EdiValue(Path = "IMD/2")]
            public string FieldC { get; set; }
        }
    }
}
