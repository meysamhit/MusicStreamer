using CsvHelper;
using MusicStreamer;
using System.Globalization;

namespace TestMusicStreamer
{
    [TestFixture]
    public class CsvWriterTests
    {
        [Test]
        public void CsvWriter_WritesRecordsToStream()
        {
            var result = new List<MusicLogTrendModel>
            {
                new MusicLogTrendModel()
                {
                    ClientCount = 2,
                    UniqeSongsPlayCount = 1
                },
                new MusicLogTrendModel()
                {
                    ClientCount = 1,
                    UniqeSongsPlayCount = 3
                },
                new MusicLogTrendModel()
                {
                    ClientCount = 4,
                    UniqeSongsPlayCount = 3
                }
            };

            var tempFilePath = "test.csv";
            try
            {
                using (var writer = new StreamWriter(tempFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(result);
                }
                var lines = File.ReadAllLines(tempFilePath);

                Assert.AreEqual(4, lines.Count());
                Assert.AreEqual("DISTINCT_PLAY_COUNT,CLIENT_COUNT", lines[0]);
                Assert.AreEqual("1,2", lines[1]);
                Assert.AreEqual("3,1", lines[2]);
                Assert.AreEqual("3,4", lines[3]);
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
    }
}