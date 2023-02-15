using System;
using ReMod.Core.Managers;
using UnityEngine;

namespace ReMod.Core.UI.ActionMenu
{
    public class ActionMenuToggle
    {
        public bool State { get; set; }
        public PedalOption currentPedalOption => actionButton.currentPedalOption;
        private ActionMenuButton actionButton { get; }
        private Sprite _onImage, _offImage;

        internal Sprite onImage 
        {
            get
            {
                if (_onImage == null) _onImage = defaultOnImage;
                return _onImage; 
            }
        }

        internal Sprite offImage 
        {
            get
            {
                if (_offImage == null) _offImage = defaultOffImage;
                return _offImage; 
            } 
        }
        
        private static readonly Sprite defaultOnImage = ResourceManager.fetchSpriteFromBundledResource("ReMod.Core.Resources.switchOnIcon.png", 300, 250);
        private static readonly Sprite defaultOffImage = ResourceManager.fetchSpriteFromBundledResource("ReMod.Core.Resources.switchOffIcon.png", 300, 250);
        
        public ActionMenuToggle(ActionMenuPage basePage, string text, Action<bool> action, bool state = false, Sprite onicon = null, Sprite officon = null) 
        {
            if (onicon != null) _onImage = onicon;
            if (officon != null) _offImage = officon;
            
            State = state;
            actionButton = new ActionMenuButton(basePage, text, () => 
            {
                State = !State;
                action.Invoke(State);
                actionButton?.SetIcon(State ? onImage : offImage);
            }, state ? onicon : officon);
        }

        public void SetState(bool newState) 
        {
            State = newState;
            actionButton.SetIcon(newState ? onImage : offImage);
        }
    }
}