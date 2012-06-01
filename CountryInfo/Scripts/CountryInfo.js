function process_post_form() {
    $(document).ready(function () {
        //submit messages
        // get the message text from the form and the selected country
        var info_text = $('#info_text').attr('value');
        var selectedCountry = $('#select_country').val();
        //validate the entry before sending it
        if (info_text.length > 0 && info_text.length <= 250) {
            $.ajax({
                type: "POST",
                url: "/Home/CountryInfo/",
                data: "info_text=" + info_text + "&selectedCountry=" + selectedCountry,
                success: function () {
                    $('#black_box').hide();
                    $('#info_text').val('');
                    $('#success').slideDown();
                    $('#success').delay(2000);
                    $('#success').slideUp();
                }
            });
            return true;
        }
        else { //if there is a validation error show to the user
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
    $('#black_box').hide();
}

function info_fadeIn() {
    $('#country_info_list').fadeIn();
}

function info_fadeOut() {
    $('#country_info_list').fadeOut();
}

$(document).ready(function () {
    //highlighting the maps
    $('#world_map').maphilight();
    $('#continent_map').maphilight();

    //when the user clicks on a continent go to the ContryInfo page
    $("#_world_map").click(function (event) {
        var continent_name = event.target.title;
        window.location = "/Home/CountryInfo/" + continent_name;
    });

    //if the user clicks outside the message form close it
    $("#black_box").click(function (event) {
        var caller = event.target.id;
        if (caller == "black_box")
            $('#black_box').hide();
    });

    //when the user clicks on a country show the message box and load the previous messages
    $("#_continent_map").click(function (event) {
        var country_name = event.target.title;
        $('#select_country').val(country_name);
        $('#black_box').show();
        $('#country_infos').load('/Home/CountryInfoList/' + encodeURIComponent(country_name));
    });

});

