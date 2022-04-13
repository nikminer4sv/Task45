var messagesCount = 0;

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function changeMessageVisibility(id, e) {

    var element = document.getElementById(id);
    if (element.classList.contains("d-none")) {

        element.classList.remove("d-none");
        e.innerText = "Hide";

    } else {
        element.classList.add("d-none");
        e.innerText = "Show";
    }

}

async function UpdateMessages() {

    while (true) {

        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", "https://" + window.location.host + "/home/getusermessagescount", false);
        xmlHttp.send(null);
        var count = xmlHttp.responseText;

        if (messagesCount != parseInt(count)) {
            messagesCount = count;
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.open("GET", "https://" + window.location.host + "/home/getusermessages", false);
            xmlHttp.send(null);
            var response = xmlHttp.responseText;
            var messages = JSON.parse(response);
            var messagesElement = document.getElementById("messages");
            messagesElement.innerHTML = "";

            for (var i = 0; i < messagesCount; i++) {

                var messageElement = '<div class="card mb-1"><div class="card-body"><h3 class="card-title">Sender: ' + messages[i]["sender"] +
                    '</h3><h5 class="card-text">Theme: ' + messages[i]["theme"] +
                    '</h5><div class="d-none" id="' + messages[i]["id"] + '"><p class="card-text">' + messages[i]["text"] +
                    '</p><span class="card-text">' + messages[i]["date"] +
                    '</span></div></div><button class="btn btn-dark" onclick="changeMessageVisibility(\'' + messages[i]["id"] + '\', this)">Show</button></div>';

                messagesElement.innerHTML += messageElement;
            }
        }

        await sleep(5000);
    }
}

window.onload = function () {
    UpdateMessages();
};
