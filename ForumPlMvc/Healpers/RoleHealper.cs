using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumPlMvc.Healpers
{
    public static class RoleHealper
    {
        public static bool IsAdmin(this WebViewPage pg)
        {
            return pg.User.IsInRole("Admin");
        }

        public static bool IsModerator(this WebViewPage pg)
        {
            return pg.User.IsInRole("Moderator");
        }

        public static bool IsAdminOrModerator(this WebViewPage pg)
        {
            return IsAdmin(pg) || IsModerator(pg);
        }
    }
}