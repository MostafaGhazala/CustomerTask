using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTask.Core.Entites
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, StringLength(14)]
        public string NationalID { get; set; } = string.Empty;

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }

        [ForeignKey("Village")]
        public int VillageId { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; }

        [Range(5000, 20000)]
        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        public Governorate? Governorate { get; set; }
        public District? District { get; set; }
        public Village? Village { get; set; }
        public Gender? Gender { get; set; }
    }
}
