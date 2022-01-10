using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ViewModel.Helpers.Decisiontree.Methodes
{
    public static class SameCityCheck
    {
        /// <summary>
        /// Checks of potential match and logged in user live in the same city
        /// </summary>
        /// <param name="pf">profile of the potential match</param>
        /// <param name="lipf">profile of the logged in user</param>
        /// <returns>true if they live is the same city and add 5 points to overall score, false if not in the same city</returns>
        public static bool CheckSameCity(Profile pf, Profile lipf)
        {
            if (pf.City == lipf.City)
            {
                MainDecisionTree.Score += 5;
                return true;
            }
            return false;
        }
    }
}
