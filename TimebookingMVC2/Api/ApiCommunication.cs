using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly static string url = "https://timebookingapi20191009012914.azurewebsites.net/";
        RestClient client = new RestClient(url);
        private string CurrentUser = string.Empty;

        //Login 
        public string PostToken(User user)
        {
            var request = new RestRequest(url + "oauth/token", Method.POST);

            request.AddParameter("username", user.UserName);
            request.AddParameter("password", user.UserPassword);
            request.AddParameter("grant_type", "password");

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var parsedToken = JsonConvert.DeserializeObject<Token>(response.Content.ToString());

                if (!string.IsNullOrEmpty(parsedToken.access_token))
                {
                    CurrentUser = user.UserName;
                    return parsedToken.access_token;
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        //Register
        public bool PostRegister(User user)
        {
            var request = new RestRequest(url + "user", Method.POST);

            request.AddParameter("UserName", user.UserName);
            request.AddParameter("UserEmail", user.UserEmail);
            request.AddParameter("UserPassword", user.UserPassword);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return true;
            }
            else
                return false;
            
        }

        //Get Bookings
        public async Task<List<BookingWEndDate>> GetBookingsAsync(string token)
        {
            try
            {
                var request = new RestRequest(url + "booking", Method.GET);

                request.AddHeader("Authorization", "Bearer " + token);

                var response = client.Execute(request);

                var jsonList = JsonConvert.DeserializeObject<List<Booking>>(response.Content.ToString());
                var bookingList = new List<BookingWEndDate>();

                foreach (var i in jsonList)
                {
                    bookingList.Add(new BookingWEndDate() 
                    {
                        Date = i.Date,
                        EndDate = i.Date.AddMinutes(30),
                        Id = i.Id,
                        UserName = i.UserName 
                    });
                }

                return bookingList;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return null;
        }

        //Add booking
        public bool PostBooking(Booking booking, string token)                         //Fix this
        {
            var request = new RestRequest(url + "booking", Method.POST);

            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("Date", "2019-10-15T10:30:00");
            request.AddParameter("EndDate", "2019-10-15T11:00:00");
            if (CurrentUser != string.Empty)
            {
                request.AddParameter("UserName", CurrentUser);
            }

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return true;
            }
            else
                return false;
        }

        //Delete booking
        public bool DeleteBooking(int id, string token)
        {
            try
            {
                var request = new RestRequest(url + "booking/" + id, Method.DELETE);

                request.AddHeader("Authorization", "Bearer " + token);

                var response = client.ExecuteTaskAsync(request);

                if (response.IsCompleted)
                {
                    return true;
                }
                else
                    return false;

            }
            catch { return false; }
        }
    }
}