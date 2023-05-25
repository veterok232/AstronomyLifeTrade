namespace Api.Constants;

public static class Routes
{
    public const string Swagger = "/swg";

    public const string Hangfire = "/hangfire";

    public const string Api = "/api";
    
    public static class Identity
    {
        public const string Root = "identity";

        public const string Login = "login";
        
        public const string RegisterConsumer = "register-consumer";

        public const string Logout = "logout";

        public const string RefreshAccessToken = "refresh-access-token";

        public const string ExtendSession = "extend-session";

        public const string SwitchToProxyAccess = "switch-to-proxy-access";

        public const string ChooseAssignment = "choose-assignment";

        public const string CreateOneTimeToken = "create-one-time-token";
    }

    public static class Catalog
    {
        public const string Root = "catalog";

        public const string GetPopularProducts = "get-popular-products";

        public const string SearchTelescopes = "telescopes";
        
        public const string ProductDetails = "product-details/{productId}";

        public const string GetProductRating = "get-product-rating/{productId}";

        public const string AddProductToFavorites = "add-product-to-favorites";
    }

    public static class Cart
    {
        public const string Root = "cart";
        
        public const string Get = "get";
        
        public const string GetProductInCart = "get-product-in-cart";
        
        public const string Add = "add";
        
        public const string Remove = "remove";

        public const string ChangeCount = "change-count";
    }

    public static class File
    {
        public const string Root = "file";
        
        public const string Download = "download";
        
        public const string DownloadAnonymously = "download-anonymously";
    }
}