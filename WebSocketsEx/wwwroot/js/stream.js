﻿(function () {
    var getWebSocketMessages = function (onMessageReceived) {
        var url = `ws://${location.host}/stream/get`
        console.log('url is: ' + url);

        var webSocket = new WebSocket(url);

        webSocket.onmessage = onMessageReceived;
    };

    var ulElement = document.getElementById('StreamToMe');

    getWebSocketMessages(function (message) {
        ulElement.innerHTML = ulElement.innerHTML += `<li> ${ulElement.childNodes.length}.${message.data}</li>`
    });
}());