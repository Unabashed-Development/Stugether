using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ViewModel.Helpers.Decisiontree.Methodes
{
    public static class SameSchoolCheck
    {
        /// <summary>
        /// Check of the potential match and the logged in user are on the same school
        /// </summary>
        /// <param name="pfSchool">school of the potential match</param>
        /// <param name="logedInUserSchool">school of the logged in user</param>
        /// <returns>true if schools are the same, false if schools are not the same</returns>
        public static bool CheckSameSchool(School pfSchool , School logedInUserSchool)
        {
            if (pfSchool.SchoolName.Equals(logedInUserSchool.SchoolName))
            {
                MainDecisionTree.Score += 5;
                return true;
            }
            return false;
        }
    }
}
