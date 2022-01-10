using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace ViewModel.Helpers.Decisiontree
{
    public class DecisionQuery : Decision
    {
        public string Question { get; set; }
        public Decision Positive { get; set; }
        public Decision Negative { get; set; }

        public Func<Profile,bool> Requirement { get; set; }

        public override void Evaluate(Profile profile)
        {
            bool result = Requirement(profile);
            if (result) Positive.Evaluate(profile);
            else Negative.Evaluate(profile);
            
        }
    }
}
