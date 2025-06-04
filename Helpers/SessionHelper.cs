namespace proyecto2.Helpers
{
    public static class SessionHelper
    {
        public static bool IsLoggedIn(HttpContext context)
        {
            return !string.IsNullOrEmpty(context.Session.GetString("UserId"));
        }

        public static bool IsAdmin(HttpContext context)
        {
            return context.Session.GetString("UserRole") == "Admin";
        }

        public static bool IsUser(HttpContext context)
        {
            return context.Session.GetString("UserRole") == "User";
        }

        public static string GetUserName(HttpContext context)
        {
            return context.Session.GetString("UserName") ?? "";
        }

        public static string GetUserId(HttpContext context)
        {
            return context.Session.GetString("UserId") ?? "";
        }
    }
}