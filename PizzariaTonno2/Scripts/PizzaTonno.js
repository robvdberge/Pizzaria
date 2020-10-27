$(function () {
    var total = 0;
    var basePrice = parseFloat($('#BasePrice').val());
    displayTotalPrice();

    function displayTotalPrice() {
        const priceDisplay = $('#totalPrice');
        priceDisplay.html(basePrice);
        resetTotal();
        for (let i = 1; i <= 3; i++) {
            getIngredientPrice( $("select#IngredientId" + i), $("#result" + i) );
        };
    }

    function getIngredientPrice(object, display) {
        let id = object.val();
        let Request = $.ajax({
            url: "/api/ingredientsapi/" + id,
            dataType: "json"
        });
        Request.done((data) => {
            display.html("+ € <span>" + data.Price + "</span>");
          
        }).then((data) => {
            total = total + parseFloat(data.Price);
            $('#totalPrice').html("€ " + total + ",-");
            });
        return true;
    }

    function resetTotal() {
        total = basePrice;
    }

    $('form').on('change', () => {
        displayTotalPrice();
        resetTotal();
    });
        
})