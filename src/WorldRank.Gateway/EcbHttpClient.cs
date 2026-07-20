using System.Xml.Serialization;
using WorldRank.Application.DTOs;
using WorldRank.Application.HttpClients;

namespace WorldRank.Gateway;

public class EcbHttpClient : IEcbHttpClient
{
    private readonly HttpClient _httpClient;

    public EcbHttpClient(HttpClient httpClient)   // ← το factory το περνάει
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<CurrencyRateDto>> GetLatestRatesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("stats/eurofxref/eurofxref-daily.xml", cancellationToken);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var serializer = new XmlSerializer(typeof(Envelope));
        var envelope = (Envelope?)serializer.Deserialize(stream);

        if (envelope?.Cube?.Cube1?.Cube is null)
            throw new InvalidOperationException("ECB response could not be parsed or contained no rates.");
        DateTime date = envelope.Cube.Cube1.time;
        return envelope.Cube.Cube1.Cube
            .Select(c => new CurrencyRateDto(c.currency, c.rate, date))
            .ToList();
    }
}