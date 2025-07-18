﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.Game
{
    public class Item
    {
        public string name;
        public Items id;
        public string description;
        public int maxStackSize;
        public Action<Player> effect;
        public IndefiniteArticle indefiniteArticle;
        public static readonly Item exitInventoryItem = new Item(Items.ExitInventory, "ExitInventory", "Exits Inventory", IndefiniteArticle.an, (p) => { });
        public static readonly Item useItemItem = new Item(Items.UseItem, "UseItem", "Uses Item", IndefiniteArticle.a, (p) => { });

        public Item(Items id, string name, int maxStackSize, string description, IndefiniteArticle indefiniteArticle, Action<Player> effect) 
        {
            this.name = name;
            this.description = description; 
            this.effect = effect;
            this.maxStackSize = maxStackSize;
            this.id = id;
        }

        public Item(Items id, string name, string description, IndefiniteArticle indefiniteArticle, Action<Player> effect)
        {
            this.name = name;
            this.description = description;
            this.effect = effect;
            this.maxStackSize = int.MaxValue;
            this.id = id;
        }

        public static Item RandomItem()
        {
            double rarityRand = RNG.NextDouble();

            if(rarityRand < GameConstants.legendaryRarity)
            {
                return GameConstants.itemEnum2Item[GameConstants.legendaryItems[RNG.Next(GameConstants.legendaryItems.Length)]];
            }
            else if (rarityRand < GameConstants.rareRarity + GameConstants.legendaryRarity)
            {
                return GameConstants.itemEnum2Item[GameConstants.rareItems[RNG.Next(GameConstants.rareItems.Length)]];
            }
            else if (rarityRand < GameConstants.uncommonRarity + GameConstants.rareRarity + GameConstants.legendaryRarity)
            {
                return GameConstants.itemEnum2Item[GameConstants.uncommonItems[RNG.Next(GameConstants.uncommonItems.Length)]];
            }
            else
            {
                return GameConstants.itemEnum2Item[GameConstants.commonItems[RNG.Next(GameConstants.commonItems.Length)]];
            }
        }

        public static ItemGiveResult CanGiveItem(Player player, Item itemToGive)
        {
            if (player.inventory.Count >= player.inventorySize) return ItemGiveResult.InventoryFull;
            else if (!player.inventoryCounts.ContainsKey(itemToGive.id)) return ItemGiveResult.Success;
            else if (player.inventoryCounts[itemToGive.id] > itemToGive.maxStackSize) return ItemGiveResult.ItemStackFull;
            else return ItemGiveResult.Success;
        }

        public string WithIndefiniteArticle()
        {
            return indefiniteArticle.ToString() + " " + name;
        }
    }
}
