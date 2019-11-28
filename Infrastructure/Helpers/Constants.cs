namespace RespaunceV2.Infrastructure.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string AdminAccess = "admin_access";
                public const string ManagementAccess = "management_access";
                public const string RegularUserAccess = "regular_user_access";
                public const string SupplierUserAccess = "supplier_user_access";
            }
        }
    }
}