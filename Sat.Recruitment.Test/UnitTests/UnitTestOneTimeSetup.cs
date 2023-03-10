using Sat.Recruitment.Test.TestUtils;

namespace Sat.Recruitment.Test.UnitTests
{
    public class UnitTestOneTimeSetup
    {
        public UnitTestOneTimeSetup()
        {
            UtilsTest.Init("UnitTestUsers.txt");
        }
    }
}
