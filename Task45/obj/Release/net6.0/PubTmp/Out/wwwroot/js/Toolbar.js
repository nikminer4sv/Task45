function deleteCheckedUsers() {

    var objectsToDelete = document.getElementsByClassName("user-checkbox")

    var contactIds = [];

    for (var i = 0; i < objectsToDelete.length; i++) {
        var objectToDelete = objectsToDelete[i];
        if (objectToDelete.checked)
            contactIds.push(objectsToDelete[i].getAttribute("name"))
    }

    if (contactIds.length > 0) {

        var RequestString = "";

        for (var i = 0; i < contactIds.length - 1; i++) {
            RequestString += "Ids[" + i + "]=" + contactIds[i] + "&"
        }

        RequestString += "Ids[" + (contactIds.length - 1) + "]=" + contactIds[contactIds.length - 1] + ""
        console.log(RequestString)

        window.location.replace("/account/DeleteUsers?" + RequestString);

    }

}

function blockCheckedUsers() {

    var objectsToDelete = document.getElementsByClassName("user-checkbox")

    var contactIds = [];

    for (var i = 0; i < objectsToDelete.length; i++) {
        var objectToDelete = objectsToDelete[i];
        if (objectToDelete.checked)
            contactIds.push(objectsToDelete[i].getAttribute("name"))
    }

    if (contactIds.length > 0) {

        var RequestString = "";

        for (var i = 0; i < contactIds.length - 1; i++) {
            RequestString += "Ids[" + i + "]=" + contactIds[i] + "&"
        }

        RequestString += "Ids[" + (contactIds.length - 1) + "]=" + contactIds[contactIds.length - 1] + ""
        console.log(RequestString)

        window.location.replace("/account/BlockUsers?" + RequestString);

    }

}

function unblockCheckedUsers() {

    var objectsToDelete = document.getElementsByClassName("user-checkbox")

    var contactIds = [];

    for (var i = 0; i < objectsToDelete.length; i++) {
        var objectToDelete = objectsToDelete[i];
        if (objectToDelete.checked)
            contactIds.push(objectsToDelete[i].getAttribute("name"))
    }

    if (contactIds.length > 0) {

        var RequestString = "";

        for (var i = 0; i < contactIds.length - 1; i++) {
            RequestString += "Ids[" + i + "]=" + contactIds[i] + "&"
        }

        RequestString += "Ids[" + (contactIds.length - 1) + "]=" + contactIds[contactIds.length - 1] + ""
        console.log(RequestString)

        window.location.replace("/account/UnblockUsers?" + RequestString);

    }

}

function activateAllCheckboxes() {
    var condition = false;
    if (document.getElementById("main-checkbox").checked)
        condition = true;

    var checkboxes = document.getElementsByClassName("user-checkbox");
    for (var i = 0; i < checkboxes.length; i++)
        checkboxes[i].checked = condition;
}