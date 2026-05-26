const WebSocket = require('ws');

const wss = new WebSocket.Server({ port: 8080 });

wss.on('connection', ws => {
    console.log("Unity connected!");

    setInterval(() => {
        const gifts = [1, 5, 10, 20, 50, 100];
        const amount = gifts[Math.floor(Math.random() * gifts.length)];

        const data = JSON.stringify({
            type: "gift",
            amount: amount
        });

        console.log("Send gift:", amount);

        ws.send(data);

    }, 2000);
});

console.log("Fake server running at ws://localhost:8080");