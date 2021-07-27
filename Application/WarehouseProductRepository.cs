namespace Application
{
	public class WarehouseProductRepository : object
	{
		public WarehouseProductRepository() : base()
		{
			InMemoryDatabase =
				new System.Collections.Generic.Dictionary
				<string, System.Collections.Generic.List<Events.IEvent>>();
		}

		private System.Collections.Generic.Dictionary
			<string, System.Collections.Generic.List<Events.IEvent>> InMemoryDatabase
		{ get; }

		public void Save(WarehouseProduct warehouseProduct)
		{
			if (InMemoryDatabase.ContainsKey(warehouseProduct.Sku) == false)
			{
				InMemoryDatabase.Add(warehouseProduct.Sku,
					new System.Collections.Generic.List<Events.IEvent>());
			}

			var newEvents =
				warehouseProduct.GetUncommittedEvents();

			InMemoryDatabase[warehouseProduct.Sku].AddRange(newEvents);

			//foreach (var currentEvent in newEvents)
			//{
			//	InMemoryDatabase[warehouseProduct.Sku].Add(currentEvent);
			//}

			warehouseProduct.EventsCommitted();
		}

		public WarehouseProduct Get(string sku)
		{
			var warehouseProduct =
				new WarehouseProduct(sku);

			if (InMemoryDatabase.ContainsKey(sku))
			{
				foreach (var currentEvent in InMemoryDatabase[sku])
				{
					warehouseProduct.ApplyEvent(currentEvent);
				}
			}

			return warehouseProduct;
		}
	}
}
