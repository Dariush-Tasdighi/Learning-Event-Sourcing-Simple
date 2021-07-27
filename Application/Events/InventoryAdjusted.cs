namespace Application.Events
{
	//public record InventoryAdjusted(string Sku, int Quantity, string Reason, System.DateTime Timestamp) : IEvent;

	public class InventoryAdjusted : object, IEvent
	{
		public InventoryAdjusted(string sku, int quantity, string reason) : base()
		{
			Sku = sku;
			Reason = reason;
			Quantity = quantity;

			Timestamp = System.DateTime.UtcNow;
		}

		public string Sku { get; }

		public int Quantity { get; }

		public string Reason { get; }

		public System.DateTime Timestamp { get; }

		public override string ToString()
		{
			string result =
				$"Event: InventoryAdjusted - Sku: { Sku } - Quantity: { Quantity } - Reason: { Reason } - Timestamp: {Timestamp:yyyy/MM/dd - HH:mm:ss}";

			return result;
		}
	}
}
