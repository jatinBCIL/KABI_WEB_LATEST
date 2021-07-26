document.onkeypress = function() { if (window.event.keyCode == 13) return false; }
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 13) {
        return false;
    }
    else {
        return true;
    }
}

function isRestructionKey(e) {
    var keynum
    var keychar
    var numcheck
    // For Internet Explorer  
    if (window.event) {
        keynum = e.keyCode
    }
    // For Netscape/Firefox/Opera  
    else if (e.which) {
        keynum = e.which
    }
    keychar = String.fromCharCode(keynum)
               
    //List of special characters you want to restrict  
    if (keychar == "'" || keychar == "`" || keychar == "''") {
        return false;
    }
    else {
        return true;
    }
}

function isCurrencyKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    keychar = String.fromCharCode(charCode)
    if (keychar == "." || keychar == "0" || keychar == "1" || keychar == "2" || keychar == "3" || keychar == "4" || keychar == "5" || keychar == "6" || keychar == "7" || keychar == "8" || keychar == "9" || keychar == "Back") {
        var parts = document.getElementById(objBtnID).Text.split("."); //e.srcElement.value.split('.');
        if (parts.length > 1 && keychar == ".") {
            return false;
        } else {
            return true;
        }
    }
    else {
        return false;
    }
}
function postBackCheckBox() {
    var o = window.event.srcElement;
    if (o.tagName == 'INPUT' && o.type == 'checkbox' && o.name != null && o.name.indexOf('CheckBox') > -1) {
        __doPostBack('LinkButton1', '');
    }
}

function my_onkeydown_handler() {
    if (event.keyCode == 116) {
        event.keyCode = 0;
        event.returnValue = false;
    }
}

function noBack() {
    window.history.forward();
}

function stopRefresh() {
    switch (event.keyCode) {
        case 116: //F5 button
            event.returnValue = false;
            event.keyCode = 0;
            return false;
        case 82: //R button
            if (event.ctrlKey) {
                event.returnValue = false;
                event.keyCode = 0;
                return false;
            }
        case 13: //Enter button
            if (event.ctrlKey) {
                event.returnValue = false;
                event.keyCode = 0;
                return false;
            }
    }
}

function DisableSplChars(e) {
    var keynum
    var keychar
    var numcheck
    // For Internet Explorer
    if (window.event) {
        keynum = e.keyCode
    }
    // For Netscape/Firefox/Opera
    else if (e.which) {
        keynum = e.which
    }
    keychar = String.fromCharCode(keynum)
    //List of special characters you want to restrict
    if (keychar == "!" || keychar == "#"
        || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&"
        || keychar == "*" || keychar == "(" || keychar == ")" || keychar == "~"
        || keychar == "<" || keychar == ">" || keychar == "'" || keychar == "="
        || keychar == "" || keychar == "`" || keychar == ";" || keychar == ":"
        || keychar == "?" || keychar == "/" || keychar == "[" || keychar == "]" 
        || keychar == "{" || keychar == "}" || keychar == "|" || keychar == "+") {
        return false;
    }
    else {
        return true;
    }
}

function button_click(objTextBox, objBtnID) {
    if (window.event.keyCode == 13) {
        document.getElementById(objBtnID).focus();
        document.getElementById(objBtnID).click();
    }
}

function CheckSingleCheckbox(ob) {
    var grid = ob.parentNode.parentNode.parentNode;
    var inputs = grid.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (ob.checked && inputs[i] != ob && inputs[i].checked) {
                inputs[i].checked = false;
            }
        }
    }
}