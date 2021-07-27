namespace Application.Events
{
	//public record ProductShipped(string Sku, int Quantity, System.DateTime Timestamp) : IEvent;

	public class ProductShipped : object, IEvent
	{
		public ProductShipped(string sku, int quantity) : base()
		{
			Sku = sku;
			Quantity = quantity;

			Timestamp = System.DateTime.UtcNow;
		}

		public string Sku { get; }

		public int Quantity { get; }

		public System.DateTime Timestamp { get; }

		public override string ToString()
		{
			string result =
				$"Event: ProductShipped - Sku: { Sku } - Quantity: { Quantity } - Timestamp: {Timestamp:yyyy/MM/dd - HH:mm:ss}";

			return result;
		}
	}

}
