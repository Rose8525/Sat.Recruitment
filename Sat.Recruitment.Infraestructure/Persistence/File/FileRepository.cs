using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Sat.Recruitment.Infraestructure.Persistence.File
{
    public class FileRepository : IUserRepository
    {
        public string FilePath { get; }

        public FileRepository(string filePath)
        {
            FilePath = filePath;
        }
        
        public async Task<bool> ExistsUserAsync(User newUser)
        {
            var usersList = new List<User>();

            await using var fileStream = new FileStream(FilePath, FileMode.Open);
            using var reader = new StreamReader(fileStream); 

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString().ToLower(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = Enum.Parse<UserType>(line.Split(',')[4].ToString()),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                usersList.Add(user);
            }

            reader.Close();

            foreach (var user in usersList)
            {
                if (user.Email == newUser.Email || user.Phone == newUser.Phone
                                                || (user.Name == newUser.Name && user.Address == newUser.Address))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            var line = new StringBuilder().AppendLine().Append(user.Name).Append(",")
                .Append(user.Email).Append(",")
                .Append(user.Phone).Append(",")
                .Append(user.Address).Append(",")
                .Append(user.UserType).Append(",")
                .Append(user.Money).ToString();

            await System.IO.File.AppendAllTextAsync(FilePath, line);

            return true;
        }
    }
}

