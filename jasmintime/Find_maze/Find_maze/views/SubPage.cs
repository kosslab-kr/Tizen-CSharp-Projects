using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Find_maze.views
{
    public class SubPage : CirclePage, IRotaryEventReceiver
    {
        bool _rotating;
        int startx = 0, starty = 0, endx = 0, endy = 0;
        int iMap = 6, iStage;
        double _angle;

        int[,] map = new int[6, 6]
        {
            {1,1,1,1,1,1},
            {1,2,1,1,0,3},
            {1,0,0,0,0,1},
            {1,1,0,1,1,1},
            {1,1,0,0,0,1},
            {1,1,1,1,1,1}
        };

        Label label = new Label { FontSize = 10, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
        Label lblMaze = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

        void vMap(int i)
        {
            label.Text = i.ToString() + "단계";

            switch (i)
            {
                case 1:      
                    iMap = 6;
                    map = new int[6, 6] {
                                        {1,1,1,1,1,3},
                                        {1,2,1,1,0,0},
                                        {1,0,0,0,0,1},
                                        {1,1,0,1,1,1},
                                        {1,0,0,0,0,1},
                                        {1,1,1,1,0,1}
                                        };
                    lblMaze.FontSize = 10;                    
                    break;
                case 2:
                    iMap = 6;
                    map = new int[6, 6] {
                                        {1,1,1,1,0,1},
                                        {2,0,1,1,0,1},
                                        {1,0,0,0,0,1},
                                        {1,1,0,1,1,1},
                                        {0,0,0,0,0,1},
                                        {1,1,1,1,0,3}
                                        };
                    lblMaze.FontSize = 10;
                    break;
                case 3:
                    iMap = 8;
                    map = new int[8, 8] {
                                        {1,1,1,2,1,1,0,3},
                                        {0,0,1,0,0,1,0,1},
                                        {1,0,0,0,1,0,0,1},
                                        {1,1,0,1,1,0,1,1},
                                        {1,0,0,0,0,0,0,1},
                                        {1,0,1,1,0,1,0,1},
                                        {1,0,0,1,0,1,0,0},
                                        {1,1,1,1,0,1,1,1}
                                        };
                    lblMaze.FontSize = 8;
                    break;
                case 4:
                    iMap = 8;
                    map = new int[8, 8] {
                                        {1,1,1,1,0,1,1,1},
                                        {1,0,1,1,0,1,1,1},
                                        {0,0,0,0,0,0,0,0},
                                        {0,1,0,1,1,1,0,1},
                                        {0,1,0,0,0,1,0,1},
                                        {2,1,0,1,0,1,1,1},
                                        {0,1,0,1,0,0,0,0},
                                        {0,0,0,1,1,1,1,3}
                                        };
                    lblMaze.FontSize = 8;
                    break;
                case 5:
                    iMap = 8;
                    map = new int[8, 8] {
                                        {1,1,1,1,0,1,0,3},
                                        {0,0,1,0,0,1,1,0},
                                        {1,0,0,0,1,0,0,0},
                                        {1,1,0,1,1,0,1,1},
                                        {0,0,0,0,0,0,0,1},
                                        {0,1,0,1,0,1,0,1},
                                        {0,1,0,1,0,1,0,0},
                                        {2,1,0,0,0,1,1,1}
                                        };
                    lblMaze.FontSize = 8;
                    break;
                case 6:
                    iMap = 10;
                    map = new int[10, 10] {
                                        {1,1,1,1,0,0,0,0,1,3},
                                        {2,0,1,1,0,1,1,1,0,0},
                                        {1,0,0,0,0,0,1,1,0,1},
                                        {1,0,1,1,1,1,0,0,0,1},
                                        {1,0,0,0,0,1,0,1,1,1},
                                        {1,0,1,1,0,0,0,0,1,1},
                                        {1,1,1,1,0,1,1,0,1,1},
                                        {1,0,0,0,0,1,1,0,0,0},
                                        {0,0,1,1,0,1,1,1,1,1},
                                        {1,1,1,1,0,1,1,1,1,1}
                                        };
                    lblMaze.FontSize = 7;
                    break;
                case 7:
                    iMap = 10;
                    map = new int[10, 10] {
                                        {1,1,1,1,1,1,1,0,0,2},
                                        {1,0,1,1,0,0,0,0,1,1},
                                        {1,0,0,0,0,1,1,0,1,1},
                                        {1,1,0,1,1,1,0,0,1,1},
                                        {0,0,0,0,0,1,1,0,1,1},
                                        {1,0,1,1,0,1,1,1,1,1},
                                        {1,0,1,1,0,0,0,0,0,1},
                                        {1,0,1,1,0,1,1,1,1,1},
                                        {1,0,0,1,0,0,0,0,0,1},
                                        {1,1,0,1,1,1,1,1,0,3}
                                        };
                    lblMaze.FontSize = 7;
                    break;
                case 8:
                    iMap = 10;
                    map = new int[10, 10] {
                                        {1,1,1,1,1,0,1,1,1,1},
                                        {1,0,0,0,0,0,1,1,1,1},
                                        {1,0,1,1,2,1,3,0,0,1},
                                        {1,0,1,1,0,1,0,1,0,1},
                                        {1,0,0,0,1,1,1,0,0,1},
                                        {1,0,1,1,0,0,0,1,0,1},
                                        {1,0,1,1,0,1,0,1,0,1},
                                        {1,0,1,1,0,1,1,1,0,1},
                                        {1,0,0,0,0,0,0,0,0,0},
                                        {1,1,1,1,0,1,1,1,1,1}
                                        };
                    lblMaze.FontSize = 7;
                    break;
                case 9:
                    iMap = 10;
                    map = new int[10, 10] {
                                        {1,1,1,1,0,1,1,1,0,1},
                                        {1,2,1,1,0,1,1,1,0,1},
                                        {1,0,0,0,0,0,0,0,0,1},
                                        {1,1,0,1,1,0,1,1,0,1},
                                        {1,0,0,0,0,1,1,1,0,1},
                                        {1,0,1,1,0,1,0,1,0,1},
                                        {1,1,1,1,0,1,0,1,0,1},
                                        {1,0,0,0,0,1,0,0,0,1},
                                        {1,1,1,1,0,1,1,1,0,0},
                                        {1,1,1,1,1,1,1,1,1,3}
                                        };
                    lblMaze.FontSize = 7;
                    break;
                case 10:
                    iMap = 12;
                    map = new int[12, 12] {
                                        {1,1,1,1,1,1,1,1,0,0,0,2},
                                        {1,0,1,1,0,0,0,0,0,1,1,0},
                                        {1,0,0,0,0,1,0,1,1,1,0,0},
                                        {1,0,0,1,1,1,0,1,1,1,0,1},
                                        {1,0,0,0,0,1,0,1,0,0,0,1},
                                        {1,0,1,1,0,1,0,1,1,1,0,1},
                                        {1,0,1,1,0,1,0,0,0,1,0,1},
                                        {0,0,0,1,0,1,0,1,1,1,0,1},
                                        {1,1,0,1,0,1,0,0,0,1,0,0},
                                        {1,1,0,1,0,1,1,1,1,0,1,0},
                                        {1,0,0,1,0,0,0,0,0,0,0,1},
                                        {1,0,1,1,0,1,1,1,1,1,0,3}
                                        };
                    lblMaze.FontSize = 6;
                    break;
                default:
                    iStage = 1;
                    iMap = 6;
                    map = new int[6, 6] {
                                        {1,1,1,1,1,1},
                                        {1,2,1,1,0,3},
                                        {1,0,0,0,0,1},
                                        {1,1,0,1,1,1},
                                        {1,1,0,0,0,1},
                                        {1,1,1,1,1,1}
                                        };
                    lblMaze.FontSize = 10;
                    break;
            }

            setmap();
        }
        
        void setmap()
        {
            lblMaze.Text = "";
            int ch;
            for (int i = 0; i < iMap; i++)
            {
                for (int j = 0; j < iMap; j++)
                {
                    ch = map[i, j];
                    switch (ch)
                    {
                        case 0:
                            lblMaze.Text += "    ";
                            break;
                        case 1:
                            lblMaze.Text += "■";
                            break;
                        case 2:
                            lblMaze.Text += "◎";
                            startx = i;
                            starty = j;
                            break;
                        case 3:
                            lblMaze.Text += "★";
                            endx = i;
                            endy = j;
                            break;
                    }
                }
                lblMaze.Text += "\r\n";
            }
        }

        async System.Threading.Tasks.Task<bool> moveAsync(string ch)
        {
            switch (ch)
            {
                case "LEFT":
                    if (starty -1 < 0)
                    {
                        bool bFail = false;
                        bFail = await DisplayAlert("절벽에 떨어짐...", "게임을 다시 시작하시겠습니까?", "Yes", "No");
                        if (bFail)
                        {
                            vMap(iStage);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                    if (map[startx, starty - 1] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx, starty - 1] = 2;
                        startx--;
                    }
                    break;
                case "RIGHT":
                    if (starty + 1 >= iMap)
                    {
                        bool bFail = false;
                        bFail = await DisplayAlert("절벽에 떨어짐...", "게임을 다시 시작하시겠습니까?", "Yes", "No");
                        if (bFail)
                        {
                            vMap(iStage);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                    if (map[startx, starty + 1] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx, starty + 1] = 2;
                        startx++;
                    }
                    break;
                case "UP":
                    if (startx - 1 < 0)
                    {
                        bool bFail = false;
                        bFail = await DisplayAlert("절벽에 떨어짐...", "게임을 다시 시작하시겠습니까?", "Yes", "No");
                        if (bFail)
                        {
                            vMap(iStage);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                    if (map[startx - 1, starty] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx - 1, starty] = 2;
                        starty--;
                    }
                    break;
                case "DOWN":
                    if (startx + 1 >= iMap)
                    {
                        bool bFail = false;
                        bFail = await DisplayAlert("절벽에 떨어짐...", "게임을 다시 시작하시겠습니까?", "Yes", "No");
                        if (bFail)
                        {
                            vMap(iStage);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                    if (map[startx + 1, starty] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx + 1, starty] = 2;
                        starty++;
                    }
                    break;
            }

            setmap();

            

            if (map[startx, starty] == map[endx, endy])
            {
                bool answer = false;
                string s = string.Format("{0}단계 클리어!!", iStage.ToString());
                iStage = iStage + 1;
                if (iStage <= 10)
                {
                    answer = await DisplayAlert(s, "다음 단계로 넘어가시겠습니까?", "Yes", "No");
                }

                if (answer)
                {
                    vMap(iStage);
                    return false;
                }
                else
                {
                    iStage = iStage - 1;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public SubPage()
        {
            RotaryFocusObject = this;
            iStage = 1;
            vMap(iStage);
            //setmap();

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(1),
                RowDefinitions =
                {
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 300 },
                    new RowDefinition { Height = 30 }
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 230 },
                    new ColumnDefinition { Width = 60 }
                }
            };

            Grid leftGrid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(5),
                RowDefinitions =
                {
                    new RowDefinition {Height=50},
                    new RowDefinition{Height=75},
                    new RowDefinition{Height=75}
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition {Width=60}
                }
            };

            Grid rightGrid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(1),
                RowDefinitions =
                {
                    new RowDefinition {Height=50},
                    new RowDefinition{Height=75},
                    new RowDefinition{Height=75}
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition {Width=50}
                }
            };

            Button btnLeft = new Button();
            btnLeft.Image = "/home/owner/content/left.png";
            btnLeft.HorizontalOptions = LayoutOptions.FillAndExpand;
            btnLeft.VerticalOptions = LayoutOptions.FillAndExpand;

            Button btnRight = new Button();
            btnRight.Image = "/home/owner/content/right.png";
            btnRight.HorizontalOptions = LayoutOptions.FillAndExpand;
            btnRight.VerticalOptions = LayoutOptions.FillAndExpand;

            Button btnUp = new Button();
            btnUp.Image = "/home/owner/content/up.png";
            btnUp.HorizontalOptions = LayoutOptions.FillAndExpand;
            btnUp.VerticalOptions = LayoutOptions.FillAndExpand;

            Button btnDown = new Button();
            btnDown.Image = "/home/owner/content/down.png";
            btnDown.HorizontalOptions = LayoutOptions.FillAndExpand;
            btnDown.VerticalOptions = LayoutOptions.FillAndExpand;

            leftGrid.Children.Add(btnLeft, 0, 1);
            leftGrid.Children.Add(btnRight, 0, 2);

            rightGrid.Children.Add(btnUp, 0, 1);
            rightGrid.Children.Add(btnDown, 0, 2);

            grid.Children.Add(label, 0, 3, 0, 1);
            grid.Children.Add(leftGrid, 0, 1);
            grid.Children.Add(lblMaze, 1, 1);
            grid.Children.Add(rightGrid, 2, 1);

            Content = grid;
            _angle = 0;

            btnLeft.Clicked += new EventHandler(left_ClickedAsync);
            btnRight.Clicked += new EventHandler(right_ClickedAsync);
            btnUp.Clicked += new EventHandler(up_Clicked);
            btnDown.Clicked += new EventHandler(down_Clicked);
        }


        public void Rotate(RotaryEventArgs args)
        {
            if (_rotating) return;

            _rotating = true;
            _angle += args.IsClockwise ? 30 : -30;
            lblMaze.RotateTo(_angle).ContinueWith((b) =>
            {
                _rotating = false;
                //if (_angle == 90 || _angle == - 270)
                //{
                //}
                //else if (_angle == 180 || _angle == -180)
                //{
                //}
                //else if (_angle == 270 || _angle == -90 )
                //{
                //}
                //else 
                if (_angle == 360) //_angle == 0 || 
                {
                    //lblMaze.Rotation = 0;
                    _angle = 0;
                }
            });
        }

        private async void left_ClickedAsync(object sender, EventArgs e)
        {
            bool bMove = await moveAsync("LEFT");
            if (bMove)  //strLeft
            {
                bool bEnd = await DisplayAlert("THE END", "게임이 끝났습니다. 종료하시겠습니까?", "Yes", "No");
                if(bEnd)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    iStage = 1;
                    vMap(iStage);
                }
            }
        }

        private async void right_ClickedAsync(object sender, EventArgs e)
        {
            bool bMove = await moveAsync("RIGHT");
            if (bMove) //strRight
            {
                bool bEnd = await DisplayAlert("THE END", "게임이 끝났습니다. 종료하시겠습니까?", "Yes", "No");
                if (bEnd)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    iStage = 1;
                    vMap(iStage);
                }
            }
        }

        private async void up_Clicked(object sender, EventArgs e)
        {
            bool bMove = await moveAsync("UP");
            if (bMove)
            {
                bool bEnd = await DisplayAlert("THE END", "게임이 끝났습니다. 종료하시겠습니까?", "Yes", "No");
                if (bEnd)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    iStage = 1;
                    vMap(iStage);
                }
            }
        }

        private async void down_Clicked(object sender, EventArgs e)
        {
            bool bMove = await moveAsync("DOWN");
            if (bMove)
            {
                bool bEnd = await DisplayAlert("THE END", "게임이 끝났습니다. 종료하시겠습니까?", "Yes", "No");
                if (bEnd)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    iStage = 1;
                    vMap(iStage);
                }
            }
        }
    }
}