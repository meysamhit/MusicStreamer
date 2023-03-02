using CsvHelper;
using CsvHelper.Configuration;
using MusicStreamer;
using System.Globalization;

namespace TestMusicStreamer
{
    [TestFixture]
    public class TestReadingCSV
    {
        [Test]
        public void TestCsvReading()
        {
            // ARNG
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t"
            };

            var csvContent = "PLAY_ID\tSONG_ID\tCLIENT_ID\tPLAY_TS\r\n44BB190BC2493964E053CF0A000AB546\t6164\t249\t09/08/2016 09:16:34\r\n44BB190BC24A3964E053CF0A000AB546\t544\t86\t10/08/2016 13:54:52";

            var csvFile = Path.Combine(Path.GetTempPath(), "test.csv");
            File.WriteAllText(csvFile, csvContent);

            // ACT
            using (var reader = new StreamReader(csvFile))
            using (var csv = new CsvReader(reader, config))
            {
                var result = csv.GetRecords<MusicLogModel>().ToList();

                var record1 = result.ElementAt(0);
                Assert.AreEqual("44BB190BC2493964E053CF0A000AB546", record1.PlayId);
                Assert.AreEqual(6164, record1.SongId);
                Assert.AreEqual(249, record1.ClientId);
                Assert.AreEqual(new DateTime(2016, 9, 8, 9, 16, 34), record1.PlayTs);

                var record2 = result.ElementAt(1);
                Assert.AreEqual("44BB190BC24A3964E053CF0A000AB546", record2.PlayId);
                Assert.AreEqual(544, record2.SongId);
                Assert.AreEqual(86, record2.ClientId);
                Assert.AreEqual(new DateTime(2016, 10, 8, 13, 54, 52), record2.PlayTs);
            }
        }
    }
}
