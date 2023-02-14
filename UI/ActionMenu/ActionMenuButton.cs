using System;
using UnityEngine;
using static ReMod.Core.UI.ActionMenu.ActionMenuAPI;

namespace ReMod.Core.UI.ActionMenu
{
    public class ActionMenuButton
    {
        internal Func<bool> buttonAction { get; }
        internal PedalOption currentPedalOption { get; set; }
        internal string buttonText { get; private set; }
        internal Texture2D buttonIcon { get; private set; }

        internal ActionMenuButton(string text, Action action, Sprite icon = null)
        {
            if (icon == null) icon = null; // todo ButtonSprite
            buttonText = text;
            buttonIcon = icon.texture;
            buttonAction = () => {
                action();
                return true;
            };
            mainMenuButtons.Add(this);
        }

        internal ActionMenuButton(ActionMenuPage basePage, string text, Action action, Sprite icon = null)
        {
            if (icon == null) icon = null; // todo ButtonSprite
            buttonText = text;
            buttonIcon = icon.texture;
            buttonAction = () => {
                action();
                return true;
            };
            basePage.buttons.Add(this);
        }

        internal void SetButtonText(string newText)
        {
            buttonText = newText;
            if (currentPedalOption != null) currentPedalOption.prop_String_0 = newText;
        }

        internal void SetIcon(Sprite newTexture)
        {
            buttonIcon = newTexture.texture;
            if (currentPedalOption != null) currentPedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(newTexture.texture);
        }
    }
}