using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex06_WinFormsAppDeadlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = GetStringAsync().Result; // 程式會當掉!
        }

        static HttpClient _httpClient = new HttpClient();

        private async Task<string> GetStringAsync()
        {
            return await _httpClient.GetStringAsync("https://www.google.com");
            // return await _httpClient.GetStringAsync("https://www.google.com").ConfigureAwait(false); // OK
        }

        private async void btnSolution1_Click(object sender, EventArgs e)
        {
            label1.Text = await GetStringAsync();
        }

        private void btnSolution2_Click(object sender, EventArgs e)
        {
            var uiContext = SynchronizationContext.Current;

            GetStringAsync().ContinueWith(task =>
            {
                uiContext.Post(delegate
                {
                    label1.Text = task.Result;
                }, null);
            });
        }

        private void btnSolution3_Click(object sender, EventArgs e)
        {
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = GetStringAsync();
            task.ContinueWith(t => label1.Text = t.Result, uiScheduler);
        }
    }
}
