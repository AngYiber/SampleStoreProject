namespace SampleStoreCodingTest.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? CustId { get; set; }
        public Customer? Customer { get; set; }

        public int EmpId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; } = null!;

        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public string ShipName { get; set; } = string.Empty;
        public string ShipAddress { get; set; } = string.Empty;
        public string ShipCity { get; set; } = string.Empty;
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string ShipCountry { get; set; } = string.Empty;

        public decimal Freight { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}