using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System;
using FrooxEngine.LogiX;

namespace NoLogixTraversalContext
{
    public class NoLogixTraversalContext : NeosMod
    {
        public override string Name => "NoLogixTraversalContext";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/NoLogixTraversalContext";

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> HideTraversal = new ModConfigurationKey<bool>("hideTraversal", "Hide Traversal context menu item", () => true);

        static ModConfiguration config;
        
        public override void OnEngineInit()
        {
            config = GetConfiguration();

            Harmony harmony = new Harmony("me.badhaloninja.NoLogixTraversalContext");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(LogixTip), "GenerateMenuItems")]
        private class LogixTip_GenerateMenuItems_Patch
        {
            public static void Postfix(LogixTip __instance, CommonTool tool, ContextMenu menu)
            {
                if (!config.GetValue(HideTraversal)) return;
                var shift = menu.Slot.GetComponentInChildren<ButtonEnumShift<LogixTraversal>>();
                shift?.Slot?.Destroy();
            }
        }
    }
}