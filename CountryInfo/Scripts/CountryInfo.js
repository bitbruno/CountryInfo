function process_post_form() {
    $(document).ready(function () {
        //submit posts
        // get the post text from the form 
        var info_text = $('#info_text').attr('value');
        var selectedCountry = $('#select_country').val();
        //validate the entry before sending it
        if (info_text.length > 0 && info_text.length <= 250) {
            $.ajax({
                type: "POST",
                url: "/Home/CountryInfo/",
                data: "info_text=" + info_text + "&selectedCountry=" + selectedCountry,
                success: function () {

                }
            });
            return true;
        }
        else {
            if (info_text.length <= 0) {
                $('#failUnder').slideDown();
                $('#failUnder').delay(1000);
                $('#failUnder').slideUp();
            }
            else if (info_text.length > 250) {
                $('#failOver').slideDown();
                $('#failOver').delay(1000);
                $('#failOver').slideUp();
            }
            return false;
        }
    });
}

function change_country(country_id) {
    $(document).ready(function () {
        $('#select_country').val(country_id);
    });
}

