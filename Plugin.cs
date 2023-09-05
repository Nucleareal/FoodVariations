using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace StellaFoodsNS
{
    public class StellaFoods : Mod
    {
        private bool _isFarmingLoaded;
        private bool _isMachineLoaded;
        private bool _isFoodsLoaded;
        private bool _isMagicLoaded;

        private const string FARMING_MOD_CLASSNAME = "StellaFarming";
        private const string MACHINE_MOD_CLASSNAME = "StellaMachine";
        private const string FOODS_MOD_CLASSNAME = "StellaFoods";
        private const string MAGIC_MOD_CLASSNAME = "StellaMagic";

        private static SetCardBagType StellaFoodsBooster;
        private SetCardBagData _StellaFoodsBoosterData;

        private void Awake()
        {
            Logger.Log("Awaking StellaFoods...");

            StellaFoodsBooster = EnumHelper.ExtendEnum<SetCardBagType>("StellaFoods");

            _StellaFoodsBoosterData = ScriptableObject.CreateInstance<SetCardBagData>();
            _StellaFoodsBoosterData.Chances = new List<SimpleCardChance>();
            _StellaFoodsBoosterData.SetCardBagType = StellaFoodsBooster;
        }

        public override void Ready()
        {
            DetectBridges();

            RegisterRecipes();

            Logger.Log("StellaFoods Ready!");
        }

        private void DetectBridges()
        {
            foreach (var m in ModManager.LoadedMods)
            {
                var mod_classname = m.GetType().Name;

                _isFarmingLoaded |= mod_classname == FARMING_MOD_CLASSNAME;
                _isMachineLoaded |= mod_classname == MACHINE_MOD_CLASSNAME;
                _isFoodsLoaded |= mod_classname == FOODS_MOD_CLASSNAME;
                _isMagicLoaded |= mod_classname == MAGIC_MOD_CLASSNAME;
            }
        }

        private void RegisterRecipes()
        {
            WorldManager.instance.GameDataLoader.SetCardBags.Add(_StellaFoodsBoosterData);

            var mainboard = WorldManager.instance.Boards.Where(e => e.Location == Location.Mainland).Single();
            mainboard.BoosterIds.Add("stella_foods_booster");
        }
    }
}