using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StagingConvert
{
    //This class allows you to call each method in the TMConversion class and
    //its corresponding label for the GUI. Honestly, i couldnt think of a better way to do it
    public class ConversionObject
    {
        public Label label { get; set; }
        public string method { get; set; }
        public int order { get; set; }
    }
}
