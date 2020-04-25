let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/roomhub")
        .build();

    connection.on("AddUserToRoom", (model) => {
        var avatarUrl = model.avatarUrl == null ? "/img/icon-avatar-default.png" : model.avatarUrl;

        var rowElm = document.createElement("tr");
        rowElm.setAttribute("id", `${model.id}`);

        var avatarElm = document.createElement("td");
        avatarElm.setAttribute("class", "d-flex justify-content-center");
        var avatarDivElm = document.createElement("div");
        avatarDivElm.setAttribute("class", "rounded-circle custom-circle");
        avatarDivElm.setAttribute("style", `background-image: url(${avatarUrl});`);
        avatarElm.appendChild(avatarDivElm);
        rowElm.appendChild(avatarElm);

        var usernameElm = document.createElement("td");
        usernameElm.innerText = model.username;
        rowElm.appendChild(usernameElm);

        var btnElm = document.createElement("td");
        if (model.isForOwner) {
            var anchor = document.createElement("a");
            anchor.setAttribute("class", "btn btn-danger");
            anchor.href = `/Rooms/Kick?userId=${model.id}&roomId=${model.roomId}`;
            anchor.innerText = "Kick";

            btnElm.appendChild(anchor);
        }
        rowElm.appendChild(btnElm);

        var users = document.querySelector("#users");

        users.appendChild(rowElm);
    });

    connection.on("RemoveUserFromRoom", (id) => {
        var userForRemove = document.getElementById(id);
        userForRemove.remove();
    });

    connection.on("RedirectUser", (location) => {
        window.location.replace(location);
    });

    connection.on("StartContest", () => {
        var notification =
            '<div class="alert alert-warning alert - dismissible fade show" role="alert">Contest starting soon!</div>';

        $("table").before(notification);
    });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

//document.getElementById("open").addEventListener("click", e => {
//    e.preventDefault();
//    const contestId = document.getElementById("contestId").innerText;

//    connection.invoke("OpenRoom", contestId);
//});
