$(document).ready(function () {

    $('form').submit(function (event) {

        if ($('#summary-success')) {
           $('#summary-success').empty();
        }

        //event.preventDefault(); // Avoid Page Reload
        alert("Form Submitted :)");

        // Tip pour récupérer les données de notre formulaire.
        // var form_data = $('form').serializeArray();
        // var display = JSON.stringify(form_data);
        // alert(display);

        $(document).ready(function () {

            $('.error-msg span').each(function (key, value) {
                // iterate through each of the '.error-msg' elements, and do stuff in here
                // 'this' and '$(this)' refer to the current '.error-msg' element

                var $span = $(this);
                var divError = $span.closest('div');

                //if ($span.hasClass('field-validation-valid')) {
                  //  if (divError.hasClass('alert alert-danger alert-dismissible')) {
                    //    divError.removeClass('alert alert-danger alert-dismissible');
                        //console.log("We remove Class Alert Danger");
                    //}
                    //console.log("Field Valid Detected");
                    //divError.addClass('alert alert-success alert-dismissible');
                    //$span.append("<div>Demande prise en compte</div>");
                    //divError.show();
                //}
                if ($span.hasClass('field-validation-error')) {
                    if (divError.hasClass('alert alert-success alert-dismissible')) {
                        divError.removeClass('alert alert-success alert-dismissible');
                        //console.log("We remove Class Alert Success");
                    }
                    console.log("Field Invalid Detected");
                    divError.addClass('alert alert-danger alert-dismissible');
                    divError.show();
                }
            });

            // Once form submitted and Dom Reloaded
            $("input").focus(function () {
                var divNear = $(this).closest('div');
                //console.log(divNear);
                $(divNear).find(".error-msg").hide();
            });
        });
    });
});