var ALERT_TITLE = "Alert!";
var ALERT_BUTTON_TEXT = "Ok";

if (document.getElementById) 
{
    window.alert = function (txt)
    {
        createCustomAlert(txt);
    }
}

function createCustomAlert(txt) 
{
    if (txt.slice(0, 2) == "E:") 
    {
        d = document;
        if (d.getElementById("modalContainerError")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerError";
        mObj.style.height = d.documentElement.scrollHeight + "px";

        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxError";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        alertObj.style.visiblity = "visible";

        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(ALERT_TITLE));
        h1.innerHTML = "<br>Error!";

        msg = alertObj.appendChild(d.createElement("p"));
        //msg.appendChild(d.createTextNode(txt));
        msg.innerHTML = txt.slice(2);

        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtn";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.focus();
        btn.onclick = function () {
            removeCustomAlertError();
            return false;
        }
        alertObj.style.display = "block";
    }
    if (txt.slice(0, 2) == "S:") 
    {
        d = document;
        if (d.getElementById("modalContainerSuccess")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerSuccess";
        mObj.style.height = d.documentElement.scrollHeight + "px";

        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxSuccess";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        alertObj.style.visiblity = "visible";

        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(ALERT_TITLE));
        h1.innerHTML = "<br>Success!";

        msg = alertObj.appendChild(d.createElement("p"));
        //msg.appendChild(d.createTextNode(txt));
        msg.innerHTML = txt.slice(2);

        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtn";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.focus();
        btn.onclick = function () {
            removeCustomAlertSuccess();
            return false;
        }
        alertObj.style.display = "block";
    }
    if (txt.slice(0, 2)=="W:") 
    {
        d = document;
        if (d.getElementById("modalContainerWarning")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerWarning";
        mObj.style.height = d.documentElement.scrollHeight + "px";

        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxWarning";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        alertObj.style.visiblity = "visible";

        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(ALERT_TITLE));
        h1.innerHTML = "<br>Warning!";

        msg = alertObj.appendChild(d.createElement("p"));
        //msg.appendChild(d.createTextNode(txt));
        msg.innerHTML = txt.slice(2);

        btntable = alertObj.appendChild(d.createElement("table"));
        btntable.width = "100%";
        btnrow = btntable.appendChild(d.createElement("tr"));
        btndata1 = btnrow.appendChild(d.createElement("td"));
        btndata2 = btnrow.appendChild(d.createElement("td"));
        btn = btndata1.appendChild(d.createElement("a"));
        btn.id = "okBtn";
        btn.width = "100px";
        btn.appendChild(d.createTextNode("Yes"));
        btn.href = "#";
        btn.class="btn btn-success"
        btn.focus();

        btnc = btndata2.appendChild(d.createElement("a"));
        btnc.id = "closeBtn";
        btn.width = "100px";
        btnc.appendChild(d.createTextNode("No"));
        btnc.href = "#";
        btnc.focus();

        btn.onclick = function () {
            removeCustomAlertWarning();
            return false;
        }

        btnc.onclick = function () {
            removeCustomAlertWarning();
            alert("S: Closing canceled");
            removeCustomAlertBtn();
            return false;
        }
        alertObj.style.display = "block";
    }
    if (txt.slice(0, 2) == "N:") 
    {
        d = document;
        if (d.getElementById("modalContainerNormal")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerNormal";
        mObj.style.height = d.documentElement.scrollHeight + "px";

        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxNormal";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        alertObj.style.visiblity = "visible";

        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(ALERT_TITLE));
        h1.innerHTML = "<br>Alert!";

        msg = alertObj.appendChild(d.createElement("p"));
        //msg.appendChild(d.createTextNode(txt));
        msg.innerHTML = txt.slice(2);

        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtn";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.focus();
        btn.onclick = function () {
            removeCustomAlertNormal();
            return false;
        }
        alertObj.style.display = "block";
    }
}

function removeCustomAlertError() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerError"));
}
function removeCustomAlertSuccess() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerSuccess"));
}
function removeCustomAlertWarning() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerWarning"));
}
function removeCustomAlertNormal() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerNormal"));
}
function removeCustomAlertBtn() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("closeBtn"));
}