$(document).ready(function () {
    $("#Order_PhoneNumer").mask('000 000 00 00', { placeholder: "Phone Number Ex: XXX XXX XX XX" });
});


function submitForm() {
    $("#checkoutForm").submit();
}
