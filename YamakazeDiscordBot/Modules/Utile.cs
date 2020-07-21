﻿using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamakazeDiscordBot.Modules
{
    public class Utile : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Affiche toute les commandes du bot")]
        public async Task HelpCommand()
        {
            List<CommandInfo> commands = Program._commands.Commands.ToList();
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle("Hello, I'm Yamakaze...")
                .WithDescription("Here are my commands, I wish you will like they...\n" +
                "It... it's okay if you don't like them... really...");
            embedBuilder.AddField("Utiles :", "help, ping, avatar, botinfo");
            embedBuilder.AddField("Moderation :", "kick, ban, unban, purge");
            embedBuilder.AddField("Fun :", "lenny, say");
            embedBuilder.AddField("Reaction :", "bite, baka, cuddle, feed, hug, kiss, \nlick, pat, putin, poke, slap, tickle");
            embedBuilder.AddField("Nsfw :", "nhsauce");
            embedBuilder.AddField("Help :", "Prefix : y!");
            embedBuilder.WithFooter(footer =>
            {
                footer
                .WithText("Wiki du personnage Yamakaze :\n https://kancolle.fandom.com/wiki/Yamakaze");
            });
            await ReplyAsync("", false, embedBuilder.Build());
        }

        [Command("ping")]
        public async Task Ping()
        {
            var EmbedBuilder = new EmbedBuilder().WithDescription("Pong! :ping_pong:**" + Program._client.Latency + " ms**");
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("Avatar")]
        [Summary("Resend the user avatar")]
        public async Task AvatarUser()
        {
            string avatarlink = Context.User.GetAvatarUrl();
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(Context.User.Username.ToString() + " avatar.");
            embedBuilder.WithImageUrl(avatarlink);
            Embed emded = embedBuilder.Build();
            await ReplyAsync(embed: emded);
        }

        [Command("botinfo")]
        [Summary("Give the bot information")]
        public async Task InfoBot()
        {
            EmbedBuilder embedbuilder = new EmbedBuilder();
            embedbuilder.AddField("Bot version :", "0.5");
            embedbuilder.AddField("Bot developper :", "Heliodore24#5872");
            embedbuilder.AddField("Program langage and library :", "C#, Discord.Net");
            embedbuilder.WithFooter(footer =>
            {
                footer.WithText("For more information, send a message to the developper");
            });
            Embed embed = embedbuilder.Build();
            await ReplyAsync(embed: embed);
        }
    }
}