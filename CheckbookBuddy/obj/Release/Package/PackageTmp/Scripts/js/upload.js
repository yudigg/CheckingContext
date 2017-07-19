$(document).ready(function () {
    $("#foofileuploader").uploadFile({
        url: "../Home/UploadFile?orderId=" + 1 + "&person=" + 7,
        statusBarWidth: '350',
        dragdropWidth: '350',
        showDelete: true,
        autoSubmit: true,
        showProgress: true,
        showPreview: false,

        deleteCallback: function (data, pd) {
            $.post("../Home/DeleteFile?url=" + data.url,
                function (resp, textStatus, jqXHR) {
                    //Show Message
                    console.log(resp, textStatus);
                    alert(resp.msg);
                    pd.statusbar.hide(); //You choice.
                });
        }

    });

});