using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.DATA.EF//.Metadata
{
    [MetadataType(typeof(VideoGameMetadata))]
    public partial class VideoGame { }

    public class VideoGameMetadata
    {
        [Required]
        [StringLength(100)]
        public string GameTitle { get; set; }

        [StringLength(10)]
        public string YearReleased { get; set; }

        [Required]
        public int DeveloperID { get; set; }

        [Required]
        public int PlatformID { get; set; }

        public int StatusID { get; set; }

        public string Description { get; set; }

        public string IMG { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }

    public class DepartmentMetadata
    {
        [StringLength(50)]
        public string DepName { get; set; }
    }

    [MetadataType(typeof(DeveloperMetadata))]
    public partial class Developer { }

    public class DeveloperMetadata
    {
        [StringLength(50)]
        public string DeveloperName { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }

    public class EmployeeMetadata
    {
        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [Required]
        public int ReportsTo { get; set; }
    }

    [MetadataType(typeof(PlatformMetadata))]
    public partial class Platform { }

    public class PlatformMetadata
    {
        [Required]
        [StringLength(50)]
        public string PlatformName { get; set; }
    }

    [MetadataType(typeof(StockStatusMetadata))]
    public partial class StockStatus { }

    public class StockStatusMetadata
    {
        [Required]
        [StringLength(11)]
        public string StockStatus1 { get; set; }
    }
}
