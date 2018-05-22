
$(document).ready(function () {

    $('#bankSelect').change(function () {
        var value = $('#bankSelect').val();
        $('#bb-form').css('display', value == 1 ? 'block' : 'none');
        $('#nubank-form').css('display', value == 2 ? 'block' : 'none');
        $('#btn-submit').css('display', value == 1 || value == 2 ? 'inline-block' : 'none');
    });

    $('#btn-submit').click(function () {

        var selectedBank = $('#bankSelect').val();
        if (selectedBank == 1) {

        } else if (selectedBank == 2) {

        }


    });

});