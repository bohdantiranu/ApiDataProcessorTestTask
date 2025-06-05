using ApiDataProcessorTestTask.Models;

namespace ApiDataProcessorTestTask.Services
{
    public class DataProcessor
    {
        public void LinkPostsToUsers(List<User> users, List<Post> posts)
        {
            var usersById = users.ToDictionary(u => u.Id);

            foreach (var post in posts)
            {
                if (usersById.TryGetValue(post.UserId, out var user))
                    user.Posts.Add(post);
            }
        }

        public List<User> FilterUsersByCityFirstLetter(List<User> users, char firstLetter)
        {
            return users.Where(u =>
                u.Address.City.StartsWith(firstLetter.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
