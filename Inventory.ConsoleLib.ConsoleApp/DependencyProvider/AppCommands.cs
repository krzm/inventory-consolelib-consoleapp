using System.Collections.Generic;
using DataToTable;
using Inventory.Data;
using Inventory.Console.Lib;
using Unity;
using CLIFramework;
using CLIHelper;
using CLIReader;

namespace Inventory.ConsoleApp;

public class AppCommands 
    : CLIFramework.AppCommands
{
    public AppCommands(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();
		RegisterItemCommands();
        RegisterItemCategoryCommands();
    }

    private void RegisterItemCommands()
    {
        RegisterCommand<HelpCommand<Item>, Item>(
            "Help Item".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(Item.ItemCategoryId)
                , nameof(Item.Name)
            });
            
        RegisterCommand<ItemReadCommand, Item>(
            "Item".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<IOutput>()
            , Container.Resolve<IDataToText<Item>>());

            RegisterCommand<ItemInsertCommand, Item>(
            "Insert Item".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<List<IReader<string>>>());

        RegisterCommand<ItemUpdateCommand, Item>(
            "Update Item".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<List<IReader<string>>>());
    }

    private void RegisterItemCategoryCommands()
    {
        RegisterCommand<HelpCommand<ItemCategory>, ItemCategory>(
            "Help ItemCategory".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(ItemCategory.Name)
                , nameof(ItemCategory.Description)
                , nameof(ItemCategory.ParentId)
            });

        RegisterCommand<ItemCategoryReadCommand, ItemCategory>(
            "ItemCategory".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<IOutput>()
            , Container.Resolve<IDataToText<ItemCategory>>());

        RegisterCommand<ItemCategoryInsertCommand, ItemCategory>(
            "Insert ItemCategory".ToLowerInvariant()
            , Container.Resolve<IInventoryUnitOfWork>()
            , Container.Resolve<List<IReader<string>>>());
    }
}