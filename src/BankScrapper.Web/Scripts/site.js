$(document).ready(function() {

    $('#inputCpf').mask('000.000.000-00');
    $('#inputAgency').mask('#-0', { reverse: true });
    $('#inputNumber').mask('#-0', { reverse: true });

    $('#bankSelect').change(function () {
        var value = $('#bankSelect').val();
        $('#bb-form').css('display', value == 1 ? 'block' : 'none');
        $('#nubank-form').css('display', value == 2 ? 'block' : 'none');
        $('#btn-submit').css('display', value == 1 || value == 2 ? 'inline-block' : 'none');
    });

    $('#btn-submit').click(function (e) {
        var body = {};

        var bank;
        var selectedBank = $('#bankSelect').val();
        if (selectedBank == 1) {
            bank = 'Banco do Brasil';
            body.agency = $('#inputAgency').val();
            body.account = $('#inputNumber').val();
            body.electronicPassword = $('#inputElectronicPassword').val();
        } else if (selectedBank == 2) {
            bank = 'Nubank';
            body.cpf = $('#inputCpf').val();
            body.password = $('#inputPassword').val();
        }

        showOverlay('Buscando os dados no ' + bank);

        $.ajax({
            url: window.location.origin + '/api/banks/scrape?bank=' + selectedBank,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(body),
            success: function (result) {
                closeOverlay();
                window.location.replace(window.location.origin + '/Results');
            },
            error: function (status, ex) {
                closeOverlay();
                console.error(status);
                console.error(ex);
                alert('Ocorreu um erro ao buscar os dados do banco!');
            }
        });

        e.preventDefault(); 
    });

    function showOverlay(message) {
        $('#overlay-text').text(message);
        $('#overlay').css('display', 'flex');
    }

    function closeOverlay() {
        $('#overlay').css('display', 'none');
    }
});