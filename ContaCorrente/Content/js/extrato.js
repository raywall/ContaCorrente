$(document).ready(function () {
    $('.abrelancamento').click(function () {
        $('#lancamento-modal').show();
    });

    $('#submit').click(function () {
        if ($('#TiposLancamento').children("option:selected").val() != 1 && $('#TiposLancamento').children("option:selected").val() != 2) {
            $('#hTiposLancamento').show();
            return;
        }
        else
            $('#hTiposLancamento').hide();

        if ($('#descricao').val().length <= 3) {
            $('#hdescricao').show();
            return;
        }
        else
            $('#hdescricao').hide();


        if ($('#valorlancamento').val().length == 0 || $('#valorlancamento').val() <= 0) {
            $('#hvalorlancamento').show();
            return;
        }
        else
            $('#hvalorlancamento').hide();

        $('#lancamentoForm').submit();
    });

    $('.close').click(function () {
        $('#lancamento-modal').hide();
    });
});