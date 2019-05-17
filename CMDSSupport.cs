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

        [Command("prune")]
        [RequireBotPermission(Discord.GuildPermission.ManageMessages)]
        public async Task Prune(int count)
        {
            count++;
            if (count < 1)
                return;
            if (count > 1000)
                count = 1000;

            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageMessages)
            {
                await Context.Channel.SendMessageAsync("User need ManageMessages permission.");
                return;
            }
            else await Context.Channel.DeleteMessageAsync(await Context.Channel.GetMessageAsync(1000));
        }

    }
}
