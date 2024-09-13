namespace indice.Edi.Tests.Models;

public class Order9
{
    public Order9Type OrderType { get; set; }
    
    public Order9Supplier Supplier { get; set; }
    
    public Order9Customer Customer { get; set; }

    public string CustomerDepotGLN { get; set; }

    public string CustomerDepotCode { get; set; }
    public string CustomerDepot { get; set; }
    public string CustomerDepotAddress { get; set; }

    public string CustomerOrderNo { get; set; }

    public string SupplierOrderNo { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DepotDateTime { get; set; }
    
    public List<Order9Line> Order9Lines { get; set; }
    
}

public class Order9Line 
{
    public string LineNo { get; set; }

    public string ProductSupplierGTIN { get; set; }

    public string ProductSupplierCode { get; set; }

    public string ProductGTIN { get; set; }

    public string ProductCustomerGTIN { get; set; }

    public string ProductCustomerCode { get; set; }

    public int OrderUnit { get; set; }

    public int OrderQty { get; set; }

    public decimal OrderUnitPrice { get; set; }

    public string ProductDescription { get; set; }
}

public class Order9Customer
{

    public string CustomerGLN { get; set; }

    public string CustomerName { get; set; }
}

public class Order9Supplier
{
    public string SupplierGLN { get; set; }

    public string SupplierName { get; set; }

}

public class Order9Type
{
    public string OrderTypeCode { get; set; }

    public string OrderType { get; set; }

    public string FileGenerationNo { get; set; }

    public string FileVersionNo { get; set; }

    public DateTime FileCreationDate { get; set; }

    public string FileName { get; set; }
}
