function profileButtonClick(event) {
    var isContain = profileContent.classList.contains("show");

    if (!isContain) {
        profileContent.classList.toggle("show");
    }
    else {
        profileContent.classList.remove("show");
    }

    event.stopPropagation();
}

$(document).ready(function () {
    var btn = document.getElementById("profileButton");
    var svg = document.getElementById("svgProfile");
    var profileContent = document.getElementById("profileContent");
    svg.onclick = profileButtonClick;
    btn.onclick = profileButtonClick;

    window.onclick = function (event) {
        if ((event.target != svg && event.target != btn)) {
            if (profileContent.classList.contains("show")) {
                profileContent.classList.remove("show");
            }
        }
    }
})
