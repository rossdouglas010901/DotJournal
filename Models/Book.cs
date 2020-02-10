using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotJournal.Models
{
    public class Book
    {
        [Required]
        public int id { get; set; }
        [MaxLength(450)]
        [Required]
        public string userId { get; set; }
        [Required]
        public string title { get; set; }
        [MaxLength(6)]
        public string colour { get; set; }
        public DateTime dateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dateUpdated { get; set; }
        public virtual ICollection<Page> pages { get; set; }
    }
}
