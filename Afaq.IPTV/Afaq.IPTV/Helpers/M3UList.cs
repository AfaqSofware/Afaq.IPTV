using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PlaylistSharpLib;

namespace Afaq.IPTV.Helpers
{
    internal class M3UPlaylist : IPlaylist
    {
        #region ctor

        public M3UPlaylist(string m3u)
        {
            var toto = from item in BuildM3U(m3u).TrackList
                let isEmpty = string.IsNullOrWhiteSpace(item.Location)
                where !isEmpty
                select item;
            M3UTracks = toto;
        }

        //public M3UPlaylist(IEnumerable<PlaylistSharpLib.PlaylistTrack> tracks)
        //{
        //    Tracks = from item in tracks
        //             where !String.IsNullOrWhiteSpace(item.Location)
        //             select item;
        //}

        #endregion

        public IEnumerable<PlaylistTrack> M3UTracks { get; set; }
        public IEnumerable<PlaylistSharpLib.PlaylistTrack> Tracks { get; set; }

        public override string ToString()
        {
            var s = new StringBuilder(string.Format("#EXTM3U{0}{0}",
                Environment.NewLine));
            //var tracks = FromBase(Tracks);
            //foreach (var playlistTrack in tracks)
            //    s.AppendFormat("#EXTINF:{0},{1}{2}{3}{2}",
            //        playlistTrack.Information.Details,
            //        playlistTrack.Information.Title,
            //        Environment.NewLine,
            //        playlistTrack.Location);
            return s.ToString();
        }

        #region cleaner

        private static string RemoveHeader(string source)
        {
            return source.Replace("#EXTM3U", string.Empty).Trim();
        }

        private static IEnumerable<string> RemoveComments(IEnumerable<string> lines)
        {
            return lines.Where(line => !line.StartsWith("#EXTREM:"));
        }

        private static IEnumerable<string> Clean(string m3u)
        {
            var str =
                RemoveComments(RemoveHeader(m3u)
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries));
            Debug.WriteLine(str);
            return str;
        }

        #endregion

        #region builder

        private static Information BuildInformation(string line, string location)
        {
            var result = new Information();

            var info = line.Replace("#EXTINF:", string.Empty);
            var index = info.IndexOf(',');
            var details = GetInformation(info.Substring(0, index));
            result.Id = GetStreamID(location);
            result.Group = details["group-title"];
            result.Logo = details["tvg-logo"];
            result.Title = details["tvg-name"];
            return result;
        }

        private static Dictionary<string, string> GetInformation(string str)
        {
            var bareString = str.Replace("\"", "");
            var commaSplit = bareString.Split(',').ToList();
            var strSpaceSplit = commaSplit[0].Split(' ');
            return strSpaceSplit.Where(s => s.Contains("="))
                .Select(s => s.Split('='))
                .ToDictionary(s => s[0], s => s[1]);
        }

        private static string GetStreamID(string str)
        {
            var result = "";
            Uri uri = new Uri(str);
            foreach (var segment in uri.Segments) {
                if (segment.Contains(".ts")) {
                    result= segment.Substring(0, segment.IndexOf(".ts", StringComparison.OrdinalIgnoreCase));
                }
            }
            return result;
        }

        private static PlaylistTrack BuildTrack(string information, string location)
        {
            return new PlaylistTrack
            {
                Information = BuildInformation(information, location),
                Location = location
            };
        }

        private static M3U BuildM3U(string m3u)
        {
            var lines = new List<string>(Clean(m3u));
            if (lines.Count%2 != 0)
                throw new FormatException("m3u");

            var tracks = new List<PlaylistTrack>();
            for (var i = 0; i < lines.Count; i += 2)
                tracks.Add(BuildTrack(lines[i], lines[i + 1]));

            return new M3U
            {
                Version = "#EXTM3U",
                TrackList = tracks.ToArray()
            };
        }

        #endregion

        #region converter

        private static IEnumerable<PlaylistSharpLib.PlaylistTrack> ToBase(IEnumerable<PlaylistTrack> xpsfList)
        {
            return from track in xpsfList
                select new PlaylistSharpLib.PlaylistTrack
                {
                    Title = track.Information.Title,
                    Location = track.Location
                };
        }

        private static IEnumerable<PlaylistTrack> FromBase(IEnumerable<PlaylistSharpLib.PlaylistTrack> playlist)
        {
            return from track in playlist
                select new PlaylistTrack
                {
                    Information = new Information
                    {
                        Details = "-1",
                        Title = track.Title
                    },
                    Location = track.Location
                };
        }

        #endregion
    }
}