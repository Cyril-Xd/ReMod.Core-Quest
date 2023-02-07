using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;
using static ReMod.Core.UI.ActionMenu.ActionMenuAPI;

namespace ReMod.Core.UI.ActionMenu
{
    public class ActionMenuPage
    {
        public List<ActionMenuButton> buttons = new();
        public ActionMenuPage previousPage;
        public ActionMenuButton menuEntryButton;

        public ActionMenuPage(ActionMenuBaseMenu baseMenu, string buttonText, Sprite buttonIcon = null)
        {
            if (baseMenu == ActionMenuBaseMenu.MainMenu)
            {
                menuEntryButton = new ActionMenuButton(ActionMenuBaseMenu.MainMenu, buttonText, OpenMenu, buttonIcon);
            }
        }

        public ActionMenuPage(ActionMenuPage basePage, string buttonText, Sprite buttonIcon = null)
        {
            ActionMenuPage page = this;
            
            previousPage = basePage;
            menuEntryButton = new ActionMenuButton(previousPage, buttonText, page.OpenMenu, buttonIcon);
        }

        public void OpenMenu()
        {
            GetActionMenuOpener()?.field_Public_ActionMenu_0.Method_Public_ObjectNPublicAcTeAcStGaUnique_Action_Action_Texture2D_String_0(new Action(() =>
            {
                foreach (ActionMenuButton button in buttons)
                {
                    PedalOption pedalOption = GetActionMenuOpener().field_Public_ActionMenu_0.Method_Public_PedalOption_0();

                    pedalOption.prop_String_0 = button.buttonText;
                    pedalOption.field_Public_Func_1_Boolean_0 = DelegateSupport.ConvertDelegate<Il2CppSystem.Func<bool>>(button.buttonAction);

                    if (button.buttonIcon != null)
                    {
                        pedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(button.buttonIcon);
                    }

                    button.currentPedalOption = pedalOption;
                }
            }));
        }
    }
}