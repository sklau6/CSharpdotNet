using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleRestApiClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string url = "https://jsonplaceholder.typicode.com/todos/1";

            try
            {
                Todo todo = await FetchTodoAsync(url);
                Console.WriteLine($"Title: {todo.Title}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static async Task<Todo> FetchTodoAsync(string url)
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Todo todo = JsonConvert.DeserializeObject<Todo>(jsonResponse);
                return todo;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }
    }

    public class Todo
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }
    }
}
