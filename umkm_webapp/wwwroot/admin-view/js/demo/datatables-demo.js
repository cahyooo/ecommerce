// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('#dataTable').DataTable();
    "order": [[2, "desc"]], //or asc 
    "columnDefs" : [{ "targets": 2, "type": "Name" }],
});
