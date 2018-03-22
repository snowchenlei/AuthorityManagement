function loadPanel() {
    $('#searchPanel').on('shown.bs.collapse', function () {
        var $search = document.getElementById('searchTitle')
        $search.setAttribute('class', 'glyphicon glyphicon-menu-up');
    })
    $('#searchPanel').on('hidden.bs.collapse', function () {
        var $search = document.getElementById('searchTitle')
        $search.setAttribute('class', 'glyphicon glyphicon-menu-down');
    });
}