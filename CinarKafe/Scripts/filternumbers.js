
function validateInput(event) {
    var theEvent = event || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;

    if (!regex.test(key)) 
        theEvent.preventDefault ? theEvent.preventDefault() : (theEvent.returnValue = false); 
}