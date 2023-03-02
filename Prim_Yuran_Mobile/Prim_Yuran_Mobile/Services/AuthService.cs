using Newtonsoft.Json;
using Prim_Yuran_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prim_Yuran_Mobile.Services
{
    public class AuthService
    {
        public HttpClient client { get; set; }
        public AuthService()
        {
            client = new HttpClient();
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                string url = YuranAPI.APIURL + "login";

                var values = new Dictionary<string, string> {
                    { "username", username },
                    { "password", password },
                };

                var content = new FormUrlEncodedContent(values);

                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadAsStringAsync();
                    Application.Current.Properties["user_id"] = data;
                    return true;
                }

                return false;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

        public async Task<User> getUserInfo()
        {
            try
            {
                string url = YuranAPI.APIURL + "getUserInfo";
                var user_id = Convert.ToInt16(Application.Current.Properties["user_id"]);

                var values = new Dictionary<string, string> {
                    { "user_id", user_id.ToString() },
                };

                var content = new FormUrlEncodedContent(values);
                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<User>(data);
                    return result;
                }

                return null;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }
    }
}
