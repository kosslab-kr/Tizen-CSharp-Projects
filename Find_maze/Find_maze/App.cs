using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace Find_maze
{
    public class App : Application
    {
        public App()
        {
            Label label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "미로찾기를 시작합니다."
            };

            Button button =
                        new Button
                        {
                            Text = "TOUCH",
                            FontSize = 7,
                            WidthRequest = 110,
                            HorizontalOptions = LayoutOptions.Center
                        };
            button.Clicked += new EventHandler(button_Clicked);

            // The root page of your application
            MainPage = new CirclePage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        label, button
                    }
                }
            };
            
            
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            MainPage = new views.SubPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
