namespace SampleStoreCodingTest.Models.Dtos
{
    public class CustomerOrderPredictionDto
    {
        public int CustId { get; set; }
        public string CustomerName { get; set; } = default!;
        public DateTime LastOrderDate { get; set; }
        public DateTime? NextPredictedOrder { get; set; }
    }
}
