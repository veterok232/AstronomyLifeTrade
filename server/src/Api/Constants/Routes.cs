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
        
        public const string Clear = "clear-cart";
    }

    public static class File
    {
        public const string Root = "files";
        
        public const string Download = "download/{fileId}";
        
        public const string DownloadAnonymously = "download-anonymously/{fileId}";
        
        public const string Upload = "upload";
    }
    
    public static class Comments
    {
        public const string Root = "comments";
        
        public const string Get = "get/{productId}";
        
        public const string Publish = "publish";
    }
    
    public static class Orders
    {
        public const string Root = "orders";
        
        public const string Search = "search";
        
        public const string Details = "details/{orderId}";
        
        public const string GetCustomerInfo = "get-customer-info";
        
        public const string MakeOrder = "make-order";
        
        public const string PostponeOrder = "postpone-order/{orderId}";
        
        public const string CancelOrder = "cancel-order/{orderId}";
        
        public const string ApproveOrder = "approve-order/{orderId}";
        
        public const string CloseOrder = "close-order/{orderId}";
    }
    
    public static class AccountProfile
    {
        public const string Root = "account-profile";
        
        public const string GetUserInfo = "get-user-info";
        
        public const string SaveUserInfo = "save-user-info";
        
        public const string SaveUserAddress = "save-user-address";
    }
}