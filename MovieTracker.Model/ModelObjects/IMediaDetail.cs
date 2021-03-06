using MovieTracker.Model.ModelEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Model.ModelObjects
{
    public interface IMediaDetail
    {
        string ID { get; set; }

        MediaType MediaType { get; set; }
    }
}
