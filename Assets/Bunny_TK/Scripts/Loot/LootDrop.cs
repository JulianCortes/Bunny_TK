using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Loot
{
    public class LootDrop : MonoBehaviour
    {
        public bool isLootsSetAtStart = false;
        public bool isAmountRandom = false;
        public int lootAmount = 1;

        [SerializeField]
        private List<Loot> loots;

        [HideInInspector]
        [SerializeField]
        private LootTableScriptableObject lootTableItem;

        [HideInInspector]
        [SerializeField]
        private LootTableScriptableObject lootTableAmount;

        public void Start()
        {
            if (isLootsSetAtStart)
                SetRandomLoot();
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
                SetRandomLoot();
        }
        public void SetLootTableItem(LootTableScriptableObject newLootTable)
        {
            SetLootTableItem(newLootTable, false);
        }
        public void SetLootTableAmount(LootTableScriptableObject newLootTable, bool updateCurrentLoot)
        {
            lootTableAmount = newLootTable;
            if (updateCurrentLoot)
                SetRandomLoot();
        }
        public void SetLootTableAmount(LootTableScriptableObject newLootTable)
        {
            SetLootTableAmount(newLootTable, false);
        }

        public void SetRandomLoot()
        {
            loots = DropLoot();
        }
    }
}
