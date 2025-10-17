
using System.ComponentModel.DataAnnotations;


namespace CustomerTask.Core.Dtos
{
    public class CustomerDto
    {
        // Customer Data
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required, StringLength(14)]
        public string NationalID { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Required]
        [Display(Name = "Governorate")]
        public int GovernorateId { get; set; }

        [Required]
        [Display(Name = "District")]
        public int DistrictId { get; set; }

        [Required]
        [Display(Name = "Village")]
        public int VillageId { get; set; }

        // Look-up Data for Dropdowns (SelectLists)
        public IEnumerable<LookupDto>? Governorates { get; set; }
        public IEnumerable<LookupDto>? Districts { get; set; }
        public IEnumerable<LookupDto>? Villages { get; set; }
        public IEnumerable<LookupDto>? Genders { get; set; }

        // Read-only properties for the Index/Details view
        [Display(Name = "Gender")]
        public string? GenderName { get; set; }
        [Display(Name = "Location")]
        public string? FullLocation { get; set; }
        [Range(5000, 20000)]
        [Required]
        [Display(Name = "Salary")]

        public decimal? Salary { get; set; }
        public int Age { get; set; }
    }
}
