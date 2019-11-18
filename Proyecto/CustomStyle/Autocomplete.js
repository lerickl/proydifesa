var $jQuery_1_8_0 = jQuery.noConflict(true);

$jQuery_1_8_0("body").ready(function () {
    $jQuery_1_8_0("#Input-BuscarCliente").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Cliente/getDatosUsuarios",
                data: "{'ename':'" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }));
                }
            });
        }, minLenth: 1,
        select: function (event, ui) {
            //configure los datos para guardar
            $("#IdProducto").val(ui.item.id).val();
            $("#ProductoPrecio").val(ui.item.precio).val();
            $("#ProductoCantidad").focus();

            //etiquedas para mostrar
            this.value = ui.item.value;
            return false;
        }


    });
    


});
$jQuery_1_8_0("body").ready(function () {
    $jQuery_1_8_0("#Input-Busqueda").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Producto/getDatos",
                data: "{'ename':'" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }));
                }
            });
        }, minLenth: 1,
        select: function (status, data) {
            if (data.item.label === null) {
                $("label[id=Lbl-ProductoEncontrado]").text("No se ha encontrado el producto!");
                $("div[id=Div-ProductoEncontrar]").hide();
            }
            //desde aqui
            else {

                $("label[id=Lbl-ProductoEncontrado]").text("Se ha encontrado el producto! " +
                    data.item.label + " -- Stock: " + data.item.stock + " - unidad de medida: " + data.item.UnidadMedida);
                $("div[id=Div-ProductoEncontrar]").show();
                var stocki = $("div[id=Div-ProductoEncontrar]").find("input[type=number]");

                if (data.Stock === 0) {
                    $("label[id=Lbl-MensajeProducto]").text("Hay 0 de stock para este producto!");
                    $("button[id=Btn-AgregarDetalleVenta]").prop("disabled", true);
                }
                else {
                    stocki.attr("value", 1);
                    stocki.attr("max", data.stock);
                    stocki.attr("min", 1);

                    $("input[name=IdProducto]").attr("value", data.item.id);
                    $("input[name=precioProd]").attr("value", data.item.precio);
                    $("label[id=Lbl-NombreProducto]").text(data.item.Nombre);
                }
            }


            ////configure los datos para guardar
            //$("#IdProducto").val(ui.item.id).val();
            //$("#ProductoPrecio").val(ui.item.precio).val();
            //$("#ProductoCantidad").focus();

            ////etiquedas para mostrar
            //this.value = ui.item.value;
            return true;


        }


    });
});