using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;

namespace WindowsFormsApp9
{

    public partial class Form1 : Form
    {

        public class WeatherData
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public Hourly hourly { get; set; }
        }
        public class Hourly
        {
            public List<string> time { get; set; }
            public List<double> temperature_2m { get; set; }

            public List<double> visibility { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        static async Task<string> GetDataFromUrlAsync(string url)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            return response;
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            string days = textBox1.Text;
            string json = await GetDataFromUrlAsync($"https://api.open-meteo.com/v1/forecast?latitude=46.3497&longitude=48.0408&hourly=temperature_2m,visibility&timezone=auto&forecast_days={days}");
            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
            

            update();
            for (int i = 0; i < weatherData.hourly.temperature_2m.Count; i++)
            {
                listBox1.Items.Add("----------------------------------------------------");
                if (checkBox1.Checked == true)
                {
                    listBox1.Items.Add($"Дата и время : {weatherData.hourly.time[i]} Температура: {weatherData.hourly.temperature_2m[i]} Видимость : {weatherData.hourly.visibility[i]}");
                }
                else
                {
                    listBox1.Items.Add($"Дата и время : {weatherData.hourly.time[i]} Температура: {weatherData.hourly.temperature_2m[i]}  ");
                }
                
            }
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void update()
        {
            listBox1.Items.Clear();
            for(int i = 0;i < listBox1.Items.Count;i++)
            {
                listBox1.Items.Add(i);
            }
        }
    }


}
