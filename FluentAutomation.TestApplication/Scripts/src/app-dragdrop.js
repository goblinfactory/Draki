function ddAllowDrop(ev) {
    ev.preventDefault();
}

function ddDrag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function ddDrop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    ev.target.appendChild(document.getElementById(data));
}