using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ViewModel.Helpers.Decisiontree.Methodes
{
    public static class SameAgeCheck
    {
        /// <summary>
        /// Check if age is the same
        /// </summary>
        /// <param name="pf">profile of potential match</param>
        /// <param name="liupf">profile of logged in user </param>
        /// <returns>true if CheckAge is true and add 5 point to score, false if CheckAge is false</returns>
        public static bool CheckSameAge(Profile pf, Profile liupf)
        {
            if (CheckAge(pf,liupf))
            {
                MainDecisionTree.Score += 5;
                return true;
            }

            return false;
        }



        /// <summary>
        /// convert the string age to int an check if they are in 2 years of each other
        /// </summary>
        /// <param name="pf">profile of potential match</param>
        /// <param name="liupf">profile of logged in user </param>
        /// <returns>true or false</returns>
        private static bool CheckAge(Profile pf, Profile liupf)
        {
            try
            {
                string pfAgeString = pf.Age.Substring(0, 2);
                string liupfAgeString = liupf.Age.Substring(0, 2);
                int pfage = int.Parse(pfAgeString);
                int liupfAge = int.Parse(liupfAgeString);
                if (pfage >= (liupfAge - 2) && pfage <= (liupfAge + 2))
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        } 
    }
}
