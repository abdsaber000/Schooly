using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace Ayadty.Data;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly IDistributedCache _cache;
    private readonly JsonSerializer _serializer = new ();
    public JsonStringLocalizer(IDistributedCache cache)
    {
        _cache = cache;
    }
    public LocalizedString this[string name]
    {
        get
        {
            var value = GetString(name);
            return new LocalizedString(name, value);
        }
    }
   public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var actualValue = this[name];
            if (!actualValue.ResourceNotFound)
                return new LocalizedString(name, string.Format(actualValue.Value, arguments));
            else return actualValue;
        }
    }
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var filepath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        using FileStream stream = new (filepath, FileMode.Open, FileAccess.Read , FileShare.Read);
        using StreamReader StreamReader = new (stream);
        using JsonTextReader reader = new (StreamReader);
        while (reader.Read())
        {
            if (reader.TokenType != JsonToken.PropertyName)
                continue;

            var key = reader.Value as string;
            var value = _serializer.Deserialize<string>(reader);
            yield return new LocalizedString(key, value);
        }
    }
        private string GetString(string key)
        {
            // Resources/en-US.json 
            // Resources/ar-EG.json ==>> i need to make this path but i use two lang
            // so i will use thread to know the current using lang
            var filepath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullPath = Path.GetFullPath(filepath);
            if (File.Exists(fullPath))
            {
                var casheKey = $"local_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
                var casheValue = _cache.GetString(casheKey);
                
                if (!string.IsNullOrEmpty(casheValue))
                    return casheValue;  // now found in cache so
                                        // he should not go to get it from json file
                                        // it is improve preformance
                    
               var result = GetValueFromJson(key, fullPath);
               if (!string.IsNullOrEmpty(result))
                   _cache.SetString(casheKey , result); // now i store the key 
                                                        // in cashe to use it in next        
                return result;
            }
            return string.Empty;
        }
        private string GetValueFromJson(string propartyName, string filePath)
        {
            if (string.IsNullOrEmpty(propartyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;
            using FileStream stream = new (filePath, FileMode.Open, FileAccess.Read , FileShare.Read);
            using StreamReader StreamReader = new (stream);
            using JsonTextReader reader = new (StreamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propartyName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }
            return string.Empty;
        }
}