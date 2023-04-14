using System;
using System.Linq;
using System.Reflection;
using Harmony;
using HarmonyMethod = HarmonyLib.HarmonyMethod;

namespace ReMod.Core.VRChat
{
    public class ReModPatches
    {
        internal static void OnActionMenu(ActionMenu __instance) => UI.ActionMenu.ActionMenuAPI.OpenMainPage(__instance);
        
        internal static void Patch()
        {
            try
            {
                var instance = HarmonyInstance.Create(Assembly.GetExecutingAssembly().FullName);
                
                // Action Menu Patch
                var mainPage = typeof(ActionMenu).GetMethods().First(
                    m => m.ReturnType == typeof(void)
                         && m.GetParameters().Length == 0
                         && m.Name.Contains("PDM")
                         && m.XRefScanForMethod(reflectedType: nameof(PedalOption))
                         && m.XRefScanForMethod(reflectedType: nameof(ActionButton))
                         && m.Name.Contains("6")
                         && m.XRefCount() < 22
                );
            
                instance.Patch(mainPage, postfix: new HarmonyMethod(typeof(ReModPatches).GetMethod(nameof(OnActionMenu), BindingFlags.NonPublic | BindingFlags.Static)));
            }
            catch (Exception e)
            {
                MelonLoader.MelonLogger.Msg("Failed Patching. " + e.Message + " " + e.StackTrace + " " + e.Source);
            }
        }
    }
}

