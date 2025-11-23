using MauiProjectBNews.Services;
using Microsoft.Maui.Controls;
using System.Linq;

namespace MauiProjectBNews
{
    public partial class MainPage : ContentPage
    {
        private NewsService _service;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            _service = new NewsService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var news = await _service.GetNewsAsync(MauiProjectBNews.Models.NewsCategory.business);
                ServiceLabel.Text = $"{news.Articles.Count} news article read.";   
            }
            catch (Exception ex)
            {
                ServiceLabel.Text = $"Error reading news: {ex.Message}";
            }
        }
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
