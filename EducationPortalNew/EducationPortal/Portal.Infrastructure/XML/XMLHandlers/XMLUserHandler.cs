using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Portal.Infrastructure.XML.XMLHandlers
{
    public class XMLUserHandler : IUserHandler
    {
        public void CreateUser(User newUser)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<User>));

                List<User> userList = (List<User>)GetAllUsers();

                newUser.Id = userList[userList.Count - 1].Id + 1;

                userList.Add(newUser);

                using (var fileStreamForSeriallize = new FileStream("..//..//..//..//users.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fileStreamForSeriallize, userList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User FindUser(string email)
        {
            List<User> userList = (List<User>)GetAllUsers();

            foreach(var user in userList)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }

            return null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<User>));

                using (var fileStreamForSeriallize = new FileStream("..//..//..//..//users.xml", FileMode.Open))
                {
                    return (List<User>)formatter.Deserialize(fileStreamForSeriallize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveUser()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User newUser)
        {

        }
    }
}
