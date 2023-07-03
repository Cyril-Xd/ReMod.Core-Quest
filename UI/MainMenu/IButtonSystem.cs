using ReMod.Core.UI.QuickMenu;
using System;
using UnityEngine;

namespace ReMod.Core.UI.MainMenu
{
    public interface IButtonSystem
    {
        ReMMPage GetMenuPage(string name);
    }
}