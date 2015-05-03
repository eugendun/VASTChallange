using numl.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace VASTChallange.Core.Entities
{
    public class Movement
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public virtual Visitor Visitor { get; set; }
    }
}
