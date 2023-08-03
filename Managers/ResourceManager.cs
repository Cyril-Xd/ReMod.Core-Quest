﻿using System.IO;
using System.Reflection;
using UnityEngine;

namespace ReMod.Core.Managers
{
    public static class ResourceManager
    {       
        public static Sprite LoadSpriteFromDisk(this string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            byte[] array = File.ReadAllBytes(path);
            if (array == null || array.Length == 0)
            {
                return null;
            }
            Texture2D texture2D = new Texture2D(512, 512);
            if (!ImageConversion.LoadImage(texture2D, array)) return null;
            Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0, 0), 100000f, 1000U, SpriteMeshType.FullRect, Vector4.zero, false);
            sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            return sprite;
        }
    }
}
