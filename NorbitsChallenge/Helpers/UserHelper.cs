using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Helpers
{
    public static class UserHelper
    {
        private static int LoggedOnUserCompanyId = 1;
        public static int GetLoggedOnUserCompanyId()
        {
            return LoggedOnUserCompanyId;
        }
        public static void setLoggedOnUserCompanyId(int newUser) {
            LoggedOnUserCompanyId = newUser;
        }
    }
}
