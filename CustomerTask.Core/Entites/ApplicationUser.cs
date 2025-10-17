using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTask.Core.Entites
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        // هنا تضيف أي خصائص إضافية للمستخدم
        public string? FullName { get; set; }
    }
}
