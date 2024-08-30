using Microsoft.AspNetCore.Mvc.Rendering;



namespace Clinicamedica.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public const string Index = "Index";
        public const string ChangePassword = "ChangePassword";
        public const string ExternalLogins = "ExternalLogins";
        public const string PersonalData = "PersonalData";
        public const string TwoFactorAuthentication = "TwoFactorAuthentication";
        public const string Email = "Email";

        public static string IndexNavClass(ViewContext viewContext) =>
            PageNavClass(viewContext, Index);

        public static string ChangePasswordNavClass(ViewContext viewContext) =>
            PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) =>
            PageNavClass(viewContext, ExternalLogins);

        public static string PersonalDataNavClass(ViewContext viewContext) =>
            PageNavClass(viewContext, PersonalData);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) =>
            PageNavClass(viewContext, TwoFactorAuthentication);

        public static string EmailNavClass(ViewContext viewContext) =>
            PageNavClass(viewContext, Email);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            // Asegúrate de que activePage no sea null y maneja el caso de valores posibles null
            var activePage = viewContext.HttpContext.Request.Path.Value ?? string.Empty;
            return activePage.EndsWith(page, StringComparison.OrdinalIgnoreCase) ? "active" : string.Empty;
        }
    }
}
