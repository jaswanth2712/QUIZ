using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QUIZ.Models;

namespace QUIZ.Controllers
{
    public class QuizQController : Controller
    {
        public QuestionerContext context;
     
        public QuizQController(QuestionerContext Context)
        {
            context = Context;
        }
            public IActionResult Index(int Id)
        {
            TempData["TopicId"] = Id;
            ViewBag.Questions = context.QuestionTables.Where(x=>x.TopicId==Id).ToList();
            TempData["TotalQ"] = context.QuestionTables.Where(x => x.TopicId == Id).Count();
            return View();
        }
        public IActionResult TopicGenerator()
        {
            var data = context.TopicTables.ToList();
            return View(data);
        }
        public IActionResult CreateTopic()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTopic(IFormCollection FormValues)
        {
            TempData["Topic"] = FormValues["TopicName"].ToString();
            TopicTable t = new TopicTable();
            t.TopicName = TempData.Peek("Topic").ToString();
            context.Add(t);
            context.SaveChanges();
            return RedirectToAction("CreateQuiz");
        }
        public IActionResult CreateQuiz()
        {

            return View();
        }
        public IActionResult StoreQuestions(IFormCollection FormValues)
        {
            string CurrentTopic = TempData.Peek("Topic").ToString();
            string charry = FormValues["choices"];
            string[] choiceProcess = charry.Split("###");
            string a = choiceProcess[1];
            string c = a.Substring(0, a.Length - 1);
            string[] b = c.Split("*");
            string question = FormValues["Question"];
            QuestionTable q = new QuestionTable();
            q.QuestionName = question;
            q.TopicId = context.TopicTables.Where(x => x.TopicName.Equals(CurrentTopic)).FirstOrDefault().TopicId;
            context.QuestionTables.Add(q);
            context.SaveChanges();
            string answer = FormValues["Answer"];
            q = context.QuestionTables.Where(d => d.QuestionName == question).FirstOrDefault();
            answer = answer.Substring(0, answer.Length - 1);
            if (choiceProcess[0].Equals("Radio"))
            {
                foreach (var item in b)
                {
                    AnswerTable ans = new AnswerTable();
                    ans.AnswerType = "Radio";
                    ans.QuestionId = q.QuestionId;
                    ans.AnswerName = item;

                    if (item.Equals(answer))
                    {
                        ans.CorrectAnswer = "true";
                    }
                    else
                    {
                        ans.CorrectAnswer = "false";
                    }

                    context.AnswerTables.Add(ans);

                }

            }
            else
            {
                string[] ansarray = answer.Split("/,");
                foreach (var item in b)
                {
                    AnswerTable ans = new AnswerTable();
                    ans.QuestionId = q.QuestionId;
                    ans.AnswerName = item;
                    ans.AnswerType = "Check";

                    if (ansarray.Contains(item))
                    {
                        ans.CorrectAnswer = "true";
                    }
                    else
                    {
                        ans.CorrectAnswer = "false";
                    }

                    context.AnswerTables.Add(ans);

                }

            }

            context.SaveChanges();



            return View();
        }
        public ActionResult CheckAnswer(IFormCollection FormValues)
        {
            int Score = 0;
            string[] QId = FormValues["questionid"];
            //Question Manipulation
            foreach (var item in QId)
            {
                var Opt = (from s in context.AnswerTables where s.QuestionId == int.Parse(item) select  s.AnswerType).Distinct();
                string OptionType = Convert.ToString(Opt);
                if (OptionType.Equals("Radio"))
                {
                    int Correct = context.QuestionTables.Find(int.Parse(item)).answerTable.Where(a => a.CorrectAnswer == "true").FirstOrDefault().AnswerId;
                    if (Correct == int.Parse(FormValues["question_" + item]))
                    {
                        Score++;
                    }
                }
                else
                {
                    string[] a = FormValues["question_"+item];
                    var val = (from d in context.AnswerTables
                              where d.QuestionId == int.Parse(item) & d.CorrectAnswer == "true"
                              orderby d.AnswerId
                              select d.AnswerId);
                    var tempArr = val.ToArray();
                    if (a.Length == val.Count())
                    {
                        int flag = 0;

                        //choice manipulation
                        for (int i = 0; i < a.Length; i++)
                        {
                            if (Convert.ToInt32(a[i]) == tempArr[i])
                            {
                                flag = 1;
                            }
                            else
                            {
                                flag = 0;
                                break;
                            }
                        }

                        if (flag == 1)
                        {
                            Score++;
                        }
                    }
        
                }
            }
            ViewBag.Total = QId.Length;
            ViewBag.Score = Score;
            ScoreRecorder sr = new ScoreRecorder();
            sr.TopicId = Convert.ToInt32(TempData["TopicId"]);
            sr.UserId = context.Users.Where(r => r.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault().Id;
            sr.Score = Score;
            sr.TotalNoOfQuestions = Convert.ToInt32(TempData["TotalQ"]);
            sr.Submitted = DateTime.Now;
            context.Add(sr);
            context.SaveChanges();
            return View();
        }
        
        
       
        public IActionResult GetResult()
        {
            var Data = context.ScoreRecorders.ToList();
            return View(Data);
        }
        public IActionResult DeleteForScore(int id)
        {
            var data = context.ScoreRecorders.Where(s => s.ScoreId == id).FirstOrDefault();
            context.Remove(data);
            context.SaveChanges();
            TempData["DeleteForScore"] = "<script>alert('Data Deleted Scuccessfully :)')</script>";
            return RedirectToAction("GetResult");
        } 
        public IActionResult  DeleteForTopic(int id)
        {
            var data = context.TopicTables.Where(s => s.TopicId == id).FirstOrDefault();
            context.Remove(data);
            context.SaveChanges();
            TempData["DeleteForTopic"]= "<script>alert('Topic Deleted Scuccessfully :)')</script>";
            return RedirectToAction("TopicGenerator");
        }
    }
}