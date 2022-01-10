namespace Model
{
    public class QA
    {

        public int QaID { get; set; }
        public string QaQuestion { get; set; }
        public string QaAnswer { get; set; }

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

        public override bool Equals(object obj)
        {
            QA otherQa = (QA)obj;
            return QaID == otherQa.QaID;
        }

    }
}
