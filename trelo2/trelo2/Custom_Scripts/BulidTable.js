$(document).ready(function() {

    $.ajax({
        url: '/Tasks/BulidTaskTable',
        success: function(result) {
            $('#TaskTableDiv').html(result);
        }

    });

});