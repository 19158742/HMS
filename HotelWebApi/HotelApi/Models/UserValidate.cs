﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApi.Models
{
    public class UserValidate
    {
        public static bool Login(string username, string password)
        {
            UsersBL userBL = new UsersBL();
            var UserLists = userBL.GetUsers();
            return UserLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }
    }
}