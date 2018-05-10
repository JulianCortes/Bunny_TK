using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Loot
{
    public class LootDrop : MonoBehaviour
    {
        /// <summary>
        /// Set loots at start
        /// </summary>
        public bool isLootsSetAtStart = false;
        /// <summary>
        /// if true, the amount is drawn from lootTableAmount
        /// </summary>
        public bool isAmountRandom = false;
        /// <summary>
        /// How many loot, if isAmountRandom = true it is initialized on DropLoot
        /// </summary>
        public int lootAmount = 1;

        [SerializeField]
        private List<Loot> loots;

        [SerializeField]
        private LootTableScriptableObject lootTableItem;

        [SerializeField]
        private LootTableScriptableObject lootTableAmount;

        public void Start()
        {
            if (isLootsSetAtStart)
                InitLoots();
        }

        public List<Loot> DropLoot()
        {
            List<Loot> loots = new List<Loot>();

            if (isAmountRandom)
                lootAmount = lootTableAmount.GetRandomWeighted().GetInt();

            for (int i = 0; i < lootAmount; i++)
                loots.Add(lootTableItem.GetRandomWeighted());

            this.loots = new List<Loot>(loots);
            return loots;
        }

        public void SetLootTableItem(LootTableScriptableObject newLootTable, bool updateCurrentLoot)
        {
            lootTableItem = newLootTable;
            if (updateCurrentLoot)
                InitLoots();
        }

        public void SetLootTableItem(LootTableScriptableObject newLootTable)
        {
            SetLootTableItem(newLootTable, false);
        }

        public void SetLootTableAmount(LootTableScriptableObject newLootTable, bool updateCurrentLoot)
        {
            lootTableAmount = newLootTable;
            if (updateCurrentLoot)
                InitLoots();
        }

        public void SetLootTableAmount(LootTableScriptableObject newLootTable)
        {
            SetLootTableAmount(newLootTable, false);
        }

        public void InitLoots()
        {
            loots = DropLoot();
        }
    }
}