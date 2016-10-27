using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xbim.SiteBuilder.Templates
{
    public partial class Layout
    {
        public string Content { get; set; }
        public bool UseContainer { get; set; } = true;
        public bool WithBanner { get; set; } = false;


        public ContentNode NavigationRoot { get; set; }
    }
}
