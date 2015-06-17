using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.WebAPI
{
    public class WidgetItem
    {
        //starting id is 7 as the list was pre-set to have 6 items already
        static int id = 7;
        public WidgetItem()
        {
            Id = id;
            //each time a new item is created it will take the next number available
            id++;
        }
        //getters and setters
        public int Id { get; set; }
        public string caption { get; set; }
        public string link { get; set; }
    }//end class
}
