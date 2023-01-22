using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace ReMod.Core.VRChat
{
    public static class PlayerExtensions
    {
        #region Others
        public static Player GetPlayer(this string UserID)
        {
            foreach (Player player in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList<Player>())
            {
                if (player.field_Private_APIUser_0.id == UserID)
                {
                    return player;
                }
            }
            return null;
        }
        public static SelectedUserMenuQM GetTarget()
        {
            return UnityEngine.Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().FirstOrDefault().field_Private_UIPage_1.GetComponent<SelectedUserMenuQM>();
        }
        #endregion

        #region IUser
        public static Player GetPlayer(this InterfacePublicAbstractStCoStBoObSt1BoSi1Unique value)
        {
            return value.prop_String_0.GetPlayer();
        }
        public static VRCPlayer GetVRCPlayer(this InterfacePublicAbstractStCoStBoObSt1BoSi1Unique value)
        {
            return value.GetPlayer()._vrcplayer;
        }
        public static APIUser GetAPIUser(this InterfacePublicAbstractStCoStBoObSt1BoSi1Unique value)
        {
            return value.GetPlayer().prop_APIUser_0;
        }
        public static ApiAvatar GetApiAvatar(this InterfacePublicAbstractStCoStBoObSt1BoSi1Unique value)
        {
            return value.GetPlayer().prop_ApiAvatar_0;
        }
        #endregion

        #region SelectedUserMenuQM
        public static InterfacePublicAbstractStCoStBoObSt1BoSi1Unique SelectedIUser()
        {
            return GetTarget().field_Private_InterfacePublicAbstractStCoStBoObSt1BoSi1Unique_0;
        }
        public static VRCPlayer GetVRCPlayer()
        {
            return GetTarget().field_Private_InterfacePublicAbstractStCoStBoObSt1BoSi1Unique_0.GetPlayer()._vrcplayer;
        }
        public static APIUser GetAPIUser()
        {
            return GetTarget().field_Private_InterfacePublicAbstractStCoStBoObSt1BoSi1Unique_0.GetPlayer().field_Private_APIUser_0;
        }
        public static ApiAvatar GetApiAvatar()
        {
            return GetTarget().field_Private_InterfacePublicAbstractStCoStBoObSt1BoSi1Unique_0.GetPlayer().prop_ApiAvatar_0;
        }
        #endregion
        public static Player[] GetPlayers(this PlayerManager playerManager)
        {
            return playerManager.field_Private_List_1_Player_0.ToArray();
        }

        public static PlayerNet GetPlayerNet(this VRCPlayer vrcPlayer)
        {
            return vrcPlayer._playerNet;
        }

        public static GameObject GetAvatarObject(this VRCPlayer vrcPlayer)
        {
            return vrcPlayer.field_Internal_GameObject_0;
        }

        public static VRCPlayerApi GetPlayerApi(this VRCPlayer vrcPlayer)
        {
            return vrcPlayer.field_Private_VRCPlayerApi_0;
        }


     

        public static bool IsStaff(this APIUser user)
        {
            if (user.hasModerationPowers)
                return true;
            if (user.developerType != APIUser.DeveloperType.None)
                return true;
            return user.tags.Contains("admin_moderator") || user.tags.Contains("admin_scripting_access") ||
                   user.tags.Contains("admin_official_thumbnail");
        }

        private static MethodInfo _reloadAvatarMethod;
        private static MethodInfo LoadAvatarMethod
        {
            get
            {
                if (_reloadAvatarMethod == null)
                {
                    _reloadAvatarMethod = typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Private_Void_Boolean_") && mi.Name.Length < 31 && mi.GetParameters().Any(pi => pi.IsOptional) && XrefUtils.CheckUsedBy(mi, "ReloadAvatarNetworkedRPC"));
                }

                return _reloadAvatarMethod;
            }
        }

        private static MethodInfo _reloadAllAvatarsMethod;
        private static MethodInfo ReloadAllAvatarsMethod
        {
            get
            {
                if (_reloadAllAvatarsMethod == null)
                {
                    _reloadAllAvatarsMethod = typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Public_Void_Boolean_") && mi.Name.Length < 30 && mi.GetParameters().All(pi => pi.IsOptional) && XrefUtils.CheckUsedBy(mi, "Method_Public_Void_", typeof(FeaturePermissionManager)));// Both methods seem to do the same thing;
                }

                return _reloadAllAvatarsMethod;
            }
        }
        public static void ReloadAvatar(this VRCPlayer instance)
        {
            LoadAvatarMethod.Invoke(instance, new object[] { true }); // parameter is forceLoad and has to be true
        }

        public static void ReloadAllAvatars(this VRCPlayer instance, bool ignoreSelf = false)
        {
            ReloadAllAvatarsMethod.Invoke(instance, new object[] { ignoreSelf });
        }

        public static void ChangeToAvatar(this VRCPlayer instance, string avatarId)
        {
          

            var apiModelContainer = new ApiModelContainer<ApiAvatar>
            {
                OnSuccess = new Action<ApiContainer>(c =>
                {
                    var pageAvatar = Resources.FindObjectsOfTypeAll<PageAvatar>()[0];
                    var apiAvatar = new ApiAvatar
                    {
                        id = avatarId
                    };
                    pageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = apiAvatar;
                    pageAvatar.ChangeToSelectedAvatar();
                })
            };
            API.SendRequest($"avatars/{avatarId}", 0, apiModelContainer, null, true, true, 3600f, 2);
        }
    }
}
