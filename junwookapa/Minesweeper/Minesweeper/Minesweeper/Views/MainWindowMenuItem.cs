using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Views
{

    public class MainWindowMenuItem
    {
        public MainWindowMenuItem()
        {
            TargetType = typeof(MainWindowDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}