using System;
using Xunit;
using PeopleSearch;

namespace PeopleSearch.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string expectedString = "Bryce Lyon";
            string actualString;

            Models.Person person = new Models.Person
            {
                FirstName = "Bryce",
                LastName = "Lyon"
            };

            actualString = person.CombinedName;

            Assert.Equal(expectedString, actualString);
        }
    }
}
