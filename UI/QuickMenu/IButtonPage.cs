using System;
using UnityEngine;

namespace ReMod.Core.UI.QuickMenu
{
    public interface IButtonPage
    {
        ReMenuButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null, string color = "#ffffff");
        ReMenuButton AddSpacer(Sprite sprite = null);
        ReMenuPage AddMenuPage(string text, string tooltip = "", Sprite sprite = null, string color = "#ffffff");
        ReCategoryPage AddCategoryPage(string text, string tooltip = "", Sprite sprite = null, string color = "#ffffff");
        ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false, string color = "#ffffff");
        ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, string color = "#ffffff");
        ReMenuLabel AddLabel(string text, string Subtitle, int FontSize = 46, string color = "#ffffff");
        ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue, Sprite iconOn, Sprite iconOff, string color = "#ffffff");
        ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, Sprite iconOn, Sprite iconOff, string color = "#ffffff");
        ReMenuPage GetMenuPage(string name);
        ReCategoryPage GetCategoryPage(string name);
        ReMenuPage ToMenuPage(string name, string tooltip = "", Sprite sprite = null);
        void AddCategoryPage(string text, string tooltip, Action<ReCategoryPage> onPageBuilt, Sprite sprite = null, string color = "#ffffff");
        void AddMenuPage(string text, string tooltip, Action<ReMenuPage> onPageBuilt, Sprite sprite = null, string color = "#ffffff");
    }
}
