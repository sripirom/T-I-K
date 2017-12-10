﻿(function () {
    'use strict';
    let transportType = signalR.TransportType[getParameterByName('transport')] || signalR.TransportType.WebSockets;
    let logger = new signalR.ConsoleLogger(signalR.LogLevel.Information);
    let connection = new signalR.HubConnection('/chat', { transport: transportType, logger: logger });

    connection.onclose(e => {
        if (e) {
            appendLine('Connection closed with error: ' + e, 'red');
        }
        else {
            appendLine('Disconnected', 'green');
        }
    });

    connection.on('SetUsersOnline', usersOnline => {
        usersOnline.forEach(user => addUserOnline(user));
    });

    connection.on('UsersJoined', users => {
        users.forEach(user => {
            appendLine('User ' + user.name + ' joined the chat');
            addUserOnline(user);
        });
    });

    connection.on('UsersLeft', users => {
        users.forEach(user => {
            appendLine('User ' + user.name + ' left the chat');
            document.getElementById(user.connectionId).outerHTML = '';
        });
    });

    connection.on('Send', (userName, message) => {
        var nameElement = document.createElement('b');
        nameElement.innerText = userName + ':';

        var msgElement = document.createElement('span');
        msgElement.innerText = ' ' + message;

        var child = document.createElement('li');
        child.appendChild(nameElement);
        child.appendChild(msgElement);
        document.getElementById('messages').appendChild(child);
    });

    connection.start().catch(err => appendLine(err, 'red'));

    document.getElementById('sendmessage').addEventListener('submit', event => {
        let data = document.getElementById('new-message').value;
        connection.invoke('Send', data).catch(err => appendLine(err, 'red'));
        event.preventDefault();
    });

    function appendLine(line, color) {
        let child = document.createElement('li');
        if (color) {
            child.style.color = color;
        }
        child.innerText = line;
        document.getElementById('messages').appendChild(child);
    };

    function addUserOnline(user) {
        if (document.getElementById(user.connectionId)) {
            return;
        }
        var userLi = document.createElement('li');
        userLi.innerText = `${user.name} (${user.connectionId})`;
        userLi.id = user.connectionId;
        document.getElementById('users').appendChild(userLi);
    }

    function getParameterByName(name, url) {
        if (!url) {
            url = window.location.href;
        }
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    };

})();
