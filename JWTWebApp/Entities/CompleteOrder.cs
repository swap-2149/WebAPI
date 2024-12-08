namespace JWTWebApp.Entities
{
    public class CompleteOrder
    {
        public Order order { get; set; }

        public string deliveryStatus { get; set; }

        public CompleteOrder(Order order, string deliveryStatus)
        {
            this.order = order;
            this.deliveryStatus = deliveryStatus;
        }
    }
}
