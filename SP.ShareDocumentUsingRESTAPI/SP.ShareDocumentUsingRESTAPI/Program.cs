using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;

using Newtonsoft.Json;

using PnP.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SP.ShareDocumentUsingRESTAPI
{
    internal class Program
    {
        static string CertificatePath = @"";
        static string CertificatePassword = "@word1";
        static string TenantId = "";
        static string ClientId = "";
        static async Task Main(string[] args)
        {
            try
            {
                await ShareDocumentDemo();
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task ShareDocumentDemo()
        {
            string siteUrl = "https://contoso.sharepoint.com/sites/SharingDemo";
            int documentItemId = 1;
            string libraryTitle = "Documents";
            AuthenticationManager authManager = new AuthenticationManager(ClientId, CertificatePath, CertificatePassword, TenantId);
            string authToken = await authManager.GetAccessTokenAsync("https://contoso.sharepoint.com/.default");
            string requestDigest = await GetRequestDigest(authToken, siteUrl);

            if (!string.IsNullOrEmpty(requestDigest))
            {
                Console.WriteLine(requestDigest);
            }
            string shareLink = await ShareDocument(authToken, siteUrl, libraryTitle, requestDigest, documentItemId, new List<string>() { "vendor1@outlook.com" });
            Console.WriteLine(shareLink);




        }
        public static async Task<string> ShareDocument(string accessToken, string siteUrl, string libraryTitle, string requestDigest, int itemId, List<string> users)
        {
            string shareLink = string.Empty;

            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, siteUrl + string.Format("/_api/web/lists/getbytitle('{0}')/getitembyid({1})/sharelink", libraryTitle, itemId));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            List<PeoplePickerInputModel> userRequests = new List<PeoplePickerInputModel>();
            users.ForEach((user) =>
            {
                userRequests.Add(new PeoplePickerInputModel() { Key = user, IsResolved = false });
            });
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            message.Headers.Add("X-RequestDigest", requestDigest);

            ShareLinkRequestModel shareLinkRequestModel = new ShareLinkRequestModel()
            {
                request = new Request()
                {
                    createLink = true,
                    peoplePickerInput = JsonConvert.SerializeObject(userRequests),
                    settings = new Settings()
                    {
                        expiration = null,
                        linkKind = SharingLinkKind.Flexible,
                        password = "",
                        restrictShareMembership = true,
                        role = Microsoft.SharePoint.Client.Sharing.Role.Edit,
                        scope = Microsoft.SharePoint.Client.Sharing.SharingScope.SpecificPeople,
                        updatePassword = false
                    }
                }
            };
            string requestContent = JsonConvert.SerializeObject(shareLinkRequestModel);
            message.Content = new StringContent(requestContent, Encoding.Default, "application/json");
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ShareDocumentResponse shareDocumentResponse = JsonConvert.DeserializeObject<ShareDocumentResponse>(await response.Content.ReadAsStringAsync());
                shareLink = shareDocumentResponse.sharingLinkInfo.Url;
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
            }
            return shareLink;

        }
        public static async Task<string> GetRequestDigest(string accessToken, string siteUrl)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, siteUrl + "/_api/contextinfo");

            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            //message.Headers.Add("Content-Type", "application/json;odata=verbose");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //client.Headers.Add(HttpRequestHeader.Accept, "application/json;odata=verbose");
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                RequestDigestResponse requestDigestResponse = JsonConvert.DeserializeObject<RequestDigestResponse>(responseContent);

                return requestDigestResponse.FormDigestValue;
            }


            return "";

        }
    }
    public class RequestDigestResponse
    {
        public string FormDigestValue { get; set; }

    }



    public class ShareLinkRequestModel
    {
        public Request request { get; set; }
    }

    public class Request
    {
        public bool createLink { get; set; }
        public Settings settings { get; set; }
        public string peoplePickerInput { get; set; }
    }

    public class Settings
    {
        public SharingLinkKind linkKind { get; set; }
        public object expiration { get; set; }
        public Microsoft.SharePoint.Client.Sharing.Role role { get; set; }
        public bool restrictShareMembership { get; set; }
        public bool updatePassword { get; set; }
        public string password { get; set; }
        public Microsoft.SharePoint.Client.Sharing.SharingScope scope { get; set; }
    }

    public class PeoplePickerInputModel
    {
        public string Key { get; set; }
        public bool IsResolved { get; set; }
    }
}
