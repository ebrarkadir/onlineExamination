using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.DataAccess.Access
{
    public class Exams
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupsId { get; set; }
        public Groups Groups { get; set; }
        public ICollection<ExamResaults> ExamResaults { get; set; } = new List<ExamResaults>();
        public ICollection<QnAs> QnAs { get; set; }
    }
}
