using CsvHelper;
using CsvHelper.Configuration;
using MusicStreamer;
using System.Globalization;

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = "\t"
};

var startDate = DateTime.Parse("10/08/2016 00:00:00");
var endDate = startDate.AddDays(1);
var result = new List<MusicLogTrendModel>();


using (var reader = new StreamReader("data.csv"))
using (var csv = new CsvReader(reader, config))
{
    var rawRecords = csv.GetRecords<MusicLogModel>();
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


    foreach (var item in trendResult)
    {
        result.Add(new MusicLogTrendModel()
        {
            ClientCount = item.clientCount,
            UniqeSongsPlayCount = item.uniqeSongsPlayCount
        });
    }

}

using (var writer = new StreamWriter("result.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(result);
}