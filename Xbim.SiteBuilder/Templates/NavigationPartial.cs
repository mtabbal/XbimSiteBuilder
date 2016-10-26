using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xbim.SiteBuilder.Templates
{
    public partial class Navigation
    {
        private bool _isRoot;
        private ContentNode _node;
        private IEnumerable<ContentNode> Children { get { return _node.Children.Where(c => !c.Name.StartsWith("index", StringComparison.InvariantCultureIgnoreCase)); } }
        public Navigation(ContentNode node, bool isRoot)
        {
            _node = node;
            _isRoot = isRoot;
        }
    }
}
