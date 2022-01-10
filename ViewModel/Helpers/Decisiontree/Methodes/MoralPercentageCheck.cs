using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Linq;
namespace ViewModel.Helpers.Decisiontree.Methodes
{
    public static class MoralPercentageCheck
    {

        /// <summary>
        /// Check the percentage of the moral add to the score or removes 2 points if its more then 10% off
        /// </summary>
        /// <param name="pf">profile of potential match</param>
        /// <param name="loggedUser">profile of the logged in user</param>
        /// <returns>return true or false</returns>
        public static bool CheckMoralPercentage(List<Moral> pf, List<Moral> loggedUser)
        {

            var sameMoral = GetSameMorals(pf, loggedUser);

            if (sameMoral.Count > 0)
            {

                foreach (var m in sameMoral)
                {
                    var pfm = pf.Find(item => item.MoralName == m.MoralName);
                    var lum = loggedUser.Find(item => item.MoralName == m.MoralName);

                    if (pfm.Percentage > (lum.Percentage - 10) && pfm.Percentage < (lum.Percentage + 10))
                        MainDecisionTree.Score += 10;
                    else
                        MainDecisionTree.Score -= 2;
                }
                return true;
            }

            return false;

        }

        /// <summary>
        /// add the moral that are the same in a list
        /// </summary>
        /// <param name="pf">profile of potential match</param>
        /// <param name="loggedUser">profile of the logged in user</param>
        /// <returns>a list of the same moral of the logged in user</returns>
        private static List<Moral> GetSameMorals(List<Moral> pf, List<Moral> loggedUser)
        {
            List<Moral> sameMoralList = new List<Moral>();

            foreach (var pff in pf)
            {
                var pfm = loggedUser.FindAll(item => item.MoralName == pff.MoralName);
                if (pfm.Count > 0)
                    sameMoralList.Add(pff);
            }
            return sameMoralList;
        }
    }
}
