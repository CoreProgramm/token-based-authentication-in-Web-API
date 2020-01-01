using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthentication.Models
{
    public class UserMasterRepository
    {
        // This was our context class name
        CoreProgrammEntities context = new CoreProgrammEntities();

        //This method is used to check and validate the user credentials
        public UserMaster ValidateUser(string username, string password)
        {
            return context.UserMasters.FirstOrDefault(user =>
            user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.UserPassword == password);
        }
        //public void Dispose()
        //{
        //    context.Dispose();
        //}
    }
}