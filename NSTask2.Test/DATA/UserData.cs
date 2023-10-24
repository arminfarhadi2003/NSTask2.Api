using NSTask2.Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Test.DATA
{
    public class UserData
    {
        public List<User> userData()
        {
            return new List<User>
            {
                new User
                {
                    Email="Test@test.test",
                    PhoneNumber="123456789",
                    UserName="test",

                },
            };
        }
    }
}
