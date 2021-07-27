namespace Application
{
	public static class Program
	{
		static Program()
		{
		}

		public static void Main()
		{
			string sku = "abc123";

			var warehouseProductRepository =
				new WarehouseProductRepository();

			{
				var warehouseProduct =
					warehouseProductRepository.Get(sku: sku);

				warehouseProduct.ReceiveProduct(quantity: 10);
				warehouseProduct.ReceiveProduct(quantity: 5);
				warehouseProduct.ShipProduct(quantity: 9);
				warehouseProduct.AdjustInventory(quantity: 50, reason: "WOW! We Found Some!");

				warehouseProductRepository.Save(warehouseProduct);
			}

			{
				var warehouseProduct =
					warehouseProductRepository.Get(sku: sku);

				System.Console.WriteLine
					($"Current Quantity for { sku } is { warehouseProduct.GetCurrentProductState().Quantity }");

				foreach (var currentEvent in warehouseProduct.GetAllEvents())
				{
					System.Console.WriteLine(currentEvent.ToString());
				}
			}
		}
	}
}
