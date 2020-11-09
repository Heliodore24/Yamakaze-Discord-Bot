﻿using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using SauceNET;
using SauceNET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using YamakazeDiscordBot.Modules.ClasseUtiles;
using YamakazeDiscordBot.Modules.ClasseUtiles.Metiers;
using YamakazeDiscordBot.Services;

namespace YamakazeDiscordBot.Modules
{
    public class VideoImage : ModuleBase<SocketCommandContext>
    {
        private string _urlhololive = "https://api.homoliver.live/api/hololive/v1/allvideos?limit=1&offset=";
        private Color _color = new Color(39, 198, 220);

        //Ne fonctionne plus car l'api utiliser n'existe plus
        [Command("hololive")]
        [Summary("Get a random video from hololive")]
        public async Task Hololive()
        {
            EmbedBuilder embedBuilderLoading = new EmbedBuilder()
                .WithTitle("Searching a random video...")
                .WithImageUrl("https://thumbs.gfycat.com/LittleBestAmoeba-max-14mb.gif")
                .WithColor(_color);
            Embed embedloading = embedBuilderLoading.Build();
            var message = await ReplyAsync(embed: embedloading);
            //Recherche de la vidéo
            bool sucess = true;
            Random rnd = new Random();
            Requethttp req = new Requethttp();
            DataJsonHololive hololive = await req.GetDataJsonHololive(_urlhololive + rnd.Next(1, 8103));
            EmbedBuilder embedBuilderfinal = new EmbedBuilder().WithCurrentTimestamp();
            if (hololive == null)
            {
                embedBuilderfinal.WithTitle("Command error")
                    .WithDescription("The API doesn't response succesfuly.");
                sucess = false;
            }
            else
            {
                VideoHololive vid = hololive.Result[0];
                string url = "https://www.youtube.com/watch?" + vid.VideoId;
                string vidid = vid.VideoId;
                embedBuilderfinal.WithTitle("Random Hololive video")
                    .AddField(vid.VideoName, vid.Vtuber + "\n" + url)
                    .WithUrl(url)
                    .WithColor(_color)
                    .WithImageUrl($"https://img.youtube.com/vi/{vidid}/hqdefault.jpg");
            }
            Embed embed = embedBuilderfinal.Build();
            await message.ModifyAsync(msg => msg.Embed = embed);
            //await ReplyAsync(embed: embed);
            Program.log.WriteCommandToConsole(Context.User.ToString(), "hololive"+" Réussi : "+sucess);
        }

        [Command("sauce")]
        [Summary("Search the sauce of a image")]
        public async Task Sauce(string link)
        {
            EmbedBuilder embedBuilderLoading = new EmbedBuilder()
                .WithTitle("Searching sauce...")
                .WithImageUrl("https://thumbs.gfycat.com/LittleBestAmoeba-max-14mb.gif")
                .WithColor(_color);
            Embed embedloading = embedBuilderLoading.Build();
            var message = await ReplyAsync(embed: embedloading);
            SauceNETClient sauce = new SauceNETClient("7018a095f6d54e1570bb5b4fd0cbcf20fbf4ed48");
            var result = await sauce.GetSauceAsync(link);
            List<Result> sourceurl = result.Results.ToList();
            EmbedAuthorBuilder embedAuthor = new EmbedAuthorBuilder()
                .WithName(Context.Client.CurrentUser.Username)
                .WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            EmbedBuilder embedBuilder = new EmbedBuilder()
                .WithTitle("Sauce Finder")
                .WithAuthor(embedAuthor)
                .WithCurrentTimestamp()
                .WithFooter(footer =>
                {
                    footer.WithText("Powered by SauceNao Api");
                });
            if (sourceurl.Count == 0)
            {
                embedBuilder.WithDescription("Aucun résultat.");
            }
            else
            {
                int ind = 0;
                while (ind < sourceurl.Count && ind < 2)
                {
                    EmbedFieldBuilder field = new EmbedFieldBuilder().WithName(sourceurl[ind].Name).WithValue(sourceurl[ind].SourceURL);
                    embedBuilder.AddField(field);
                    ind++;
                }
            }
            
            Embed embed = embedBuilder.Build();
            await message.ModifyAsync(msg => msg.Embed = embed);
        }
    }
}
