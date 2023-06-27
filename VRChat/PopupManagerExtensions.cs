using System;
using TMPro;
using UnityEngine;
using VRC.DataModel;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.VRChat
{
    public static class PopupManagerExtensions
    {
        private static VRCInputField _KeyboardComponent;
        private static GameObject keyboardGameObject;
        
        [Obsolete("Alert is deprecated, please use Alert2Box instead. This will be removed in future updates. -Neeko")]
        public static void Alert(string title, string content, Action leftBtnAction = null, Action rightBtnAction = null, string leftBtnText = "Yes", string rightBtnText = "No", bool showInMainMenu = false)
        {
            if (showInMainMenu) 
                MenuEx.MMInstance.Method_Public_Virtual_Final_New_Void_String_String_Action_Action_String_String_String_TextAlignmentOptions_0(title, content, leftBtnAction, rightBtnAction, leftBtnText, rightBtnText);
            else
                MenuEx.QMInstance.Method_Public_Virtual_Final_New_Void_String_String_String_String_Action_Action_String_TextAlignmentOptions_0(title, content, leftBtnText, rightBtnText, leftBtnAction, rightBtnAction);
        }
        
        public static void Alert1Box(string title, string content, Action middleBtnAction = null, string middleBtnText = "Okay", bool showInMainMenu = false)
        {
            if (showInMainMenu)
                MenuEx.MMInstance.Method_Public_Void_String_String_String_String_String_Action_Action_Action_String_TextAlignmentOptions_PDM_0(title, content, middleBtnText, null, null, middleBtnAction);
            else
                MenuEx.QMInstance.Method_Public_Void_String_String_String_String_String_Action_Action_Action_String_TextAlignmentOptions_PDM_0(title, content, middleBtnText, null, null, middleBtnAction);
        }
        
        public static void Alert2Box(string title, string content, Action leftBtnAction = null, Action rightBtnAction = null, string leftBtnText = "Yes", string rightBtnText = "No", bool showInMainMenu = false)
        {
            if (showInMainMenu)
                MenuEx.MMInstance.Method_Public_Void_String_String_String_String_String_Action_Action_Action_String_TextAlignmentOptions_PDM_0(title, content, leftBtnText, rightBtnText, null, leftBtnAction, rightBtnAction);
            else
                MenuEx.QMInstance.Method_Public_Void_String_String_String_String_String_Action_Action_Action_String_TextAlignmentOptions_PDM_0(title, content, leftBtnText, rightBtnText, null, leftBtnAction, rightBtnAction);
        }
        public static void Alert3Box(string title, string content, Action leftBtnAction = null, Action middleBtnAction = null, Action rightBtnAction = null, string leftBtnText = "Yes", string middleBtnText = "Maybe", string rightBtnText = "No", bool showInMainMenu = false)
        {
            if (showInMainMenu)
                MenuEx.MMInstance.Method_Public_Void_String_String_String_String_String_Action_Action_Action_String_TextAlignmentOptions_PDM_0(title, content, leftBtnText, middleBtnText, rightBtnText, leftBtnAction, middleBtnAction, rightBtnAction);
            else
                MenuEx.QMInstance.Method_Public_Void_String_String_String_String_String_Action_Action_Action_String_TextAlignmentOptions_PDM_0(title, content, leftBtnText, middleBtnText, rightBtnText, leftBtnAction, middleBtnAction, rightBtnAction);
        }
        
        public static void MainMenuBigAlert(string title, string content)
        {
            MenuEx.MMInstance.Method_Public_Void_String_String_PDM_0(title, content);
        }

        public static void InfoBox(string content, bool showInMainMenu = false)
        {
            if (showInMainMenu)
                MenuEx.MMInstance.Method_Public_Virtual_Final_New_Void_String_1(content);
            else
                MenuEx.QMInstance.Method_Public_Virtual_Final_New_Void_String_1(content);
        }
        
        public static void Toast(string content, Sprite icon = null, float duration = 5f)
        {
            VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.Method_Public_Void_String_Sprite_Single_0(content, icon, duration);
        }
        
        public static void Toast(string content, string description = null, Sprite icon = null, float duration = 5f)
        {
            VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.notification.Method_Public_Void_Sprite_String_String_Single_Object1PublicTYBoTYUnique_1_Boolean_0(icon, content, description, duration);
        }

        public enum KeyBoardType
        {
            Standard = EnumPublicSealedvaStNuSe4vUnique.Standard,
            Numeric = EnumPublicSealedvaStNuSe4vUnique.Numeric,
            Search = EnumPublicSealedvaStNuSe4vUnique.Search
        }

        public static void InputPopup
        (
            string Title, 
            Action<string> EndString, 
            Action<string> RealTimeString = null, 
            Action OnClose = null, 
            KeyBoardType keyBoardType = KeyBoardType.Standard, 
            string Placeholder = "Enter Text", 
            string OkButton = "OK", 
            string CancelButton = "Cancel", 
            bool MultiLine = true, 
            int CharLimit = 0,
            bool KeepOpen = false,
            bool ReadOnly = false
        )
        {
            if (_KeyboardComponent == null)
            {
                keyboardGameObject = new GameObject("ReMod.Core_KeyBoard");
                UnityEngine.Object.DontDestroyOnLoad(keyboardGameObject);
                _KeyboardComponent = keyboardGameObject.AddComponent<VRCInputField>();
            }

            // No idea what these do as I don't see any effect changing it
            const bool unKnownBool = true;
            const string stringThatDoesNothing = "";
            
            try
            {
                KeyboardData One = new();
                KeyboardData Two = One.Method_Public_KeyboardData_String_String_String_String_String_0(Title, Placeholder, stringThatDoesNothing, OkButton, CancelButton);
                KeyboardData Three = Two.Method_Public_KeyboardData_Action_1_String_Action_1_String_Action_Boolean_0(RealTimeString, EndString, OnClose, KeepOpen);
                KeyboardData Four = Three.Method_Public_KeyboardData_EnumPublicSealedvaStNuSe4vUnique_Boolean_0((EnumPublicSealedvaStNuSe4vUnique) keyBoardType, unKnownBool);
                KeyboardData Five = Four.Method_Public_KeyboardData_InputType_ContentType_Int32_Boolean_Boolean_InterfacePublicAbstractBoStVoAc1VoAcSt1BoUnique_0(TMP_InputField.InputType.Standard, TMP_InputField.ContentType.Standard, CharLimit, MultiLine, ReadOnly, null);
                Five._isWorldKeyboard = true;
                _KeyboardComponent._keyboardData = Five;
                _KeyboardComponent.Method_Private_Void_0();
            }
            catch {}
        }
    }
}
