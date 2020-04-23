let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/roomhub")
        .build();

    connection.on("AddUserToRoom", (model) => {
        var avatarUrl = model.avatarUrl == null ? "/img/icon-avatar-default.png" : model.avatarUrl;
        var markup = `<tr> <td class="d-flex justify-content-center"><div class="rounded-circle custom-circle" style="background-image: url(${avatarUrl});"></div></td> <td>${model.username}</td> <td></td> </tr>`;

        $("#users tr:last").after(markup);
    });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

document.getElementById("start").addEventListener("click", e => {
    e.preventDefault();
    
    fetch("/roomhub",
            {
                method: "GET",
                headers: {
                    'content-type': 'application/json'
                }
            })
        .then(response => response.text())
        .then(connection.invoke("GetUpdate"));
});