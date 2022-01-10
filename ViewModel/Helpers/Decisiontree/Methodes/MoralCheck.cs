using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace ViewModel.Helpers.Decisiontree.Methodes
{
    public static class MoralCheck
    {

        /// <summary>
        /// Check for the same morals
        /// </summary>
        /// <param name="pf">profile of the potential match</param>
        /// <param name="loggedUser">profile of the logged in user</param>
        /// <returns>true or false</returns>
        public static bool CheckMoral(List<Moral> pf, List<Moral> loggedUser)
        {
            //var sameMoral = pf.Where(item => loggedUser.Contains(item)).ToList();
            //var sameInterest = pf.Where(item => loggedUser.Contains(item)).ToList();

            var sameMoral = MoralID(pf, loggedUser);
            
            if (sameMoral.Count >= 5)
                MainDecisionTree.Score = 100;
            else if (sameMoral.Count >= 3 && sameMoral.Count < 5)
                MainDecisionTree.Score = 75;
            else if (sameMoral.Count < 3 && sameMoral.Count >= 1)
                MainDecisionTree.Score = 50;
            else
                return false;

            return true;

        }

        /// <summary>
        /// check how many moral are the same
        /// </summary>
        /// <param name="listpf">a list of moral from potential match</param>
        /// <param name="listLiu">a list of moral from logged in user</param>
        /// <returns>a list with more moral that are te same</returns>
        private static List<int> MoralID(List<Moral> listpf, List<Moral> listLiu)
        {
            List<int> moralIDpf = new List<int>();
            foreach (var l in listpf)
            {
                moralIDpf.Add(l.MoralID);
            }
            List<int> moralIDliu = new List<int>();
            foreach (var l in listLiu)
            {
                moralIDliu.Add(l.MoralID);
            }

            List<int> returnList = moralIDpf.Where(item => moralIDliu.Contains(item)).ToList();

            return returnList;
        }
    }
}
