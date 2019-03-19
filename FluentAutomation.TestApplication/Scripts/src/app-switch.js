function popUp(url) {
    var features = 'height=440,width=400,location=0,menubar=0,scrollbars=1,toolbar=0,resizable=0,left=300,top=200';
    newwindow = window.open(url, 'new popup window name', features, false);
    if (window.focus) { newwindow.focus() }
    return false;
}
