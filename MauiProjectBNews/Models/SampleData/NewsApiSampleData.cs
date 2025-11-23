using System.Reflection;
using System.Xml.Serialization;

namespace MauiProjectBNews.Models.SampleData
{
    public static class NewsApiSampleData
    {
        #region News Test Data as News Service only allows 100 calls per day
        static public async Task<NewsApiData> GetNewsApiSampleAsync(NewsCategory category)
        {
            Task<NewsApiData> t = Task.Run(() =>
            {
                NewsApiData n = Deserialize($"sample {category}.xml");
                return n;
            });

            return await t;

            static NewsApiData Deserialize(string fname)
            {
                var _locker = new object();
                lock (_locker)
                {
                    NewsApiData newsapi;

                    //How to access an embedded file resource
                    Type type = typeof(NewsApiSampleData);
                    var assembly = IntrospectionExtensions.GetTypeInfo(type).Assembly;
                    Stream stream = assembly.GetManifestResourceStream($"{type.Namespace}.{fname}");

                    //Deserialize the embedded resource
                    using var reader = new StreamReader(stream);
                    var xmls = new XmlSerializer(typeof(NewsApiData));
                    newsapi = (NewsApiData)xmls.Deserialize(reader);

                    return newsapi;
                }
            }
        }
        #endregion
    }

}
