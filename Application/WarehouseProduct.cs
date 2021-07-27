namespace Application
{
	/// <summary>
	/// انبار محصول
	/// </summary>
	public class WarehouseProduct : object
	{
		public WarehouseProduct(string sku) : base()
		{
			Sku = sku;

			CurrentProductState = new ProductState();

			AllEvents =
				new System.Collections.Generic.List<Events.IEvent>();

			UncommittedEvents =
				new System.Collections.Generic.List<Events.IEvent>();
		}

		/// <summary>
		/// Stock Keeping Units
		/// </summary>
		public string Sku { get; }

		/// <summary>
		/// Projection (Current State)
		/// </summary>
		private ProductState CurrentProductState { get; }

		private System.Collections.Generic.IList<Events.IEvent> AllEvents { get; }

		private System.Collections.Generic.IList<Events.IEvent> UncommittedEvents { get; }

		public void ShipProduct(int quantity)
		{
			if (quantity > CurrentProductState.Quantity)
			{
				throw new InvalidDomainException
					(message: "We do not have enough product to ship!");
			}

			AddEvent(new Events.ProductShipped(Sku, quantity));
		}

		public void ReceiveProduct(int quantity)
		{
			AddEvent(new Events.ProductReceived(Sku, quantity));
		}

		public void AdjustInventory(int quantity, string reason)
		{
			if (CurrentProductState.Quantity + quantity < 0)
			{
				throw new InvalidDomainException
					(message: "Cannot adjust to a negative quantity on hand!");
			}

			AddEvent(new Events.InventoryAdjusted(Sku, quantity, reason));
		}

		private void AddEvent(Events.IEvent currentEvent)
		{
			ApplyEvent(currentEvent);
			UncommittedEvents.Add(currentEvent);
		}

		public void ApplyEvent(Events.IEvent currentEvent)
		{
			switch (currentEvent)
			{
				case Events.ProductShipped shipProduct:
				{
					Apply(shipProduct);
					break;
				}

				case Events.ProductReceived receiveProduct:
				{
					Apply(receiveProduct);
					break;
				}

				case Events.InventoryAdjusted inventoryAdjusted:
				{
					Apply(inventoryAdjusted);
					break;
				}

				default:
				{
					throw new InvalidDomainException(message: "Unsupported Event!");
				}
			}

			AllEvents.Add(currentEvent);
		}

		private void Apply(Events.ProductShipped currentEvent)
		{
			CurrentProductState.Quantity -= currentEvent.Quantity;
		}

		private void Apply(Events.ProductReceived currentEvent)
		{
			CurrentProductState.Quantity += currentEvent.Quantity;
		}

		private void Apply(Events.InventoryAdjusted currentEvent)
		{
			CurrentProductState.Quantity += currentEvent.Quantity;
		}

		public System.Collections.Generic.IList<Events.IEvent> GetAllEvents()
		{
			//return AllEvents;

			return new System.Collections.Generic.List<Events.IEvent>(AllEvents);
		}

		public System.Collections.Generic.IList<Events.IEvent> GetUncommittedEvents()
		{
			//return UncommittedEvents;

			return new System.Collections.Generic.List<Events.IEvent>(UncommittedEvents);
		}

		public void EventsCommitted()
		{
			UncommittedEvents.Clear();
		}

		public ProductState GetCurrentProductState()
		{
			return CurrentProductState;
		}
	}
}
