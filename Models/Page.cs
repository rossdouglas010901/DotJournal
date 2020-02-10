using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotJournal.Models
{
    public class Page
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string pageNum { get; set; }
        [Required]
        public string content { get; set; }
        public int bookId { get; set; }  //FK 
        public DateTime dateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dateUpdated { get; set; }
        public virtual Book book { get; set; }
    }
}
