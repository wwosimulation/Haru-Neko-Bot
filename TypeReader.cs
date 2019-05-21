using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace Neko_Test.CommandHandler
{
    public class CommandHandler
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandler(CommandService commands, DiscordSocketClient client, IServiceProvider services)
        {
            _commands = commands;
            _client = client;
            _services = services;
        }

        public async Task SetupAsync()
        {
            _client.MessageReceived += CommandHandleAsync;

            // Add BooleanTypeReader to type read for the type "bool"
            _commands.AddTypeReader(typeof(bool), new Func());

            // Then register the modules
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        public async Task CommandHandleAsync(SocketMessage msg)
        {
            // ...
        }

        public class Func : TypeReader
        {
            public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
            {
                bool result;
                if (bool.TryParse(input, out result))
                    return Task.FromResult(TypeReaderResult.FromSuccess(result));

                return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as a boolean."));
            }
        }
    }
}
