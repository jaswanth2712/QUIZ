using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QUIZ.Models
{
    public class TopicTable
    {
        public TopicTable()
        {
            questionTable = new HashSet<QuestionTable>();
        }
        [Key]
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public virtual ICollection<ScoreRecorder> ScoreRecorders { get; set; }
        public virtual ICollection<QuestionTable> questionTable { get; set; }
    }
    public class QuestionTable
    {
        public QuestionTable()
        {
            answerTable = new HashSet<AnswerTable>();
        }
        [Key]
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public int TopicId { get; set; }
        public virtual TopicTable topicTable { get; set; }
        public virtual ICollection<AnswerTable> answerTable { get; set; }
    }
  
    public class AnswerTable
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public string CorrectAnswer { get; set; }
        public string AnswerType { get; set; }
        public int QuestionId { get; set; }
       
        public virtual QuestionTable QuestionTableRefernce { get; set; }
    }
}
