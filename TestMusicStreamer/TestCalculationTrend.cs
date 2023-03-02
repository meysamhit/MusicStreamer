using MusicStreamer;

namespace TestMusicStreamer
{
    [TestFixture]
    public class MusicLogModelTests
    {
        [Test]
        public void TestTrendAnalysis()
        {
            // arrange
            var startDate = new DateTime(2016, 8, 10, 0, 0, 0);
            var endDate = new DateTime(2016, 9, 10, 0, 0, 0);
            var rawRecords = new List<MusicLogModel>
        {
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 1, ClientId = 1, PlayTs = startDate },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 2, ClientId = 2, PlayTs = startDate.AddMinutes(50) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 1, ClientId = 1, PlayTs = startDate.AddMinutes(40) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 2, ClientId = 2, PlayTs = startDate.AddMinutes(90) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 3, ClientId = 1, PlayTs = startDate.AddMinutes(100) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 3, ClientId = 2, PlayTs = startDate.AddMinutes(120) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 5, ClientId = 3, PlayTs = startDate.AddMinutes(10) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 5, ClientId = 3, PlayTs = startDate.AddDays(1) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 5, ClientId = 3, PlayTs = startDate.AddDays(-1) },
            new MusicLogModel { PlayId = "44BB190BC2493964E053CF0A000AB546", SongId = 5, ClientId = 3, PlayTs = startDate.AddDays(2) },
        };

            var targetRecords = rawRecords.Where(w => w.PlayTs >= startDate && w.PlayTs < endDate).ToList();
            var trendResult = targetRecords.GroupBy(g => g.ClientId)
                .Select(s => new
                {
                    clientId = s.Key,
                    uniqeSongsPlays = s.DistinctBy(d => d.SongId).Count()
                })
                .GroupBy(g => g.uniqeSongsPlays)
                .Select(s => new
                {
                    uniqeSongsPlayCount = s.Key,
                    clientCount = s.DistinctBy(d => d.clientId).Count()
                });

            Assert.AreEqual(2, trendResult.Count());

            var result1 = trendResult.FirstOrDefault(r => r.uniqeSongsPlayCount == 2);
            Assert.IsNotNull(result1);
            Assert.AreEqual(2, result1.clientCount);


            var result2 = trendResult.FirstOrDefault(r => r.uniqeSongsPlayCount == 1);
            Assert.IsNotNull(result2);
            Assert.AreEqual(1, result2.clientCount);

        }
    }
}
