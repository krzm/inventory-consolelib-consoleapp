using System.Collections.Generic;
using CLIFramework;
using CLIHelper;
using CLIReader;
using DataToTable;
using Inventory.Console.Lib;
using Inventory.Data;
using Unity;

namespace Inventory.ConsoleApp;

public class AppCommands2 
    : AppCommands
{
    public AppCommands2(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();
		RegisterImagePathCommands();
    }

    private void RegisterImagePathCommands()
    {
        RegisterCommand<HelpCommand<ItemImage>, ItemImage>(
            "Help ImagePath".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(ItemImage.ItemId)
                , nameof(ItemImage.Path)
            });
            
        RegisterCommand<ItemImageReadCommand, ItemImage>(
            "ImagePath".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<IOutput>()
            , Container.Resolve<IDataToText<ItemImage>>());

        RegisterCommand<ItemImageInsertCommand, ItemImage>(
            "Insert ImagePath".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<List<IReader<string>>>());
            
        RegisterCommand<ItemImageUpdateCommand, ItemImage>(
            "Update ImagePath".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<List<IReader<string>>>());
    }
}