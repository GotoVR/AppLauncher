using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlc.DotNet.Core.Interops.Signatures;

namespace AppLauncher.Models
{
    public class ProductsDetails
    {
        public int ID { get; set; }

        public int ProductsID { get; set; }

        public string ProductsDetailsName { get; set; }

        public string ExePath { get; set; }
    }
}
