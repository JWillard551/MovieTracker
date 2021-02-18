using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTracker.App.Models
{
    public class ItemTest
    {
        public string PhotoUrl
        {
            get
            {
                return "kingsman.jpg";
            }
        }

        public string TextValue { get; set; }

        public string Title { get; set; }

        public string ReleaseYear { get; set; }
    }
}
