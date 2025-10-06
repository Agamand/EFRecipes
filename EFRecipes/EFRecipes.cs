using ACulinaryArtillery;
using ACulinaryArtillery.Util;
using HarmonyLib;
using System;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace EFRecipes
{
    public class EFRecipes : ModSystem
      {
        Harmony _harmony;
        Mod Me;
        string OriginalName;
        public override void Start(ICoreAPI api)
		 {

            Me = api.ModLoader.Mods.FirstOrDefault((m) => m.Info.ModID == "expandedfoodspatch");
            OriginalName = Me.Info.ModID;
            api.Logger.Notification("Temporary change modid of expandedfoodspatch to expandedfoods");
            Me.Info.ModID = "expandedfoods"; // identify as expandedfoods

            _harmony = new Harmony("ExpandedFoods.Patches");
            _harmony.PatchAll(typeof(EFRecipes).Assembly);

			CookingRecipe.NamingRegistry["compote"] = new acaRecipeNames();
            CookingRecipe.NamingRegistry["augratin"] = new acaRecipeNames();
            CookingRecipe.NamingRegistry["riceandbeans"] = new acaRecipeNames();
            CookingRecipe.NamingRegistry["meatysalad"] = new acaRecipeNames();
            CookingRecipe.NamingRegistry["yogurtmeal"] = new acaRecipeNames();
            CookingRecipe.NamingRegistry["pastahot"] = new acaRecipeNames();
            CookingRecipe.NamingRegistry["pastacold"] = new acaRecipeNames();
		} 

        public override void Dispose() {
            base.Dispose();
            _harmony.UnpatchAll();
        }


        public override void AssetsFinalize(ICoreAPI api)
        {
            api.Logger.Notification("Restore modid");
            Me.Info.ModID = OriginalName;
        }
    } 
}
