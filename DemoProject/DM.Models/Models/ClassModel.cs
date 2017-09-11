using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models.Models
{
    public class ClassModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }

        [StringLength(500)]
        public string ClassDescription { get; set; }

        public List<StudentModel> Students { get; set; }
    }
}
