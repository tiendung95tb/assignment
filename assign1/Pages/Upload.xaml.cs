using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace assign1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Upload : Page
    {
        string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/post-free";
        public Upload()
        {
            this.InitializeComponent();
        }
        public bool validate(string name, string thumbnail, string link)
        {
            if (name.Equals(""))
            {
                this.name_validate.Text = "Name not be blank";
                return false;
            }
            if (name.Length >= 50)
            {
                this.name_validate.Text = "Name is max 50 characters";
                return false;
            }

            if (thumbnail.Equals(""))
            {
                this.thumbnail_validate.Text = "Name not be blank";
                return false;
            }

            if (link.Equals(""))
            {
                this.name_validate.Text = "Link not be blank";
                return false;
            }
         
                return true;
        }
        private void ButtonUpload_OnClick(object sender, RoutedEventArgs e)
        {
           if(!validate(this.name.Text, this.thumbnail.Text, this.link.Text))
            {
                return;
            }
            var Music = new Music()
            {
                name = this.name.Text,
                description = this.description.Text,
                singer = this.singer.Text,
                author = this.author.Text,
                thumbnail = this.thumbnail.Text,
                link = this.link.Text,
            };
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(Music), Encoding.UTF8,
                "application/json");
            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(ApiUrl, content);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
        }
        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            this.name.Text = string.Empty;
            this.description.Text = string.Empty;
            this.singer.Text = string.Empty;
            this.author.Text = string.Empty;
            this.thumbnail.Text = string.Empty;
            this.link.Text = string.Empty;
        }
    }
}
