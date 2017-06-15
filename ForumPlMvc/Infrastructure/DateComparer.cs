using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumPlMvc.Infrastructure
{
    public class DateComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return -x.CompareTo(y);
        }
    }
}