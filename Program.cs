using System;
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
using System.Threading.Tasks;


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

            string load = "Token Bot";

            await Client.LoginAsync(TokenType.Bot, load);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }


        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Supporting WWO - Game Server");
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
        }
    }
}
