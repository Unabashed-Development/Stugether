using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace ViewModel.Helpers.Decisiontree
{
    public abstract class Decision
    {
        /// <summary>
        /// the evalute method, used in the descision tree
        /// </summary>
        /// <param name="profile">profile of the potential match</param>
        public abstract void Evaluate(Profile profile);
    }
}
