using System;
using System.ComponentModel.DataAnnotations;

namespace VASTChallange.Core.Entities
{
    public class CommData
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual Visitor From { get; set; }

        public virtual Visitor To { get; set; }
    }
}
