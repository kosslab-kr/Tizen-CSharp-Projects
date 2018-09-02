using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using Xamarin.Forms;
using static System.Console;

struct Result
{
    public int strike;
    public int ball;
};

namespace CrossTemplate3
{
    public class App : Application
    {
        Entry entry;
        Label label;
        Button button;
        int trial = 0;
        int[] target = new int[4];
        public App()
        {
            

            label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
            };
            entry = new Entry
            {
                MaxLength = 4,
                Keyboard = Keyboard.Numeric,
                Placeholder = "Enter Your numbers",

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                IsVisible = false
                

            };
            button = new Button
            {
                Text = "버튼을 누르면 게임이 시작됩니다",
                HorizontalOptions = LayoutOptions.Center,
            };

            entry.Completed += input_completed;
            button.Pressed += button_Pressed;





            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        label,
                        button,
                        entry,
                        

                    }
                }
            };
        }

        private void button_Pressed(object sender, EventArgs e)
        {
            trial = 0;
            label.Text = "";
            entry.IsVisible = true;
            button.IsVisible = false;
            makeTargetNumber();


            //string temp = "";
            //for (int i = 0; i < 4; i++)
            //{
            //    temp += target[i].ToString();
            //}
            //label.Text += "정답은 " + temp + " 입니다\n";


        }

        private void makeTargetNumber()
        {
            bool[] is_picked = new bool[10];
            for (int i = 0; i < 4; i++)
                target[i] = -1;
            for (int i = 0; i < 10; i++)
            {
                is_picked[i] = false;
            }
            for (int i = 0; i < target.Length; i++)
            {
                do {
                    Random r = new Random();

                    int temp = r.Next(0, 9);
                    if (is_picked[temp] == false)
                    {
                        target[i] = temp;
                        is_picked[temp] = true;
                    }
                }
                while (target[i]<0);
            }
        }

        async void input_completed(object sender, EventArgs e)
        {
            if (overlapchecked(entry))
            {
                // 에러처리
                await Application.Current.MainPage.DisplayAlert("경고", "두번이상 쓰인 숫자가 있습니다.", "다시입력해주세요");

            }
            else if (entry.Text.Length != 4)
            {
                // 에러처리
                await Application.Current.MainPage.DisplayAlert("경고", "네자리 수가 아닙니다.", "다시입력해주세요");

            }
            else
            {
                entry.IsEnabled = false;
                trial++;
                Result r = query(entry);
                label.Text += entry.Text + "\n";
                label.Text += "#" + trial + "번째 시도 " + "strike : " + r.strike + ", ball : " + r.ball + "\n";
                if (r.strike == 4 && r.ball ==0)
                {
                    label.Text += "정답입니다\n게임을 다시 진행하려면 아래 버튼을 눌러주세요";
                    entry.IsVisible = false;
                    button.IsVisible = true;
                }
                entry.IsEnabled = true;
                entry.Text = "";
                //entry.Text.Remove(0);
            }

        }

        Result query(Entry _entry)
        {
            Result temp = new Result();
            int[] target_cnt = new int[10];
            int[] guess = new int[4];

            for (int i = 0; i < 4; i++)
            {
                guess[i] = _entry.Text[i] - '0';
                target_cnt[target[i]]++;
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == target[i])
                    temp.strike++;
                else if (target_cnt[guess[i]] > 0)
                    temp.ball++;
            }

            return temp;
        }

        bool overlapchecked(Entry _entry)
        {

            int []num_cnt = new int[10];
            for (int i = 0; i < num_cnt.Length; i++)
                num_cnt[i] = 0;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {
                num_cnt[temp[i] - '0']+=1;
            }

            for (int i = 0; i < num_cnt.Length; i++)
            {
                if (num_cnt[i] >= 2)
                {
                    return true;
                }
            }

            return false; 
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
