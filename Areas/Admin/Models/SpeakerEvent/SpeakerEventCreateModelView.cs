using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHomeProject.Areas.Admin.Models.SpeakerEvent
{
    public class SpeakerEventCreateModelView
    {
        public int SpeakerId { get; set; }
        public List<SelectListItem> SpeakerList { get; set; } = new();
        public int EventId { get; set; }
        public List<SelectListItem> EventList { get; set; } = new();
    }
}
