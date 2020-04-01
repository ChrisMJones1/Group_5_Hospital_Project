using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group_5_Hospital_Project.Models
{
    public class Newsletter
    {
        [Key]
        public int newsletter_id { get; set; }
        public string newsletter_title { get; set; }
        public string newsletter_body { get; set; }
        public ICollection<Subscriber> Subscribers { get; set; }
    }
}