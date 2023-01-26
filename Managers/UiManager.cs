using ReMod.Core.UI.QuickMenu;
using ReMod.Core.VRChat;
using UnityEngine;

namespace ReMod.Core.Managers
{
    public class UiManager
    {
        public IButtonPage MainMenu { get; }
        public IButtonPage TargetMenu { get; }
        public IButtonPage LaunchPad { get; }

        public UiManager(string menuName, Sprite menuSprite, bool createTargetMenu = true, bool createMainMenu = true, bool crxcmodule = false)
        {
            if (!crxcmodule)
            {
                MainMenu = new ReMenuPage(menuName, true);
                ReTabButton.Create(menuName, $"Open the {menuName} menu.", menuName, menuSprite);
            }
            if (crxcmodule)
            {
                var localMenu = new ReMenuPage(QuickMenuEx.CRXCMenu.transform);
                MainMenu = localMenu.AddMenuPage($"{menuName}");
            }

            if (createTargetMenu)
            {
                var localMenu = new ReCategoryPage(QuickMenuEx.SelectedUserLocal.transform);
                TargetMenu = localMenu.AddCategory($"{menuName}");
            }
            if (createMainMenu )
            {
                var localMenu = new ReCategoryPage(QuickMenuEx.DashboardMenu.transform);
                LaunchPad = localMenu.AddCategory($"{menuName}");
            }
        }
    }
}
