(function () {
    let webSocket
    var getWebSocketMessages = function (onMessageReceived) {
        let url = `ws://${location.host}/stream/get`;
        webSocket = new WebSocket(url);

        webSocket.onmessage = onMessageReceived;
    };

    let ulElement = document.getElementById('chatMessages');

    getWebSocketMessages(function (message) {
        ulElement.innerHTML = ulElement.innerHTML += `<li>${message.data}</li>`
    });

    document.getElementById("sendmessage").addEventListener("click", function () {
        let textElement = document.getElementById("messageTextInput");
        let text = textElement.value;
        webSocket.send(text);
        textElement.value = '';
    });
}());