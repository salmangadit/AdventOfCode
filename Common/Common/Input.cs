namespace AdventOfCode.Common
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Input
    {
        private const string SessionId = "53616c7465645f5fd38179d0788c5028bd2bb35b1b1ee8eebbe2eb2295c43a78583a7008a649d212811b7e3d15647972";

        public static async Task<string> Get(int year, int day)
        {
            var baseAddress = new Uri("https://adventofcode.com");
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, $"/{year}/day/{day}/input");
                message.Headers.Add("Cookie", $"session={SessionId};");
                var result = await client.SendAsync(message);
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
