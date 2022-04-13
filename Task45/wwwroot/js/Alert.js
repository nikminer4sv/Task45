messages = 0;

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function SetMessagesCount() {

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "https://" + window.location.host + "/home/getusermessagescount", false);
    xmlHttp.send(null);
    var count = xmlHttp.responseText;

    messages = parseInt(count);

    DisplayNewMessage();

}

async function DisplayNewMessage() {

    while (true) {
        console.log("GG");
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", "https://" + window.location.host + "/home/getusermessagescount", false);
        xmlHttp.send(null);
        var count = xmlHttp.responseText;

        if (parseInt(count) > messages) {
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.open("GET", "https://" + window.location.host + "/home/getusermessages", false);
            xmlHttp.send(null);
            var response = xmlHttp.responseText;
            alert("New message\nSender: " + JSON.parse(response)[0]["sender"] + "\nTheme: " + JSON.parse(response)[0]["theme"]);
            messages = parseInt(count);
        }

        await sleep(5000);
    }


}

window.onload = function () {
    SetMessagesCount();
};