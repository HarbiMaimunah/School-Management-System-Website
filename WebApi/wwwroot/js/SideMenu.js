function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function openMenu() {
    setCookie("sidebar", "opened", 30);
    location.reload();
}

function closeMenu() {
    setCookie("sidebar", "closed", 30);
    location.reload();
}

if (getCookie("sidebar") == "opened") {
    document.getElementById("sidemenu").style.width = "200px";
    document.getElementById("sidemenu").style.border = "1px solid gray";
    document.body.style.marginLeft = "200px";
    document.getElementById("welcome").innerHTML = "Welcome to School<br />Management System";
    document.getElementById("welcome").style.left = "330px";
    document.getElementById("welcome").style.top = "145px";
    document.getElementById("welcome").style.textAlign = "center";
}

else if (getCookie("sidebar") == "closed") {
    document.getElementById("sidemenu").style.width = "0px";
    document.getElementById("sidemenu").style.border = "0px";
}

function link1() {
    document.getElementById("navLink1").style.color = "#d13838";
    document.getElementById("navLink2").style.color = "black";
    document.getElementById("navLink3").style.color = "black";
    document.getElementById("navLink4").style.color = "black";
}

function link2() {
    document.getElementById("navLink2").style.color = "#d13838";
    document.getElementById("navLink1").style.color = "black";
    document.getElementById("navLink3").style.color = "black";
    document.getElementById("navLink4").style.color = "black";
}

function link3() {
    document.getElementById("navLink3").style.color = "#d13838";
    document.getElementById("navLink1").style.color = "black";
    document.getElementById("navLink2").style.color = "black";
    document.getElementById("navLink4").style.color = "black";
}

function link4() {
    document.getElementById("navLink4").style.color = "#d13838";
    document.getElementById("navLink1").style.color = "black";
    document.getElementById("navLink2").style.color = "black";
    document.getElementById("navLink3").style.color = "black";
}
