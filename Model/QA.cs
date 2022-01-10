namespace Model
{
    /// <summary>
    /// Holds the qa question and answer information
    /// </summary>
    public class QA
    {

        #region properties
        public int QaID { get; set; }
        public string QaQuestion { get; set; }
        public string QaAnswer { get; set; }
        #endregion

        #region constuctors
        public QA(int qaID, string qaQuestion, string qaAnswer)
        {
            QaID = qaID;
            QaQuestion = qaQuestion;
            QaAnswer = qaAnswer;
        }

        public QA(int qaID, string qaQuestion)
        {
            QaID = qaID;
            QaQuestion = qaQuestion;
            QaAnswer = "";
        }
        #endregion

        #region methods
        public override bool Equals(object obj)
        {
            QA otherQa = (QA)obj;
            return QaID == otherQa.QaID;
        }
        #endregion

    }
}
