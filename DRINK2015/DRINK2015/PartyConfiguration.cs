using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DRINK2015.Domain;
using System.Text.RegularExpressions;
using Windows.Storage;

namespace DRINK2015
{
    public class PartyConfiguration
    {
        private HttpClient _client;

        private string _githubApiGist = "https://api.github.com/gists/9ef4478cf4bb9469ef1c";

        private string filename = "DRINK2015PartyList.json";

        public Updated PartyListUpdated;

        public delegate void Updated(PartyListEventArgs e);

        public PartyConfiguration()
        {
            _client = new HttpClient();
        }

        public async Task<List<PartyDetail>> GetUpdatedPartyList()
        {
            try
            {
                var rawJson = await _client.GetStringAsync(_githubApiGist);
                var githubResponse = JsonConvert.DeserializeObject<Example>(rawJson);
                var content = githubResponse.files.DRINK2015PartyList.content;
                var partyList = JsonConvert.DeserializeObject<List<PartyDetail>>(content);

                await SavePartyList(partyList);
                PartyListUpdated(new PartyListEventArgs { PartyList = partyList });
                return partyList;
            }
            catch(Exception ex)
            {
                var t = ex;
            }
            return new List<PartyDetail>();
        }

        public async Task SavePartyList(List<PartyDetail> partyList) 
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            var text = JsonConvert.SerializeObject(partyList);
            await FileIO.WriteTextAsync(file, text);
        }

        public async Task<List<PartyDetail>> GetPartyListFromStorage()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            var read = await FileIO.ReadTextAsync(file);
            var items = JsonConvert.DeserializeObject<List<PartyDetail>>(read);
            return items;
        }
    }

    public class PartyListEventArgs : EventArgs
    {
        public List<PartyDetail> PartyList { get; set; }
    }
}
