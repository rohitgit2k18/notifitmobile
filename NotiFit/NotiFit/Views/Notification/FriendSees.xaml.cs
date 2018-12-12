using Newtonsoft.Json;
using NotiFit.Helpers;
using NotiFit.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Notification
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendSees : ContentPage
	{
		public FriendSees ()
		{
			InitializeComponent ();
            getListofInvitedFrnd();
        }

        public async Task<NotificationFriend> getListofInvitedFrnd()
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            Common common = new Common();
            common.UserID = 1;
            NotificationFriend _response;
            string s = JsonConvert.SerializeObject(common);
            HttpResponseMessage response = null;
            string uri = "http://noti.fit:130/api/Friend/ListOfInvitedFriend";
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                response = await client.PostAsync(uri, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<NotificationFriend>(SucessResponse);
                    List<ListofFriend> unPaidList = new List<ListofFriend>();
                    foreach (var i in _response.Response.listofFriends)
                    {
                        if (i.PaymentDone > 0)
                        {
                            unPaidList.Add(i);
                        }
                        else
                        {
                            
                        }
                    }
                    await Navigation.PopAllPopupAsync();
                    lstView.ItemsSource = unPaidList;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<NotificationFriend>(ErrorResponse);
                }


                return _response;

            }
        }
    }
}