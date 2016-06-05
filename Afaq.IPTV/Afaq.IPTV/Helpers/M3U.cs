
namespace Afaq.IPTV.Helpers
{
    public class M3U
    {
        public PlaylistTrack[] TrackList { get; set; }
        public string Version { get; set; }
    }

    public class PlaylistTrack
    {
        public Information Information { get; set; }
        public string Location { get; set; }
    }

    public class Information
    {
        public string Details { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Group { get; set; }
        public string Logo { get; set; }
    }
}