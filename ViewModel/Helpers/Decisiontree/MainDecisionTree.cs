using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Gateway;
using System.Linq;

namespace ViewModel.Helpers.Decisiontree
{
    public static class MainDecisionTree
    {

        /// <summary>
        /// all the list that are going to be returend in order of bestmatch, goodmatch and other
        /// </summary>
        public static List<Profile> BestMatch { get; set; }
        public static List<Profile> GoodMatch { get; set; }
        public static List<Profile> Other { get; set; }

        private static int _score;
        public static int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                if (_score > 100)
                    _score = 100;
                if (_score < 0)
                    _score = 0;
            }
        }


        public static Profile LoggedInUser { get; set; }


        /// <summary>
        /// the main function that needs to be called
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a list of profiles based on intrest and normas and values, in order of people that would be the best match, a good match and other</returns>
        public static List<Profile> GetProfilesBasedOnIntrestAndNormsAndValues(int id)
        {
            LoggedInUser = ProfileDataAccess.LoadProfile(id);
            BestMatch = new List<Profile>();
            GoodMatch = new List<Profile>();
            Other = new List<Profile>();

            var trunk = DecisionTree();
            var allPosibleMatches = SearchProfileDataAccess.GetProfileBasedOnRelationType(id);

            foreach (var pf in allPosibleMatches)
            {
                var pff = ProfileDataAccess.LoadProfile(pf.UserID);
                trunk.Evaluate(pff);
            }

            return BestMatch.Concat(GoodMatch).Concat(Other).ToList();
        }

        /// <summary>
        /// the main function the de decisiontree
        /// </summary>
        public static DecisionQuery DecisionTree()
        {

            //decision 6 (can only improve overall score)
            var sameAgeBranch = new DecisionQuery
            {
                Question = "is de person around the same age 2+/-",
                Requirement = (profile) => Methodes.SameAgeCheck.CheckSameAge(profile,LoggedInUser),
                Positive = new DecisionResult{result = true},
                Negative = new DecisionResult{result = false}
                
            };

            //decision 5 (can only improve overall score)
            var sameCityBranch = new DecisionQuery
            {
                Question = "lives the person in the same city?",
                Requirement = (profile) => Methodes.SameCityCheck.CheckSameCity(profile, LoggedInUser),
                Positive = sameAgeBranch,
                Negative = sameAgeBranch

            };

            //decision 4(can only improve overall score)
            var sameSchoolBranch = new DecisionQuery
            {
                Question = "is the person on the same school?",
                Requirement = (profile) => Methodes.SameSchoolCheck.CheckSameSchool(profile.School,LoggedInUser.School),
                Positive = sameCityBranch,
                Negative = sameCityBranch
            };

            //decision 3
            var moralPercentageBranch = new DecisionQuery
            {
                Question = "are the percentage almost the same?",
                Requirement = (profile) => Methodes.MoralPercentageCheck.CheckMoralPercentage(profile.MoralsData.Morals, LoggedInUser.MoralsData.Morals),
                Positive = sameSchoolBranch,
                Negative = new DecisionResult{WhichList = AllLists.Other}
            };

            //decision 2
            var sameMoralsBranch = new DecisionQuery
            {
                Question = "Has the profile the same Morals?",
                Requirement = (profile) => Methodes.MoralCheck.CheckMoral(profile.MoralsData.Morals, LoggedInUser.MoralsData.Morals),
                Positive = moralPercentageBranch,
                Negative = new DecisionResult { WhichList = AllLists.Other}
            };

            //Decision 2
            //Question is the question that is asked
            //Requirement is the questions requirement
            //if positive continue to other question
            //if negative quit the question(maybe as other questions) 
            var trunk = new DecisionQuery
            {
                Question = "has the profile the same interest?",
                Requirement = (profile) => Methodes.InterestCheck.CheckInterest(profile.InterestsData.Interests,LoggedInUser.InterestsData.Interests),
                Positive = sameMoralsBranch,
                Negative = new DecisionResult()
            };

            //trunk for treetrunk
            return trunk;
        }
    }
}
