using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTask.Core.Entites
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // 1 ذكر 2 أنثى
    }
}
