using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Holds the data for all the QA of a profile
    /// </summary>
    public class QAData
    {

        #region properties
        public int UserID { get; set; }
        public List<QA> QAList { get; set; }
        #endregion

        #region constructors
        public QAData(int userID, List<QA> qaList)
        {
            UserID = userID;
            QAList = qaList;
        }
        #endregion

    }
}
