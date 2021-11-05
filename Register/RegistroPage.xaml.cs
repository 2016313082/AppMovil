using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Register.Model;
using Xamarin.Forms;

namespace Register
{
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
        }

        async void btnVolver_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void btnRegistrar_Clicked(object sender, System.EventArgs e)
        {
            Login registro = new Login
            {
                usuario = txtRegistroUsuario.Text,
                pass = txtRegistroPass.Text
            };
            Uri RequestUri = new Uri("http://192.168.100.10:62512/api/usuarios/Agregar");
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(registro);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject(content);
                await DisplayAlert("Mensaje", content, "ok");
            }
            else
            {
                await DisplayAlert("Mensaje", "No se pudo", "ok");
            }
        }
    }
}
