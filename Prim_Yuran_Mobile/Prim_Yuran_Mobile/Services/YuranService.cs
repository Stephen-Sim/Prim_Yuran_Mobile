using Newtonsoft.Json;
using Prim_Yuran_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prim_Yuran_Mobile.Services
{
    public class YuranService
    {
        public HttpClient client { get; set; }
        public int user_id { get; set; }
        public YuranService()
        {
            client = new HttpClient();
            user_id = Convert.ToInt16(Application.Current.Properties["user_id"]);
        }

        public async Task<List<School>> GetSchools()
        {
            try
            {
                string url = YuranAPI.APIURL + $"getOrganizationByUserId?user_id={this.user_id}";
                var res = await client.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<List<School>>(res);
                return result;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }

        public async Task<List<PayHistory>> GetPayHistories(int oid)
        {
            try
            {
                string url = YuranAPI.APIURL + $"getReceiptByOid";
                var values = new Dictionary<string, string>
                {
                    {"oid", oid.ToString() },
                    {"user_id", user_id.ToString() }
                };

                var content = new FormUrlEncodedContent(values);
                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    var histories = JsonConvert.DeserializeObject<List<PayHistory>>(result);
                    return histories;
                }

                return null;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }

        public async Task<string> GetReceiptHtml(int transacId)
        {
            try
            {
                string url = YuranAPI.URL + $"viewfeereceipt/{transacId}";
                var res = await client.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
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

        public async Task<List<Fee>> GetFees(int oid)
        {
            try
            {
                string url = YuranAPI.APIURL + $"getYuran";
                var values = new Dictionary<string, string>
                {
                    {"oid", oid.ToString() },
                    {"user_id", user_id.ToString() }
                };

                var content = new FormUrlEncodedContent(values);
                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    var fees = JsonConvert.DeserializeObject<List<Fee>>(result);
                    return fees;
                }

                return null;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }

        public async Task<string> PayYuranHtml(List<string> id, List<string> category)
        {
            try
            {
                string url = YuranAPI.APIURL + $"yuranTransaction";
                
                var data = new
                {
                    user_id = user_id,
                    id = new List<string>(id),
                    category = new List<string>(category)
                };

                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
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
