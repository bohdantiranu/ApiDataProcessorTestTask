using System.Text.Json;

namespace ApiDataProcessorTestTask.Services
{
    public class DataFetcher
    {
        private readonly HttpClient _httpClient;

        public DataFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Fetch<T>(string url) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                
                return JsonSerializer.Deserialize<T>(jsonResponse, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP Request failed for {url}: {e.Message}");

                return null;
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Failed to deserialize json for {url}: {e.Message}");

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to fetch data for {url}: {e.Message}");

                return null;
            }
        }
    }
}