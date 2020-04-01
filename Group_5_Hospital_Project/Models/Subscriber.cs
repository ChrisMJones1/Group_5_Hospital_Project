using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group_5_Hospital_Project.Models
{
    public class Subscriber
    {
        [Key]
        public int subscriber_id { get; set; }

        public string subscriber_name { get; set; }
        public string subscriber_email { get; set; }
        public ICollection<Newsletter> Newsletters { get; set; }
    }
}