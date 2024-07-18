using Newtonsoft.Json;

public class CodPuntuacionMatrizWrapper
{
    public string CodPuntuacionMatrizJson { get; set; }

    public List<short> CodPuntuacionMatriz
    {
        get => JsonConvert.DeserializeObject<List<short>>(CodPuntuacionMatrizJson);
        set => CodPuntuacionMatrizJson = JsonConvert.SerializeObject(value);
    }
}
