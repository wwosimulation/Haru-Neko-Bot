﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Neko_Test
{
    class Program
    {
        private CommandService _service;
        private DiscordSocketClient Client;
        private CommandService Commands;
        private InteractiveService Interact;
        private IServiceProvider services;
        static void Main(string[] args)
       => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            services = new ServiceCollection()
    .AddSingleton(Client)
    .AddSingleton<InteractiveService>()
    .BuildServiceProvider();
            Commands = new CommandService();
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);

            Client.MessageReceived += Client_MessageReceived;


            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            //string load = "NTQ4NzA0OTQyMzUzODA5NDA5.XOAxnw.t4zVsJZb-xAAJFQanENrb_ySYFs";

            await Client.LoginAsync(TokenType.Bot, "Bot Token");
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        //public event Func<GuildConfig, Task> JoinedGuild = delegate { return Task.CompletedTask; };

        private async Task Client_Log(LogMessage Message)
        {
            //Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
            //Make Spam Console.
        }


        private async Task Client_Ready()
        {
            await Client.SetGameAsync("No Game Hosting");
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix("-", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos, services);

            if (!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error {Result.ErrorReason}");

                if (Result.Error != null)
                    switch (Result.Error)
                    {

                        case CommandError.UnknownCommand:
                            break;
                        case CommandError.BadArgCount:
                            break;
                        default:
                            await Context.Channel.SendMessageAsync(
                                $"An error has occurred {Result.ErrorReason}");
                            break;
                    }
            }
            if (Result.IsSuccess)
            {
                if (Context.Guild.Id == 465795320526274561)
                {
                    var embed = new EmbedBuilder();
                    {
                        embed.WithAuthor($"{Context.User.Username}#{Context.User.Discriminator}", Context.User.GetAvatarUrl());
                        embed.AddField($"Used Command: "+Context.Message+"", ""+DateTime.Now.Hour+ ":" + DateTime.Now.Minute + " • " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " (GMT +7)");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Client.GetGuild(465795320526274561).GetTextChannel(579152173217218570).SendMessageAsync("", false, embed.Build());
                    }
                }
            }
        }
    }
}
