using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimebookingMVC2.Api
{
    // TODO: Add authentication things? 
    public class ApiCommunication
    {
        HttpClient _client = new HttpClient();
        private readonly string url = "https://localhost:44363/";

        /// <summary>
        /// Gets a list of all bookings from the API.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Posts a booking to the API to save in the database.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a booking from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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