﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace YamakazeDiscordBot.Modules
{
    public class Nsfw : ModuleBase<SocketCommandContext>
    {
        [Command("nhsauce")]
        [Summary("Renvoie un lien Nhentai en fonction du 6digit donner en pinguant quelqu'un")]
        [Alias("nhs")]
        public async Task NhentaiSauce(IGuildUser user = null, [Remainder] string sauce=null)
        {
            if (sauce == null)
            {
                await ReplyAsync("You have to give a 6digits");
                return;
            }
            string message = $":eyes: {user.Mention} Here the sauce. \n https://www.nhentai.net/g/{sauce}";
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription(message);                
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("nhsauce")]
        [Summary("Renvoie un lien Nhentai en fonction du 6digit donner")]
        [Alias("nhs")]
        public async Task NhentaiSauceNoUser([Remainder] string sauce = null)
        {
            if (sauce == null)
            {
                await ReplyAsync("You have to give a 6digits. ");
                return;
            }
            string message = $":eyes: Here the sauce.\n https://www.nhentai.net/g/{sauce}";
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription(message);
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
    }
}