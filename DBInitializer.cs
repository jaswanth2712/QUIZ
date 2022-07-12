using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QUIZ.Models;

namespace QUIZ
{
    public class DBInitializer
    {

        public static void Initialize(QuestionerContext context)
        {
          

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var user = new User[]
            {
                new User { UserName = "Admin", PassWord = "Admin@123",
                 Role="Admin"},
                 
            };
            
            context.Users.Add(user[0]);
            context.SaveChanges();
            }
        
    }
}
