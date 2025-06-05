using ApiDataProcessorTestTask.Configurations;
using ApiDataProcessorTestTask.Models;
using ApiDataProcessorTestTask.Services;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            var apiUrls = configuration.GetSection("ApiUrls").Get<ApiUrls>();

            using var httpClient = new HttpClient();
            var dataFetcher = new DataFetcher(httpClient);

            Console.WriteLine("Fetching data from APIs...");

            var usersTask = dataFetcher.Fetch<List<User>>(apiUrls.Users);
            var postsTask = dataFetcher.Fetch<List<Post>>(apiUrls.Posts);

            await Task.WhenAll(usersTask, postsTask);

            var users = usersTask.Result;
            var posts = postsTask.Result;

            if (users == null || posts == null)
            {
                Console.WriteLine("Failed to fetch all necessary data");

                return;
            }

            Console.WriteLine("Data fetched successfuly");
            
            var dataProcessor = new DataProcessor();
            dataProcessor.LinkPostsToUsers(users, posts);
            
            bool retryFilter;

            do
            {
                Console.Write("Enter a letter to filter users by cities which starts from it:");

                var filterCityLetter = Console.ReadKey().KeyChar;

                if (!char.IsLetter(filterCityLetter))
                {
                    Console.WriteLine("\nInvalid input. Will be used default filter by 'S'");
                    filterCityLetter = 'S';
                }

                var filteredUsers = dataProcessor.FilterUsersByCityFirstLetter(users, filterCityLetter);

                Console.WriteLine("\nFiltered users:");

                if (filteredUsers.Any())
                {
                    foreach (var user in filteredUsers)
                    {
                        Console.WriteLine($"Name: {user.Name}");
                        Console.WriteLine($"City: {user.Address.City}");
                        Console.WriteLine($"Posts count: {user.Posts.Count}");
                        Console.WriteLine("------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No users found matching the filter.");
                }

                Console.WriteLine("\nDo you want to try another filter? (yes/no): ");

                var retryInput = Console.ReadLine()?.Trim().ToLowerInvariant();
                retryFilter = (retryInput == "yes" || retryInput == "y");
            } while (retryFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }

        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
    }
}
