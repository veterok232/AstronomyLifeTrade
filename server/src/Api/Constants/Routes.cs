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

        public const string Get = "get";
    }
}