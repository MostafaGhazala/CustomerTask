using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTask.Core.Dtos
{
    public class LookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int? ParentId { get; set; } // District->GovernorateId, Village->DistrictId
    }
}
