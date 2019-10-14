using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TimebookingMVC2.Api.Models;

namespace TimebookingMVC2.Api
{
    // TODO: Add authentication things? 
    public class ApiCommunication
    {
        HttpClient _client = new HttpClient();
        private readonly string url = "https://timebookingapi20191009012914.azurewebsites.net/";

        //Login 
        public string PostToken(User user)
        {
            var client = new RestClient(@"https://localhost:44363");
            var request = new RestRequest(@"https://localhost:44363/oauth/token", Method.POST);

            request.AddParameter("username", user.UserName);
            request.AddParameter("password", user.UserPassword);
            request.AddParameter("grant_type", "password");

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var parsedToken = JsonConvert.DeserializeObject<Token>(response.Content.ToString());

                if (!string.IsNullOrEmpty(parsedToken.access_token))
                {
                    return parsedToken.access_token;
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        //Register
        public string PostRegister(User user)
        {
            var client = new RestClient(@"https://localhost:44363");
            var request = new RestRequest(@"https://localhost:44363/user", Method.POST);

            request.AddParameter("UserName", user.UserName);
            request.AddParameter("UserEmail", user.UserEmail);
            request.AddParameter("UserPassword", user.UserPassword);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
                return string.Empty;
            
        }

        public async Task<List<Booking>> GetBookingsAsync()
        {
            try
            {
                // Get response and content stream
                var response = _client.GetAsync(url + @"Booking").Result;
                var stream = response.Content.ReadAsStreamAsync();
                string responseJson = "";
                // Parse and return stream content
                using (var sr = new StreamReader(await stream))
                {
                    responseJson = sr.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<List<Booking>>(responseJson);
            }
            catch { return null; }
        }

        public bool Post(Booking booking)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(booking);

                // Need a HttpContent for POST methods
                var content = new StringContent(jsonData, Encoding.Default, "application/json");
                var response = _client.PostAsync(url + @"Booking/Post", content);

                if (response.Result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        public bool DeleteBooking(int id)
        {
            try
            {
                var response = _client.DeleteAsync(url + "Booking/" + id);
                if (response.Result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        // This might not be needed?
        /// <summary>
        /// Gets a list of all users from the API.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                // Get response and content stream
                var response = _client.GetAsync(url + @"User").Result;
                var stream = response.Content.ReadAsStreamAsync();
                string responseJson = "";
                // Parse and return stream content
                using (var sr = new StreamReader(await stream))
                {
                    responseJson = sr.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<List<User>>(responseJson);
            }
            catch { return null; }
        }
        
        /// <summary>
        /// Post to create a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Post(User user)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(user);
                // Need a HttpContent for POST methods
                var content = new StringContent(jsonData, Encoding.Default, "application/json");
                var response = _client.PostAsync(url + @"User/Post", content);

                if (response.Result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            try
            {
                var response = _client.DeleteAsync(url + "User/" + id);
                if (response.Result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
    }
}