using indice.Edi.Serialization;

namespace indice.Edi.Tests.Models;

public class EdiFact_Issue170
{
    public Message Msg { get; set; }

    [EdiMessage]
    public class Message
    {
        public List<GID> Goods { get; set; }
    }

    [EdiElement]
    public class GoodsItemPackageDetails
    {
        [EdiValue("9(1)", Path = "GID/*/0", Mandatory = true)]
        public int PackageQuantity { get; set; }

        [EdiValue("X(17)", Path = "GID/*/1")]
        public string PackageTypeDescriptionCode { get; set; }
    }

    [EdiSegment(Description = "Goods item details - To indicate totals of a goods item."), EdiPath("GID")]
    public class GID
    {
        [EdiValue("9(1)", Path = "GID/0")]
        public int GoodsItemNumber { get; set; }

        [EdiPath("GID/1")]
        public GoodsItemPackageDetails FirstPackagingLevel { get; set; }

        [EdiPath("GID/2")]
        public GoodsItemPackageDetails SecondPackagingLevel { get; set; }

        [EdiPath("GID/3")]
        public GoodsItemPackageDetails ThirdPackagingLevel { get; set; }

        [EdiPath("GID/4")]
        public GoodsItemPackageDetails FourthPackagingLevel { get; set; }

        [EdiPath("GID/5")]
        public GoodsItemPackageDetails FifthPackagingLevel { get; set; }
    }
}
