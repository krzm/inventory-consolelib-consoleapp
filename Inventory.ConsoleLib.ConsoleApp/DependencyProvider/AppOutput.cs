using DataToTable;
using Inventory.Data;
using Inventory.Modern.Lib;
using Unity;

namespace Inventory.ConsoleApp;

public class AppOutput : CLI.Core.Lib.AppOutput
{
    public AppOutput(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterColumnCalculators()
    {
        Container
            .RegisterType<IColumnCalculator<ItemCategory>, ColumnCalculator<ItemCategory>>()
	        .RegisterType<IColumnCalculator<Item>, ColumnCalculator<Item>>()
	        .RegisterType<IColumnCalculator<ItemImage>, ColumnCalculator<ItemImage>>();
    }

    protected override void RegisterTableProviders()
    {
        Container
            .RegisterType<IDataToText<ItemCategory>, ItemCategoryTable>()
	        .RegisterType<IDataToText<Item>, ItemTable>()
	        .RegisterType<IDataToText<ItemImage>, ItemImageTable>();
    }
}