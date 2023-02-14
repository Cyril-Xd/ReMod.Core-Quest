using System.Collections.Generic;
using UnhollowerRuntimeLib;

namespace ReMod.Core.UI.ActionMenu
{
    public static class ActionMenuAPI
    {
        internal static readonly List<ActionMenuButton> mainMenuButtons = new();
        internal static global::ActionMenu activeActionMenu { get; private set; }
        internal static bool IsOpen(this ActionMenuOpener actionMenuOpener) => actionMenuOpener.field_Private_Boolean_0;
        internal static bool IsWeelOpen => ActionMenuOpener1.field_Private_Boolean_0 || ActionMenuOpener2.field_Private_Boolean_0;
        private static readonly ActionMenuOpener ActionMenuOpener1 = ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_0;
        private static readonly ActionMenuOpener ActionMenuOpener2 = ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_1;
        
        internal static ActionMenuOpener GetActionMenuOpener() 
        {
            if (!ActionMenuOpener1.IsOpen() && ActionMenuOpener2.IsOpen()) return ActionMenuOpener1;
            if (ActionMenuOpener1.IsOpen() && !ActionMenuOpener2.IsOpen()) return ActionMenuOpener1;
            return null;
        }

        internal static void OpenMainPage(global::ActionMenu menu) 
        {
            activeActionMenu = menu;

            foreach (ActionMenuButton button in mainMenuButtons) 
            {
                PedalOption pedalOption = activeActionMenu.Method_Public_PedalOption_0();
                pedalOption.prop_String_0 = button.buttonText;
                pedalOption.field_Public_Func_1_Boolean_0 = DelegateSupport.ConvertDelegate<Il2CppSystem.Func<bool>>(button.buttonAction);
                pedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(button.buttonIcon != null ? button.buttonIcon : null); // todo ButtonSprite

                button.currentPedalOption = pedalOption;
            }
        }
    }
}