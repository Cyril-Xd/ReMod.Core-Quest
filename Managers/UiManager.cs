using ReMod.Core.UI.MainMenu;
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
        public IButtonSystem BigMenu { get; }

        public UiManager(string menuName, Sprite menuSprite, bool createQMTargets = true, bool createLaunchPadMenu = true, bool createMainMenu = false, bool crxcmodule = false, string color = "#ffffff")
        {
            // Quick Menu
            if (!crxcmodule)
            {
                MainMenu = new ReMenuPage(menuName, true, color);
                ReTabButton.Create(menuName, $"Open the {menuName} menu.", menuName, menuSprite);
            }
            else
            {
                var localMenu = new ReMenuPage(MenuEx.CRXCMenu.transform);
                MainMenu = localMenu.AddMenuPage($"{menuName}", color:color);
            }

            // Target Menu
            if (createQMTargets)
            {
                var localMenu = new ReCategoryPage(MenuEx.SelectedUserLocal.transform);
                TargetMenu = localMenu.AddCategory($"{menuName}", color);
            }
            
            // LaunchPad Menu
            if (createLaunchPadMenu)
            {
                var localMenu = new ReCategoryPage(MenuEx.DashboardMenu.transform);
                LaunchPad = localMenu.AddCategory($"{menuName}", color);
            }
            
            // Main Menu
            if (createMainMenu)
            {
                BigMenu = new ReMMenuPage(menuName, menuSprite, true, color);
                ReMMTab.Create(menuName, $"Open the {menuName} menu.", menuName, menuSprite);
            }
        }
    }
}
