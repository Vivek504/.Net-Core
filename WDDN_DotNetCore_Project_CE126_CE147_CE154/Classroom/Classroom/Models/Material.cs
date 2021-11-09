using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public class Material
    {
        public int MaterialId { get; set; }
        public int ClassCode { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public string Desc { get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public byte[] Document { get; set; }
        public bool IsAssignment { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime? UploadTime { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
    }
}
