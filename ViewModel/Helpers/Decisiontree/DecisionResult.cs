using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Helpers.Decisiontree
{
    public class DecisionResult : Decision
    {

        public bool result { get; set; }
        public int QuestionNumber { get; set; }
        public AllLists WhichList { get; set; }

        /// <summary>
        /// the result of the decision tree and the potential match to a list 
        /// </summary>
        /// <param name="profile">a potential match</param>
        public override void Evaluate(Profile profile)
        {
            if (WhichList == AllLists.Other)
            {
                MainDecisionTree.Other.Add(profile);
            }
            else
            {
                if (MainDecisionTree.Score >= 80)
                {
                    MainDecisionTree.BestMatch.Add(profile);
                }
                else if (MainDecisionTree.Score >= 60)
                {
                    MainDecisionTree.GoodMatch.Add(profile);
                }
                else
                {
                    MainDecisionTree.Other.Add(profile);
                }
            }
            MainDecisionTree.Score = 0;
        }
    }

    /// <summary>
    /// all the list that can be returned, so can you force a profile in a specific list
    /// </summary>
    public enum AllLists
    {
        Best,
        Good,
        Other
    }
}
