using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumPlMvc.Controllers
{
    public static class ControllerExtention
    {
        public static IEnumerable<T> GetItemsOnPage<T>(this Controller c, IEnumerable<T> items, int? page, int itemsPerPage)
        {
            int itemsCount = items.Count();
            int pageCount = (int)Math.Ceiling(itemsCount / (double)itemsPerPage);
            if (!page.HasValue || (page.Value <= 0) || (page.Value > pageCount))
                page = 1;

            c.ViewBag.Page = page.Value;
            c.ViewBag.PageCount = pageCount;

            return items
                .Skip((page.Value - 1) * itemsPerPage)
                .Take(itemsPerPage);
        }
    }
}