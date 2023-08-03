using System;
using TMPro;
using UnityEngine;
using VRC.DataModel;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.VRChat
{
    public static class PopupManagerExtensions
    {
        public static void Alert(string title, string content, Action leftBtnAction = null, Action rightBtnAction = null, string leftBtnText = "Yes", string rightBtnText = "No", bool showInMainMenu = false)
        {
            if (showInMainMenu) 
                MenuEx.MMInstance.Method_Public_Virtual_Final_New_Void_String_String_Action_Action_String_String_String_TextAlignmentOptions_0(title, content, leftBtnAction, rightBtnAction, leftBtnText, rightBtnText);
            else
                MenuEx.QMInstance.Method_Public_Virtual_Final_New_Void_String_String_String_String_Action_Action_String_TextAlignmentOptions_0(title, content, leftBtnText, rightBtnText, leftBtnAction, rightBtnAction);
        }

        public static void MainMenuBigAlert(string title, string content)
        {
            MenuEx.MMInstance.Method_Public_Void_String_String_PDM_0(title, content);
        }

        public static void Toast(string content, Sprite icon = null, float duration = 5f)
        {
            VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.Method_Public_Void_String_Sprite_Single_0(content, icon, duration);
        }
        
        public static void Toast(string content, string description = null, Sprite icon = null, float duration = 5f)
        {
            VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.notification.Method_Public_Void_Sprite_String_String_Single_Object1PublicTYBoTYUnique_1_Boolean_0(icon, content, description, duration);
        }
    }
}
