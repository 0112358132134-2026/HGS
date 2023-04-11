using Microsoft.AspNetCore.Mvc.Rendering;

namespace HGSModel
{
    public class Bedpatient
    {
        public int Id { get; set; }

        public int BedId { get; set; }

        public int PatientId { get; set; }

        public string Reason { get; set; } = null!;

        public bool State { get; set; }

        public int DoctorId { get; set; }

        public string? Annotations { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        //

        public string? PatientName { get; set; }

        public string? DoctorName { get; set; }

        public List<SelectListItem>? Beds { get; set; }

        public List<SelectListItem>? Patients { get; set; }

        public List<SelectListItem>? Doctors { get; set; }

        public Bed? Bed { get; set; }

        public Patient? Patient { get; set; }

        public Doctor? Doctor { get; set; }
    }
}