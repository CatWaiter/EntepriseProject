using System.ComponentModel.DataAnnotations;

namespace EnterpriseProject.Models
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public string Data { get; set; }

        public string UserName { get; set; }

        public string IpAddress { get; set; }

        public DateTime ActivityData { get; set; } = DateTime.Now;
    }
}
