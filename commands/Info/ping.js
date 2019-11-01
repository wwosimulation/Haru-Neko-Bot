module.exports = {
    name: "ping",
    category: "Info",
    description: "Returns latency and API ping.",
    run: async(_client, message, args) => {
        const msg = await message.channel.send("Pinging...");

        msg.edit(`Pong!\nLatency is ${Math.floor(msg.createdAt - message.createdAt)}ms\nAPI Latency is ${Math.round(_client.ping)}ms`);
    }
}