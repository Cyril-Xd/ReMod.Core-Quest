using System.Collections.Generic;
using UnhollowerRuntimeLib;

namespace ReMod.Core.UI.ActionMenu
{
    public static class ActionMenuAPI
    {
        internal static readonly List<ActionMenuButton> mainMenuButtons = new();
        public static global::ActionMenu activeActionMenu;
        public enum ActionMenuBaseMenu
        {
            MainMenu = 1
        }

        public static bool IsOpen(this ActionMenuOpener actionMenuOpener)
            => actionMenuOpener.field_Private_Boolean_0;

        internal static ActionMenuOpener GetActionMenuOpener()
        {
            if (!ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_0.IsOpen() &&
                ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_1.IsOpen())
            {
                return ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_1;
            }

            if (ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_0.IsOpen() &&
                !ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_1.IsOpen())
            {
                return ActionMenuController.prop_ActionMenuController_0.field_Public_ActionMenuOpener_0;
            }

            return null;
        }
        
        public static void OpenMainPage(global::ActionMenu menu)
        {
            activeActionMenu = menu;
            foreach (ActionMenuButton button in mainMenuButtons)
            {
                PedalOption pedalOption = activeActionMenu.Method_Public_PedalOption_0();

                pedalOption.prop_String_0 = button.buttonText;
                pedalOption.field_Public_Func_1_Boolean_0 = DelegateSupport.ConvertDelegate<Il2CppSystem.Func<bool>>(button.buttonAction);

                if (button.buttonIcon != null)
                {
                    pedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(button.buttonIcon);
                }

                button.currentPedalOption = pedalOption;
            }
        }
    }
}