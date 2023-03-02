using CsvHelper.Configuration.Attributes;

namespace MusicStreamer
{
    public class MusicLogTrendModel
    {
        [Name("DISTINCT_PLAY_COUNT")]
        public int UniqeSongsPlayCount { get; set; }

        [Name("CLIENT_COUNT")]
        public int ClientCount { get; set; }
    }
}
