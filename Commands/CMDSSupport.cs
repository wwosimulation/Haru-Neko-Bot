using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Neko_Test
{
    public class CMDSSupport : ModuleBase<SocketCommandContext>
    {
 /*       [Command("prune")]
        [RequireBotPermission(Discord.GuildPermission.ManageMessages)]
        public async Task Prune(ITextChannel channel)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;

            var texttoprune = Context.Channel.GetMessagesAsync(100).FlattenAsync();
            IMessage[] msgs;
            IMessage lastMessage = null;
                if (!User1.GuildPermissions.ManageMessages)
            {
                await Context.Channel.SendMessageAsync("User need ManageMessages permission.");
                return;
            }
            else
            {
                (texttoprune as IMessage).DeleteAsync();
            }
        }
        */
    }
}
