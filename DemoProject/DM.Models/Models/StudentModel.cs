using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models.Models
{
    public class StudentModel
    {
        public int id { get; set; }

        public List<int> ClassId { get; set; }

        [Required]
        [StringLength(50)]
        public string StudentName { get; set; }

        [Required]
        public int StudentAge { get; set; }

        [StringLength(500)]
        public string StudentAddress { get; set; }

        [StringLength(50)]
        public string StudentSex { get; set; }

        public List<ClassModel> Classes { get; set; }

    }
}
