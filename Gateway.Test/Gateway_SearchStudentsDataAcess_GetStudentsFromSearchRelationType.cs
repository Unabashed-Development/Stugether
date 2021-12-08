using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_SearchStudentsDataAcess_GetStudentsFromSearchRelationType
    {

        private Student _student;

        [SetUp]
        public void Setup()
        {
            _student = new Student()
            {
                FirstName = "Henk"
            };

            _student.Relationships = new HashSet<Relationships>();
            _student.Relationships.Add(Relationships.Business);
            _student.Relationships.Add(Relationships.Study);
            _student.Relationships.Add(Relationships.Love);
            _student.Relationships.Add(Relationships.Friends);
        }

        [TestCase()]
        public void GetStudentsFromSearchRelationType_ReturnStudentObject()
        {
            SearchStudentsDataAccess.GetStudentsFromSearchRelationType(_student);
            //Assert.AreEqual();
        }
    }
}
