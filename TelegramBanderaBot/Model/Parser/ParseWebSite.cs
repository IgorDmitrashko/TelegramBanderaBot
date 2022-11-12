using CsQuery;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TelegramBanderaBot.Parser.ModelParseWebSite.Clasess;
using TelegramBanderaBot.Parser.ModelParseWebSite.Interface;


namespace TelegramBot_Bandera228Bot.Parser
{
    class ParseWebSite
    {
        private readonly List<string> html = new List<string>();
        private readonly string _httpxmlHoroscope = "https://ignio.com/r/export/utf/xml/daily/com.xml";

        private List<string> ParseSite(IParseWebSite parseWebSite, List<string> html) {

            CQ dom = CQ.CreateFromUrl(parseWebSite.UrlSite);
            foreach(IDomObject obj in dom.Find(parseWebSite.PropForSearch))
            {
                if(obj.TextContent!="")
                {
                    html.Add(obj.TextContent);
                }
            }
            return html;
        }

        public string GetContentHoroscope(string horoscope) {
            XDocument xml = new();
            try
            {
                xml=XDocument.Load(_httpxmlHoroscope);
                return (string?)xml.Root?.Element(horoscope)?.Element("today");
            }
            catch(Exception)
            {

                return "erorr XML horoscope";
            }
        }

        public string GetJsonStringFromUrlSite(IUrlSite urlSite, int numberOfParticipants = 1) {
            WebClient? client = new WebClient();
            if(urlSite.GetType()==typeof(INumberOFParticipants))
            {
                return client.DownloadString(urlSite.UrlSite+=numberOfParticipants.ToString());

            }
            return client.DownloadString(urlSite.UrlSite);
        }

        public string GetContentWhatWantToDo(INumberOFParticipants urlSiteExchangeRate) {
            string json = "";
            try
            {
                WhatWantToDoJSon? whatWantToDoJSon = JsonConvert.DeserializeObject<WhatWantToDoJSon>(GetJsonStringFromUrlSite(urlSiteExchangeRate, 2));
                return whatWantToDoJSon.Activity;
            }
            catch(Exception e)
            {
                json=e.Message;
                return json;
            }
            return json;
        }

        public List<ExchangeRateJson> GetContentExchangeRate(IUrlSite urlSiteExchangeRate) {
            List<ExchangeRateJson> exchangeRates = new();

            try
            {
                exchangeRates=DeserializeJson(urlSiteExchangeRate);
                foreach(var item in DeserializeJson(urlSiteExchangeRate))
                {
                    if(item.Ccy=="USD") exchangeRates.Add(item);
                    else if(item.Ccy=="EUR") exchangeRates.Add(item);
                }
            }
            catch(Exception e)
            {
                exchangeRates.Add(new ExchangeRateJson { Ccy=$"error {e.Message}" });
                return exchangeRates;
            }

            return exchangeRates;
        }

        private List<ExchangeRateJson> DeserializeJson(IUrlSite urlSiteExchangeRate) {
            return JsonConvert.DeserializeObject<List<ExchangeRateJson>>(GetJsonStringFromUrlSite(urlSiteExchangeRate));
        }

        public string GetContentMinFin(IParseWebSite webSite) {
            try
            {
                var replacement = ParseSite(webSite, html)[0];
                html.Clear();
                return new Regex(@"(\W+)([0-9\s(\)\+]+)([0-9\(\)]+)").Replace(replacement, "$1$2$3\n");
            }
            catch(Exception e)
            {

                return $"erorr: {e.Message}";
            }
        }

        public string GetContentWaterTemp(IParseWebSite webSiteWaterTemp) {

            try
            {
                var water = "";
                var replacement = ParseSite(webSiteWaterTemp, html);
                foreach(var item in replacement)
                {
                    water=$"{water}{item}";
                }
                html.Clear();
                water=new Regex(@"([а-я])(\d)").Replace(water, "$1 $2");
                water=new Regex(@"(\d+°C)(\D\w+)").Replace(water, "$1\n$2");
                return water;
            }
            catch(Exception e)
            {
                return $"erorr: {e.Message}";
            }
        }
    }
}
