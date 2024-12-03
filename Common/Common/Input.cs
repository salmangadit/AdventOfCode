namespace AdventOfCode.Common
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Input
    {
        private const string SessionId = "53616c7465645f5fb0a7912ae10be6f904f1da5e24ed0057540b32c95fafb16a613c3065c755aa12fe0860d02cba16dfa9649d14c79c15435296590230fb12f7";

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
