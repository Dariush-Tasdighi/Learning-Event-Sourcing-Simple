namespace Application.Events
{
	//public record ProductReceived(string Sku, int Quantity, System.DateTime Timestamp) : IEvent;

	public class ProductReceived : object, IEvent
	{
		public ProductReceived(string sku, int quantity) : base()
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
				$"Event: ProductReceived - Sku: { Sku } - Quantity: { Quantity } - Timestamp: {Timestamp:yyyy/MM/dd - HH:mm:ss}";

			return result;
		}
	}
}
