using BizCover.Repository.Cars;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.Cars.API.Tests
{
    public class SampleTest
    {
        [Fact]
        public async Task Test_GetAll()
        {
            var response = await GetAsync("/api/cars");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Test_Get_ById()
        {
            var response = await GetAsync("/api/cars/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_Post()
        {
            Car TestCar = new Car
            {
                Id = 1001,
                Make = "Alfa Romeo",
                Model = "Giulia",
                Year = 2000,
                CountryManufactured = "Italy",
                Colour = "White",
                Price = 50000
            };

            string JsonCar = JsonConvert.SerializeObject(TestCar);

            var response = await PostAsync("/api/cars", JsonCar);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Theory]
        [InlineData("[1]", 50000)]
        [InlineData("[1,2]", 95000)]
        [InlineData("[1,2,3]", 129010)]
        [InlineData("[1,2,3,4]", 175085)]
        public async Task Test_Post_Discount(string JsonID, int Expected)
        {           
            var response = await PostAsync("/api/cars/discount", JsonID);

            string strResponse = await response.Content.ReadAsStringAsync();
            double intResponse = double.Parse(strResponse);

            Assert.Equal(Expected.ToString(), intResponse.ToString());
        }

        [Fact]
        public async Task Test_Put()
        {
            Car TestCar = new Car
            {
                Id = 1005,
                Make = "Alfa Romeo",
                Model = "Giulia",
                Year = 2000,
                CountryManufactured = "Italy",
                Colour = "White",
                Price = 50000
            };

            string JsonCar = JsonConvert.SerializeObject(TestCar);

            var response = await PutAsync("/api/cars", JsonCar);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1, "Same Id already exist")]
        [InlineData(1001, "Car Added")]
        public async Task Test_Post_Content(int ID, string Expected)
        {
            Car TestCar = new Car
            {
                Id = ID,
                Make = "Alfa Romeo",
                Model = "Giulia",
                Year = 2000,
                CountryManufactured = "Italy",
                Colour = "White",
                Price = 50000
            };

            string JsonCar = JsonConvert.SerializeObject(TestCar);

            var response = await PostAsync("/api/cars", JsonCar);

            string strResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(Expected, strResponse);
        }
        
        [Theory]
        [InlineData(1009, "Expected Car Not Exist")]
        [InlineData(1, "Car has been updated")]
        public async Task Test_Put_Content(int ID, string Expected)
        {
            Car TestCar = new Car
            {
                Id = ID,
                Make = "Alfa Romeo",
                Model = "Giulia",
                Year = 2000,
                CountryManufactured = "Italy",
                Colour = "White",
                Price = 50000
            };

            string JsonCar = JsonConvert.SerializeObject(TestCar);

            var response = await PutAsync("/api/cars", JsonCar);

            string strResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(Expected, strResponse);
        }

        private async Task<HttpResponseMessage> GetAsync(string requestURI)
        {

            using (var client = new ClientProvider().Client)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "dGVzdDp0ZXN0");

                var response = await client.GetAsync(requestURI);

                return response.EnsureSuccessStatusCode();
            }
        }
        private async Task<HttpResponseMessage> PostAsync(string requestURI, string JsonCar)
        {
            using (var client = new ClientProvider().Client)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "dGVzdDp0ZXN0");

                HttpContent content = new StringContent(JsonCar, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(requestURI, content);

                return response.EnsureSuccessStatusCode();
            }
        }

        private async Task<HttpResponseMessage> PutAsync(string requestURI, string JsonCar)
        {
            using (var client = new ClientProvider().Client)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "dGVzdDp0ZXN0");

                HttpContent content = new StringContent(JsonCar, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(requestURI, content);

                return response.EnsureSuccessStatusCode();
            }
        }
    }
}
