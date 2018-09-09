using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

struct Result
{
    public int strike;
    public int ball;
}


namespace Baseball_with_AI
{
    public class App : Application
    {
        Entry entry;
        Label label;
        Button button;
        int trial = 0;
        int[] answer;
        int[] guess;
        int[,] numberList = new int[5040, 5];

        public App()
        {
            label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start

            };

            entry = new Entry
            {
                MaxLength = 4,
                Keyboard = Keyboard.Numeric,
                Placeholder = "Enter Your Answer",

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
            button.Pressed += button_pressed;

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

        private async void input_completed(object sender, EventArgs e)
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
                answer = new int[4];
                string temp = entry.Text;
                for (int i = 0; i < 4; i++)
                {
                    answer[i] = temp[i] - '0';
                }
                
                while (true)
                {
                    guess = thinknumber();
                    trial++;

                    string str = "";
                    for (int i = 0; i < 4; i++)
                    {
                        str += guess[i].ToString();
                    }
                    label.Text += "컴퓨터의 "+trial+"번째 선택 : " + str + "\n";
                    
                    
                    Result r = query();
                    //Result r;
                    //r.strike = 4;
                    //r.ball = 0;
                    
                    label.Text += r.strike.ToString() +"strike "+ r.ball.ToString()+"ball\n";

                    if (r.strike == 4 && r.ball == 0)
                    {
                        label.Text += trial + "번째 시도끝에 컴퓨터가 정답을 맞췄습니다.";
                        break;
                    }
                    else
                        removeNumbers(r);

                }
                button.IsVisible = true;
                entry.IsEnabled = true;
                entry.Text = "";
                entry.IsVisible = false;
            }
        }

        private void removeNumbers(Result _r)
        {
 
            for (int i = 0; i < 5040; i++)
            {
                Result temp = new Result();
                temp.strike = 0;
                temp.ball = 0;
                if (numberList[i, 4] == -1)
                    continue;
                else
                {
                    int[] target_cnt = new int[10];

                    for (int j = 0; j < 4; j++)
                    {
                        target_cnt[numberList[i, j]]++;
                    }

                    for (int k = 0; k < 4; k++)
                    {
                        if (guess[k] == numberList[i, k])
                            temp.strike++;
                        else if (target_cnt[guess[k]] > 0)
                            temp.ball++;

                    }
                }

                if (temp.strike == _r.strike && temp.ball == _r.ball)
                {
                    numberList[i, 4] = 0;
                }
                else
                    numberList[i, 4] = -1;
            }


        }

        private Result query()
        {
            Result temp = new Result();
            int[] target_cnt = new int[10];

            for (int i = 0; i < 4; i++)
            {
                target_cnt[answer[i]]++;
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == answer[i])
                    temp.strike++;
                else if (target_cnt[guess[i]] > 0)
                    temp.ball++;
            }

            return temp;
        }

        private int[] thinknumber()
        {
            int[] temp = new int[4];
            for (int i = 0; i < 5040; i++)
            {

                if (numberList[i, 4] == 0)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp[j] = numberList[i, j];
                    }
                    break;
                }
                else
                    continue;
                
            }
            return temp;
        }


        //잘작동함
        private void button_pressed(object sender, EventArgs e)
        {
            trial = 0;
            label.Text = "";
            entry.IsVisible = true;
            button.IsVisible = false;

            makeNumberList();
        }

        //잘작동함
        private void makeNumberList()
        {
            int index = 0;
          
            for (int i = 123; i <= 9876; i++)
            {
                bool overlap = false;
                int[] Numbercheck = new int[10];
                int num = i;
                int last = 3;
                if (i < 1000)
                    Numbercheck[0]++;
                while (num > 0)
                {
                    if (Numbercheck[num % 10] > 0)
                    {
                        overlap = true;
                        break;
                    }
                    Numbercheck[num % 10]++;
                    numberList[index, last--] = num % 10;
                    num /= 10;
                }
                if (overlap)
                    continue;
                else
                {
                    numberList[index, 4] = 0;
                    index++;
                }

            }
        }

        //제대로 작동함
        bool overlapchecked(Entry _entry)
        {

            int[] num_cnt = new int[10];
            for (int i = 0; i < num_cnt.Length; i++)
                num_cnt[i] = 0;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {
                num_cnt[temp[i] - '0'] += 1;
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
