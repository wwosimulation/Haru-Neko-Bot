using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using Neko_Test.Modules.services;
using Neko_Test.Responses;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neko_Test.Modules
{
    public class NekoCommands : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();//GetSfwAsync()
        [Command("neko")]
        public async Task Neko()
        {
            //var neko = await NekoServices.GetNekoImage();
            var embed = new EmbedBuilder();
            var http = new HttpClient();
            {
                /*var patRequest2 = await http.GetStreamAsync("https://nekos.life/api/neko");
                var patRequest = await http.GetAsync("https://nekos.life/api/neko");
                //var neko = patRequest.ToString();
                //https://cdn.nekos.life/neko/neko_315.jpgl
                var patImage = JsonConvert.DeserializeObject<NekoServices.WeebServices>(await patRequest.Content.ReadAsStringAsync());

                string json = JsonConvert.SerializeObject(patRequest);

                NekoServices.WeebServices deserializedProduct = JsonConvert.DeserializeObject<NekoServices.WeebServices>(json);

                var some = deserializedProduct.Url;*/

                //var c = NekosClient.GetSfwAsync2("wallpaper");
                //var neko = NekosImage;
                //var patRequest = await http.GetAsync($"{NekosClient.GetSfwAsync()}");
                //var rdimage = rnd.Next(299, 400);

                /*HttpClient httpClient = new HttpClient();
                var HostUrl = "https://nekos.life/api/v2";
                var HostUrl2 = "https://nekos.life/api/neko";
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{HostUrl2}");
                HttpResponseMessage res = await httpClient.SendAsync(req);
                string response = await res.Content.ReadAsStringAsync();

                //var neko = JsonConvert.DeserializeObject<NekosImage>(response);
                var alpha = JsonConvert.DeserializeObject(response);*/
                var patRequest = await http.GetAsync($"https://nekos.life/api/neko");
                string response = await patRequest.Content.ReadAsStringAsync();
                var another = JsonConvert.DeserializeObject<NekosImage>(response);

                embed.WithTitle("Neko <3");
                embed.WithImageUrl($"{another.neko}");
                embed.WithFooter("Powered by nekos.life");
                await Context.Channel.SendMessageAsync("", embed: embed.Build());
            }
        }

        [Command("neko2")]
        public async Task Neko2()
        {
            //var neko = await NekoServices.GetNekoImage();
            var embed = new EmbedBuilder();
            var http = new HttpClient();
            {
                var another = NekosClient.GetSfwAsync();
                embed.WithTitle("Neko <3");
                embed.WithImageUrl($"{another}");
                embed.WithFooter("Powered by nekos.life");
                await Context.Channel.SendMessageAsync("", embed: embed.Build());
            }
        }
    }
}
