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
                    $('#info_form').hide();
                    $('#success').slideDown();
                    $('#success').delay(2000);
                    $('#success').slideUp();
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

function cancel_post() {
    $('#info_form').hide();
}


$(document).ready(function () {
    //highlighting the maps
    $('#world_map').maphilight();
    $('#continent_map').maphilight();

    $("#_world_map").click(function (event) {
        var continent_name = event.target.title;
        window.location = "/Home/CountryInfo/" + continent_name;
    });

    $("#_continent_map").click(function (event) {
        var country_name = event.target.title;
        $('#select_country').val(country_name);
        $('#info_form').show();
    });
});

