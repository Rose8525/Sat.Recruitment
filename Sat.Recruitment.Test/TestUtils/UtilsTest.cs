using System.IO;

namespace Sat.Recruitment.Test.TestUtils
{
    public class UtilsTest
    {
        private const string USERS_FOR_TEXT_FILE = "Juan,Juan@marmol.com,+5491154762312,Peru 2464,Normal,1234" +
                                                   "\r\nFranco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234" +
                                                   "\r\nAgustina,Agustina@gmail.com,+534645213542,Garay y Otra Calle,SuperUser,112234";

        public static void Init(string fileName)
        {
            var pathDb = Path.Combine(Directory.GetCurrentDirectory(), "Users.db");
            if (File.Exists(pathDb))
            {
                File.Delete(pathDb);
            }

            var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }

            using var sw = File.CreateText(pathFile);
            sw.Write(USERS_FOR_TEXT_FILE);
        }
    }
}
