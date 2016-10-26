using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xbim.SiteBuilder
{
    internal class PageSettings
    {
        public bool UseContainer { get; set; } = true;
        public string Title { get; set; }
        public string Layout { get; set; }
    }
}
