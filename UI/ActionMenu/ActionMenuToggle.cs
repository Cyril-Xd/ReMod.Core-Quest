using System;
using UnityEngine;

namespace ReMod.Core.UI.ActionMenu
{
    public class ActionMenuToggle
    {
        private static Sprite ondata, offdata;

        public static Sprite onImageData 
        {
            get
            { 
                if (ondata == null) ondata = null; // todo ToggleOnSprite
                return ondata; 
            }
        }

        public static Sprite offImageData 
        {
            get 
            { 
                if (offdata == null) offdata = null; // todo ToggleOffSprite
                return offdata; 
            } 
        }

        private Sprite OnImage, OffImage;
        internal ActionMenuButton actionButton { get; }
        internal bool State { get; set; }

        internal ActionMenuToggle(ActionMenuPage basePage, string text, Action<bool> action, bool state = false, Sprite onicon = null, Sprite officon = null) 
        {
            if (onicon == null) onicon = onImageData;
            if (officon == null) officon = offImageData;
            var img = state ? onicon : officon;
            OnImage = onicon;
            OffImage = officon;

            State = state;
            actionButton = new ActionMenuButton(basePage, text, () => 
            {
                State = !State;
                action.Invoke(State);
                actionButton?.SetIcon(State ? OnImage : OffImage);
            }, img); 
        }

        internal void SetState(bool newState) 
        {
            State = newState;
            actionButton.SetIcon(newState ? OnImage : OffImage);
        }
    }
}