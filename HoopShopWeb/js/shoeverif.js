function openShoeVerifOption(evt, transName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(transName).style.display = "block";
    evt.currentTarget.className += " active";
}

// Trigger the click event on the first button to make "London" tab active by default
document.getElementsByClassName("tablinks")[0].click();