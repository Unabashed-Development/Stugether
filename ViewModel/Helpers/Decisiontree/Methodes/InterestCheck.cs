using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
namespace ViewModel.Helpers.Decisiontree.Methodes
{
    public static class InterestCheck
    {

        /// <summary>
        /// check if the logged in user and a potential match have the same interrest
        /// </summary>
        /// <param name="pf">the profile of a potential match</param>
        /// <param name="loggedUser">the profile of the logged in user</param>
        /// <returns>return true if the potential match has atleast 1 interest otherwise return false</returns>
        public static bool CheckInterest(List<Interest> pf, List<Interest> loggedUser)
        {

            if (pf == null)
            {
                return false;
            }

            var sameInterest = pf.Where(item => loggedUser.Contains(item)).ToList();

            if (sameInterest.Count >= 5)
                MainDecisionTree.Score = 100;
            else if (sameInterest.Count >= 3 && sameInterest.Count < 5)
                MainDecisionTree.Score = 75;
            else if (sameInterest.Count < 3 && sameInterest.Count > 0)
                MainDecisionTree.Score = 50;
            else
                return false;

            return true;
        }
    }
}
