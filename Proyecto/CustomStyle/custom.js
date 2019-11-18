$(document).ready(function () {


    $("body").on("change", "input[name=registrado]", function () {

       
        if ($(this).val() === "1") {
            $("div[id=Div-ClienteRegis]").show();
            $("div[id=Div-ClienteNoRegis]").hide();
        }
        else {
            $("div[id=Div-ClienteRegis]").hide();

            $.ajax({
                url: "/Cliente/Registrar",
                method: "get",
                success: function (data, status) {
                    if (data !== null) {
                        $("div[id=Div-Form-Cont]").html(data);
                        $("div[id=Div-ClienteNoRegis]").show();
                    }
                }
            });
        }
    });


   
    //linea 39 yo me entiendo :V

    //$("body").on("click", "button[id=Btn-BuscarProducto]", function (e) {

    //    var keys = $("input[id=Input-Busqueda]").val();
   
    //    $.ajax({
    //        url: "/Producto/Buscar?key=" + keys,
    //        method: "post",
    //        success: function (data, status) {
    //            if (data.Nombre === null) {
    //                $("label[id=Lbl-ProductoEncontrado]").text("No se ha encontrado el producto!");
    //                $("div[id=Div-ProductoEncontrar]").hide();
    //            }
    //            //desde aqui
    //            else {
                    
    //                $("label[id=Lbl-ProductoEncontrado]").text("Se ha hencontrado el producto! " + 
    //                    data.Nombre + " -- Stock: " +data.Stock + " - unidad de medida: " + data.UnidadMedida);
    //                $("div[id=Div-ProductoEncontrar]").show();
    //                var stocki = $("div[id=Div-ProductoEncontrar]").find("input[type=number]");

    //                if (data.Stock === 0) {
    //                    $("label[id=Lbl-MensajeProducto]").text("Hay 0 de stock para este producto!");
    //                    $("button[id=Btn-AgregarDetalleVenta]").prop("disabled", true);
    //                }
    //                else {
    //                    stocki.attr("value", 1);
    //                    stocki.attr("max", data.Stock);
    //                    stocki.attr("min", 1);

    //                    $("input[name=IdProducto]").attr("value", data.IdProducto);
    //                    $("input[name=precioProd]").attr("value", data.Precio);
    //                    $("label[id=Lbl-NombreProducto]").text(data.Nombre);
    //                }
    //            }
    //        }
    //    });
    //});


    //var rows = [];

    $("body").on("click", "button[id=Btn-AgregarDetalleVenta]", function (e) {
        e.preventDefault();
       
        var form = $("form[id=Form-Add-VentaDetalle]");

            form.find("input[name=Total]").attr("value",
            form.find("input[name=Cantidad]").val().replace(",", ".") *
            form.find("input[name=precioProd]").val().replace(",", "."));
        
        //e.preventDefault();
        var stocki = $("div[id=Div-ProductoEncontrar]").find("input[type=number]");

        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: form.serialize(),
            beforeSend: function () {
                stocki.prop("disabled", true);
            },
            success: function (data, status) {
                if (data === false) {
                    $("label[id=Lbl-ProductoEncontrado]").text("No se puede agregar has sobrepasado el stock! ");
                    return;
                }
                if (data !== null) {
                    var body = $("tbody[id=TBodyVenta]");
                    var nombre = $("label[id=Lbl-NombreProducto]").text();

                    body.append("<tr id=" + data.IdDetalleVenta + "><td>" + nombre + "</td>" +
                        "<td >" + data.Cantidad + "</td>" +
                        "<td class=" + "tr-cantidad-sub" +">" + data.Total + "</td><td><a class="+"a-eliminar-row"+" id=" + data.IdDetalleVenta + " href=" + "/Venta/QuitarDetalle?id=" +
                        data.IdDetalleVenta + ">Quitar</a></td></tr>");

                   var rows = $("tbody[id=TBodyVenta]").find("a");

                    if (rows.length >= 1) {

                        var suma = 0.0;
                        for (var i = 0; i < rows.length; i++) {
                            var auxrow = body.find("tr[id=" + rows[i].getAttribute("id") + "]");
                            var valueros = parseFloat(auxrow.find("td[class=tr-cantidad-sub]").text()
                                .replace(",","."));
                            suma += valueros;
                        }

                        $("button[id=Btn-RealizarCompra]").show();
                        $("label[id=Lbl-TotalGananciasVenta]").show();
                        $("label[id=Lbl-TotalGananciasVenta]").text("Total en soles: " + suma +" S./");

                    }
                    else {
                        $("button[id=Btn-RealizarCompra]").hide();
                        $("label[id=Lbl-TotalGananciasVenta]").hide();
                    }
                    
                }
            }
        });

       
        $.ajax({
            url: "/Venta/getInfoUpdate?id=" + form.find("input[name=IdProducto]").attr("value"),
            method: "post",
            beforeSend: function () {
                stocki.prop("disabled", true);
                },
            success: function (data, status) {
                if (data.Nombre !== null) {
                    stocki.prop("disabled", false);

                    $("label[id=Lbl-ProductoEncontrado]").text("Se ha hencontrado el producto! " +
                        data.Nombre + " -- Stock: " + data.Stock) + " - unidad de medida: " + data.UnidadMedida;

                    if (data.Stock === 0) {
                        $("lable[id=Lbl-MensajeProducto]").text("Hay 0 de stock para este producto!");
                        $("button[id=Btn-AgregarDetalleVenta]").prop("disabled", true);
                    }
                    else {
                        stocki.attr("value", data.Stock);
                        stocki.attr("max", data.Stock);
                        stocki.attr("min", 1);

                        $("input[name=IdProducto]").attr("value", data.IdProducto);
                        $("input[name=precioProd]").attr("value", data.Precio);
                        $("label[id=Lbl-NombreProducto]").text(data.Nombre);
                    }
                }
                else {
                    console.log("no se pudo actualizar el stock del producto al agregar el detalle");
                }
            }
        });
    });



    $("body").on("click", "a[class=a-eliminar-row]", function (e) {
        e.preventDefault();

        var rowDelete = $("tbody[id=TBodyVenta]").find("tr[id=" + $(this).attr("id") + "]");
        rowDelete.remove();
        
        $.ajax({
            url: "/Venta/QuitarDetalle?id=" + $(this).attr("id"),
            method: "post",
            success: function (data, status) {
                if (data === true) {
                    console.log("se ha eliminado el detalle");
                }
                else {
                    console.log("no se ha eliminado");
                }
            }
        });

    });

    $("body").on("click", "button[id=Btn-BuscarClienteDni-Nom]", function (e) {

        var key = $("input[id=Input-BuscarCliente]").val();
        $.ajax({
            url: "/Cliente/BuscarCliente?key=" + key,
            method: "post",
            success: function (data, status) {
                if (data.Nombre === null) {
                    $("label[id=Lbl-Encontrado2]").text("No se ha encontrado un cliente con ese nombre o DNI!");
                }
                else {
                    $("label[id=Lbl-Encontrado2]").text("Se ha encontrado al usuario " + data.Nombre);
                    $("input[name=IdCliente]").attr("value", data.IdCliente);

                    $("div[id=Div-Cuerpo-Venta]").show();
                }
            }
        });

    });


    $("body").on("click", "button[id=Btn-RealizarCompra]", function () {
        return location.reload();
    });

    

});

