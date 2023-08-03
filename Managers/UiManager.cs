using ReMod.Core.UI.QuickMenu;
using ReMod.Core.VRChat;
using UnityEngine;

namespace ReMod.Core.Managers
{
    public class UiManager
    {
        public IButtonPage QMenu { get; }
        public IButtonPage TargetMenu { get; }
        public IButtonPage LaunchPad { get; }

        public UiManager(string menuName, Sprite menuSprite, bool createQMTargets = true, bool createLaunchPadMenu = true, string color = "#ffffff")
        {
            // Quick Menu
            QMenu = new ReMenuPage(menuName, true, color);
            ReTabButton.Create(menuName, $"Open the {menuName} menu.", menuName, menuSprite);

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
        }
    }
}
