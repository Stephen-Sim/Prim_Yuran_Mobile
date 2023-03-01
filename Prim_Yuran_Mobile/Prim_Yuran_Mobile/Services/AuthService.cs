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

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                string url = YuranAPI.APIURL + "login";

                var values = new Dictionary<string, string> {
                    { "email", email },
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
    }
}
