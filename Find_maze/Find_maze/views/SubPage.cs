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
        double _angle;
        int startx = 0, starty = 0, endx = 0, endy = 0;        string strLeft = "LEFT", strRight = "Right";
        int[,] map = new int[25, 25]{
                                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                 {1,2,1,0,1,1,0,1,0,0,0,1,0,0,0,0,1,1,0,0,1,0,0,0,1},
                                 {1,0,1,0,0,0,0,1,0,1,0,1,0,0,1,0,0,0,0,1,1,0,1,0,1},
                                 {1,0,1,0,1,1,0,1,0,0,0,1,1,0,1,1,0,1,0,1,0,0,1,0,1},
                                 {1,0,1,0,0,0,0,1,1,0,1,1,0,0,0,1,1,1,0,1,0,1,1,0,1},
                                 {1,0,1,1,1,1,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,1,0,1},
                                 {1,0,0,0,0,0,0,1,0,1,0,1,0,1,1,0,0,1,0,1,1,1,1,0,1},
                                 {1,0,1,1,0,1,0,1,0,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,1},
                                 {1,0,1,0,0,1,0,1,0,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,1},
                                 {1,0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,0,1,0,0,0,0,1,0,1},
                                 {1,0,1,0,1,0,1,1,0,1,0,1,0,0,1,1,0,1,1,1,0,0,1,0,1},
                                 {1,0,1,0,0,0,1,0,0,1,0,0,0,0,0,1,0,1,0,1,0,0,1,0,1},
                                 {1,0,1,1,1,1,1,0,1,1,1,1,0,1,1,1,0,1,0,1,0,1,1,0,1},
                                 {1,0,0,0,0,0,1,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,1},
                                 {1,1,1,1,0,1,1,0,1,0,0,1,0,1,0,0,1,1,1,1,0,1,1,0,1},
                                 {1,0,0,1,0,0,0,0,1,0,0,1,0,1,1,0,0,1,0,0,1,0,0,0,1},
                                 {1,0,1,1,0,0,1,0,1,1,1,1,0,1,0,0,1,1,0,1,1,1,0,1,1},
                                 {1,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,1,0,1,0,1,0,1,1},
                                 {1,0,1,1,0,1,1,1,1,1,0,1,0,0,1,1,1,0,0,1,0,1,0,1,1},
                                 {1,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,1,0,0,0,1,0,0,0,1},
                                 {1,0,1,0,1,1,1,0,1,0,1,1,0,1,1,1,0,0,0,1,0,1,0,1,1},
                                 {1,0,1,0,0,0,0,0,1,0,0,1,0,0,0,1,1,1,0,1,0,1,0,0,1},
                                 {1,0,1,1,1,1,1,1,1,1,0,1,1,1,0,1,0,0,0,1,0,1,1,0,1},
                                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,1},
                                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                                };
        Label lblMaze = new Label { FontSize = 2.8, HorizontalOptions = LayoutOptions.Center };
        
        void setmap()
        {
            lblMaze.Text = "";
            int ch;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
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

        bool move(string ch)
        {
            switch (ch)
            {
                case "LEFT":
                    if (map[startx, starty - 1] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx, starty - 1] = 2;
                        startx--;
                    }
                    break;
                case "RIGHT":
                    if (map[startx, starty + 1] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx, starty + 1] = 2;
                        startx++;
                    }
                    break;
                case "UP":
                    if (map[startx - 1, starty] != 1)
                    {
                        map[startx, starty] = 0;
                        map[startx - 1, starty] = 2;
                        starty--;
                    }
                    break;
                case "DOWN":
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

            setmap();

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(10),
                RowDefinitions =
                {
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 300 },
                    new RowDefinition { Height = 30 }
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 30 },
                    new ColumnDefinition { Width = 265 },
                    new ColumnDefinition { Width = 30 }
                }
            };            Button btnLeft = new Button();            btnLeft.Image = "/home/owner/content/Left_small.png";            btnLeft.HorizontalOptions = LayoutOptions.FillAndExpand;            btnLeft.VerticalOptions = LayoutOptions.FillAndExpand;            Button btnRight = new Button();            btnRight.Image = "/home/owner/content/Right_small.png";            btnRight.HorizontalOptions = LayoutOptions.FillAndExpand;            btnRight.VerticalOptions = LayoutOptions.FillAndExpand;            

            Label label = new Label
            {
                Text = "Find Maze Game",
                FontSize = 5,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            grid.Children.Add(label, 0, 3, 0, 1);
            grid.Children.Add(btnLeft, 0, 1);
            grid.Children.Add(lblMaze, 1, 1);
            grid.Children.Add(btnRight, 2, 1);
            Content = grid;
            _angle = 0;

            btnLeft.Clicked += new EventHandler(left_Clicked);
            btnRight.Clicked += new EventHandler(right_Clicked);
        }


        public void Rotate(RotaryEventArgs args)
        {
            if (_rotating) return;

            _rotating = true;
            _angle += args.IsClockwise ? 30 : -30;
            lblMaze.RotateTo(_angle).ContinueWith((b) =>
            {
                _rotating = false;
                if (_angle == 90 || _angle == - 270)
                {
                    strLeft = "DOWN";
                    strRight = "UP";
                }
                else if (_angle == 180 || _angle == -180)
                {
                    strLeft = "RIGHT";
                    strRight = "LEFT";
                }
                else if (_angle == 270 || _angle == -90 )
                {
                    strLeft = "UP";
                    strRight = "DOWN";
                }
                else if (_angle == 0 || _angle == 360)
                {
                    strLeft = "LEFT";
                    strRight = "RIGHT";
                    lblMaze.Rotation = 0;
                    _angle = 0;
                }
            });
        }

        private void left_Clicked(object sender, EventArgs e)
        {
            if (move(strLeft))
            {
                InformationPopup popup = new InformationPopup();
                popup.Title = "CLEAR";
                popup.Text = "End the game";
                popup.Show();
            }
        }

        private void right_Clicked(object sender, EventArgs e)
        {
            if (move(strRight))
            {
                InformationPopup popup = new InformationPopup();
                popup.Title = "CLEAR";
                popup.Text = "End the game";
                popup.Show();
            }
        }
        
    }

    public class InformationPopup : BindableObject
    {
        /// <summary>
        /// BindableProperty. Identifies the IsProgressRunning bindable property.        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsProgressRunningProperty = BindableProperty.Create(nameof(IsProgressRunning), typeof(bool), typeof(InformationPopup), false);
        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InformationPopup), null);               
        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(InformationPopup), null);                
        /// <summary>
        /// BindableProperty. Identifies the first button bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BottomButtonProperty = BindableProperty.Create(nameof(BottomButton), typeof(MenuItem), typeof(InformationPopup), null);               
        InformationPopup _popUp;
        /// <summary>
        /// Occurs when the device's back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler BackButtonPressed;
        public InformationPopup()
        {
            _popUp = DependencyService.Get<InformationPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
            {
                throw new InvalidOperationException("Object reference not set to an instance of a Popup.");
            }

            _popUp.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };
            SetBinding(IsProgressRunningProperty, new Binding(nameof(IsProgressRunning), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(BottomButtonProperty, new Binding(nameof(BottomButton), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TitleProperty, new Binding(nameof(Title), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TextProperty, new Binding(nameof(Text), mode: BindingMode.OneWayToSource, source: _popUp));
        }
        /// <summary>
        /// Gets or sets progress visibility of the Popup.
        /// If this value is true. Popup displays circular progress and hides Title automatically.
        /// </summary>
        public bool IsProgressRunning
        {
            get { return (bool)GetValue(IsProgressRunningProperty); }
            set { SetValue(IsProgressRunningProperty, value); }
        }
        /// <summary>
        /// Gets or sets title of the Popup.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        /// <summary>
        /// Gets or sets text of the Popup.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        /// <summary>
        /// Gets or sets bottom button of the Popup.
        /// You should use only one property between Icon property and Text property because two area has the same position.
        /// </summary>
        public MenuItem BottomButton
        {
            get { return (MenuItem)GetValue(BottomButtonProperty); }
            set { SetValue(BottomButtonProperty, value); }
        }
        /// <summary>
        /// Shows the Popup.
        /// </summary>
        public void Show()
        {
            _popUp.Show();
        }               
        /// <summary>
        /// Dismisses the InformationPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()        {
            _popUp.Dismiss();
        }
    }
}