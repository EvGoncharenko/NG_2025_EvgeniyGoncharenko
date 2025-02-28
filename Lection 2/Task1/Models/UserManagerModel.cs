namespace Task1.Models
{
    public class UserManagerModel
    {
        /* List of users storing objects of type User.  
           Used to simulate a user database. */
        public static int user_id = 1;
        public static List<UserModel> users = new List<UserModel>();

        // Create new users
        public bool create_user(string name, string password, bool isAdmin)
        {

            foreach (var user in users)
            {
                if (user.UserName == name)
                {
                    return false;
                }
            }

            // Creating a new User object with the provided username, password, and admin status.
            var NewUser = new UserModel()
            {
                Id = user_id++,
                UserName = name,
                Password = password,
                IsAdmin = isAdmin
            };

            users.Add(NewUser);
            Console.WriteLine("Create new user");

            return true;
        }

        // Method for user data verification.
        public bool user_data_verification(string name, string password)
        {

            foreach (var user_item in users)
            {
                if (user_item.UserName == name && user_item.Password == password)
                {
                    return true;
                }
            }

            return false;
        }

        public bool admin_check(string name)
        {

            foreach (var user_item in users)
            {
                if (user_item.UserName == name)
                {
                    if (user_item.IsAdmin)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public void user_info(string name)
        {
            foreach (var user_item in users)
            {
                if(user_item.UserName == name)
                {

                    Console.WriteLine("User with the name " + user_item.UserName + " has connected.");
                    Console.WriteLine("Id: " + user_item.Id);
        
                }
            }
        }
    }
}
