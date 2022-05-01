using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReviewAPI.Auth
{
    public static class InMemoryUserLogins
    {
        static InMemoryUserLogins()
        {
            Users = new List<User>() {
                new User() {
                    Id=1,
                    Name = "admin",
                    Surname = "admin",
                    Password = "12345",
                    Email = "admin@g.c"
                }
            };
        }
        public static List<User> Users { get; set; }     
    }
}
