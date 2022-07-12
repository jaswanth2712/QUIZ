using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QUIZ.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
         [Required]
        public string UserName { get; set; }
        [MaxLength(20)]
        [Required]
        public string  PassWord { get; set; }
        [Required]
        public string Role { get; set; }
        public virtual ICollection<ScoreRecorder> ScoreRecorders { get; set; }
       // public virtual ICollection<TopicTable> topicTable { get; set; }
    }
    public class ScoreRecorder {
        [Key]
        public int ScoreId { get; set; }
     
        public int UserId { get; set; }
        public virtual User user { get; set; }
        public int TopicId { get; set; }
        public virtual TopicTable topicTable { get; set; }
        public int Score { get; set; }
        public int TotalNoOfQuestions { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Submitted { get; set; }


    }

}
