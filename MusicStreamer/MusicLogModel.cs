using CsvHelper.Configuration.Attributes;

namespace MusicStreamer
{
    public class MusicLogModel
    {
        [Name("PLAY_ID")]
        public string PlayId { get; set; } = string.Empty;

        [Name("SONG_ID")]
        public int SongId { get; set; }

        [Name("CLIENT_ID")]
        public int ClientId { get; set; }

        [Name("PLAY_TS")]
        public DateTime PlayTs { get; set; }

    }
}
