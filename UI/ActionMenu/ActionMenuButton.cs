using System;
using UnityEngine;
using static ReMod.Core.UI.ActionMenu.ActionMenuAPI;

namespace ReMod.Core.UI.ActionMenu
{
    public class ActionMenuButton
    {
        public PedalOption currentPedalOption;
        public string buttonText;
        public Texture2D buttonIcon;
        public Func<bool> buttonAction;

        public ActionMenuButton(ActionMenuBaseMenu baseMenu, string text, Action action, Sprite icon = null)
        {
            buttonText = text;
            if (icon != null)
                buttonIcon = icon.texture;
            buttonAction = delegate
            {
                action();
                return true;
            };

            if (baseMenu == ActionMenuBaseMenu.MainMenu)
            {
                mainMenuButtons.Add(this);
            }
        }

        public ActionMenuButton(ActionMenuPage basePage, string text, Action action, Sprite icon = null)
        {
            buttonText = text;
            if (icon != null)
                buttonIcon = icon.texture;
            buttonAction = delegate
            {
                action();
                return true;
            };

            basePage.buttons.Add(this);
        }

        public void SetButtonText(string newText)
        {
            buttonText = newText;

            if (currentPedalOption != null)
            {
                currentPedalOption.prop_String_0 = newText;
            }
        }

        public void SetIcon(Texture2D newTexture)
        {
            buttonIcon = newTexture;

            if (currentPedalOption != null)
            {
                currentPedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(newTexture);
            }
        }
    }
}