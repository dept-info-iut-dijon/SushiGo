using Logic_Layer.cards;
using System.Configuration;
using System.Collections.Specialized;

namespace Logic_Layer.factories.utils;

public class GenerationParametersExtractor
{
    /// <summary>
    /// Extrait toutes les données de générations de la config
    /// </summary>
    /// <returns>Données de génération</returns>
    public Dictionary<string, int> GetParameters()
    {
        NameValueCollection parameters = ConfigurationManager.AppSettings;
        Dictionary<string, int> ret = new Dictionary<string, int>();
        foreach (string param in parameters)
        {
            ret.Add(param, Convert.ToInt32(parameters.Get(param)));
        }

        return ret;
    }
}