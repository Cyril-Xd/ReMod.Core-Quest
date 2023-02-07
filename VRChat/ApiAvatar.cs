using VRC.Core;

namespace ReMod.Core.VRChat
{
    internal class Avatar_Object
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

        internal Avatar_Object() { }

        internal Avatar_Object(ApiAvatar avtr)
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

        internal Avatar_Object(Avatar_Object avtr)
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
            return num switch
            {
                0 => ApiModel.SupportedPlatforms.All,
                1 => ApiModel.SupportedPlatforms.Android,
                2 => ApiModel.SupportedPlatforms.StandaloneWindows,
                _ => ApiModel.SupportedPlatforms.None
            };
        }

        private static int savePlatform(ApiModel.SupportedPlatforms supportedPlatform)
        {
            return supportedPlatform switch
            {
                ApiModel.SupportedPlatforms.All => 0,
                ApiModel.SupportedPlatforms.Android => 1,
                ApiModel.SupportedPlatforms.StandaloneWindows => 2,
                _ => 3
            };
        }
    }
}