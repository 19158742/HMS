using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApi.Models
{
    public class UsersBL
    {
        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "admin",
                Password = "admin"
            });
            
            return userList;
        }
    }
}