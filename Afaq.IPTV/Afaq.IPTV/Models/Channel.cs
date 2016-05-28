using System;
using System.Collections.Generic;

namespace Afaq.IPTV.Models
{
    public class Channel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<Source> Sources { get; set; }
        public Uri Logo { get; set; }
        public Source CurrentSource { get; set; }

    }
}
