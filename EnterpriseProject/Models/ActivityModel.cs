using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EnterpriseProject.Models
{
    public class ActivityModel
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public string IpAddress { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string Color { get; set; }

        public string Icon { get; set; }
    }
}
