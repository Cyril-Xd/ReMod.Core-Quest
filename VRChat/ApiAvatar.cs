using VRC.Core;

namespace ReMod.Core.VRChat.Avatars
{
    internal class AvatarFav_Object
    {
        public string id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string authorName { get; set; }
        public string authorId { get; set; }
        public string assetUrl { get; set; }
        public string description { get; set; }
        public string thumbnailImageUrl { get; set; }
        public int version { get; set; }
        public int supportedPlatforms { get; set; }
        public string releaseStatus { get; set; }

        internal AvatarFav_Object() { }

        internal AvatarFav_Object(ApiAvatar avtr)
        {
            id = avtr.id;
            name = avtr.name;
            imageUrl = avtr.imageUrl;
            authorName = avtr.authorName;
            authorId = avtr.authorId;
            assetUrl = avtr.assetUrl;
            description = avtr.description;
            thumbnailImageUrl = avtr.thumbnailImageUrl;
            version = avtr.version;
            supportedPlatforms = savePlatform(avtr.supportedPlatforms);
            releaseStatus = avtr.releaseStatus;
        }

        internal AvatarFav_Object(AvatarFav_Object avtr)
        {
            id = avtr.id;
            name = avtr.name;
            imageUrl = avtr.imageUrl;
            authorName = avtr.authorName;
            authorId = avtr.authorId;
            assetUrl = avtr.assetUrl;
            description = avtr.description;
            thumbnailImageUrl = avtr.thumbnailImageUrl;
            version = avtr.version;
            supportedPlatforms = avtr.supportedPlatforms;
            releaseStatus = avtr.releaseStatus;
        }

        internal ApiAvatar toApiAvatar()
        {
            return new ApiAvatar
            {
                id = id,
                name = name,
                authorName = authorName,
                authorId = authorId,
                assetUrl = assetUrl,
                description = description,
                thumbnailImageUrl = thumbnailImageUrl,
                version = version,
                supportedPlatforms = getPlatform(supportedPlatforms),
                releaseStatus = releaseStatus
            };
        }

        private static ApiModel.SupportedPlatforms getPlatform(int num)
        {
            switch (num)
            {
                case 0:
                    return ApiModel.SupportedPlatforms.All;
                case 1:
                    return ApiModel.SupportedPlatforms.Android;
                case 2:
                    return ApiModel.SupportedPlatforms.StandaloneWindows;
                default:
                    return ApiModel.SupportedPlatforms.None;
            }
        }

        private static int savePlatform(ApiModel.SupportedPlatforms supportedPlatform)
        {
            switch (supportedPlatform)
            {
                case ApiModel.SupportedPlatforms.All:
                    return 0;
                case ApiModel.SupportedPlatforms.Android:
                    return 1;
                case ApiModel.SupportedPlatforms.StandaloneWindows:
                    return 2;
                default:
                    return 3;
            }
        }
    }
}