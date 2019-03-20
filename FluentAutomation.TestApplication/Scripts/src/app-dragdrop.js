function enableDrop(ev) {
    ev.preventDefault();
}

function dragStart(ev) {
    var id = ev.currentTarget.id;
    ev.dataTransfer.setData('boarderId', id);

}

function dragDrop(ev) {
    var boarderId = ev.dataTransfer.getData('boarderId');
    var boarder = document.getElementById(boarderId);
    var conference = ev.currentTarget;
    //var conference = document.getElementById(conferenceId);
    conference.appendChild(boarder);
}