using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Register.Model;
using System.Net.Http;

namespace Register
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void btnLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            Login log = new Login
            {
                usuario = txtUsuario.Text,
                pass = txtPass.Text
            };
            Uri RequestUri = new Uri("http://192.168.100.10:62512/api/usuarios");
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(log);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri,contentJson);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Mensaje",content,"ok");
                //await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Mensaje", "Datos incorrectos", "ok");
            }

        }

        async void BtnRegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPage());
        }
    }
}
