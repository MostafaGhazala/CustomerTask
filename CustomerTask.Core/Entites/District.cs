using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTask.Core.Entites
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GovernorateId { get; set; }
    }
}
