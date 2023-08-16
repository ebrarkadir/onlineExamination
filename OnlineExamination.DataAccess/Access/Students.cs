using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.DataAccess.Access
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string CVFileName { get; set; }
        public string PictureFileName { get; set; }
        public int GroupsId { get; set; }
        public Groups Groups { get; set; }
        public ICollection<ExamResaults> ExamResaults { get; set; } = new HashSet<ExamResaults>();

    }
}
