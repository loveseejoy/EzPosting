var print= {
    printHawb: function (result, type) {
        if (result.twPostServiceType === 1) {
            Report.LoadFromURL("/Content/Grid/ShipmentE.grf?" + (new Date()).getTime());

            var Recordset = Report.DetailGrid.Recordset;
            Report.PrepareRecordset();
            Recordset.Append();
            Report.FieldByName("shipment_number").AsString = result.hawbNumber;
            Report.FieldByName("sender_name").AsString = result.senderName == null ? "" : result.senderName;
            Report.FieldByName("sender_address1").AsString = result.senderAddress == null ? "" : result.senderAddress;
            Report.FieldByName("receiver_name").AsString = result.receiverName == null ? "" : result.receiverName;
            Report.FieldByName("receiver_address1").AsString = result.receiverAddress1 == null ? "" : result.receiverAddress1;
            Report.FieldByName("receiver_tel").AsString = result.receiverTel == null ? "" : result.receiverTel;
            Report.FieldByName("receiver_zip").AsString = result.receiverZip == null ? "" : result.receiverZip;
            Report.FieldByName("declare_currency").AsString = result.declareCurrency == null ? "" : result.declareCurrency;
            Report.FieldByName("weight").AsFloat = result.weight;
            Report.FieldByName("scontents").AsString = result.content == null ? "" : result.content;
            Report.FieldByName("declare_value").AsFloat = result.declareValue;
            Recordset.Post();

        } else if (result.twPostServiceType === 2) {
            Report.LoadFromURL("/Content/Grid/ShipmentR.grf?" + (new Date()).getTime());

            var Recordset = Report.DetailGrid.Recordset;
            Report.PrepareRecordset();
            Recordset.Append();
            Report.FieldByName("shipment_number").AsString = result.hawbNumber;
            Report.FieldByName("receiver_name").AsString = result.receiverName == null ? "" : result.receiverName;
            Report.FieldByName("receiver_tel").AsString = result.receiverTel == null ? "" : result.receiverTel;
            Report.FieldByName("sender_name").AsString = result.senderName == null ? "" : result.senderName;
            Report.FieldByName("sender_phone").AsString = result.senderPhone == null ? "" : result.senderPhone;
            Report.FieldByName("sender_address1").AsString = result.senderAddress == null ? "" : result.senderAddress;

            Report.FieldByName("receiver_address1").AsString = result.receiverAddress1 == null ? "" : result.receiverAddress1;

            Report.FieldByName("receiver_zip").AsString = result.receiverZip == null ? "" : result.receiverZip;
            Report.FieldByName("declare_currency").AsString = result.declareCurrency == null ? "" : result.declareCurrency;
            Report.FieldByName("weight").AsFloat = result.weight;
            Report.FieldByName("declare_value").AsFloat = result.declareValue;

            Report.FieldByName("Item").AsString = result.content == null ? "" : result.content;
            Report.FieldByName("ItemUnitPrice").AsFloat = result.declareValue;
            Report.FieldByName("ItemQuantity").AsInteger = 1;
            Recordset.Post();
        }
        if (type == 1) {
            //false: 立即打印
            //true: 选择打印机再打印
            Report.Print(false);
        } else if (type == 2) {
            Report.PrintPreview(true);
        }
    },
    printInvoic:function(result,type) {
        var myDate = new Date();
        Report.LoadFromURL("/Content/Grid/SamInvoice.grf?" + myDate.getTime());
        //CreatePrintViewerEx("100%", "80%", "/Content/Grid/SamInvoice.grf?" + myDate.getTime(), "", false, "<param name=BorderStyle value=1>");
        var Recordset = Report.DetailGrid.Recordset;
        Report.PrepareRecordset();
        for (var i = 0; i < result.length; i++) {
            for (var j = 0; j < result[i].hawbItems.length; j++) {
                Recordset.Append();
                Report.FieldByName("shipment_number").AsString = result[i].hawbNumber;
                Report.FieldByName("sender_name").AsString = result[i].senderName == null ? "" : result[i].senderName;
                Report.FieldByName("sender_address1").AsString = result[i].senderAddress == null ? "" : result[i].senderAddress;
                Report.FieldByName("receiver_name").AsString = result[i].receiverName == null ? "" : result[i].receiverName;
                Report.FieldByName("receiver_address1").AsString = result[i].receiverAddress1 == null ? "" : result[i].receiverAddress1;
                Report.FieldByName("sender_contactname").AsString = result[i].senderName == null ? "" : result[i].senderName;
                Report.FieldByName("sender_contactphone").AsString = result[i].senderPhone == null ? "" : result[i].senderPhone;
                Report.FieldByName("receiver_contactname").AsString = result[i].receiverName == null ? "" : result[i].receiverName;
                Report.FieldByName("receiver_contactphone").AsString = result[i].receiverTel == null ? "" : result[i].receiverTel;
                Report.FieldByName("declare_currency").AsString = result[i].declareCurrency == null ? "" : result[i].declareCurrency;
                Report.FieldByName("w_weight").AsFloat = result[i].weight;
                Report.FieldByName("weight").AsFloat = result[i].hawbItems[j].weight;
                Report.FieldByName("Item").AsString = result[i].hawbItems[j].content;
                Report.FieldByName("ItemQuantity").AsInteger = result[i].hawbItems[j].pieces;
                Report.FieldByName("ItemUnitPrice").AsFloat = result[i].hawbItems[j].price;
                Report.FieldByName("TotalValue").AsFloat = result[i].hawbItems[j].totalValue;
                Recordset.Post();
            }
        }

        if (type == 1) {
            Report.Print(false);
        } else if (type == 2) {
            Report.PrintPreview(true);
        }
    },
    printManifest:function(result,type) {
        Report.LoadFromURL("/Content/Grid/MawbReport.grf?" + (new Date()).getTime());
        var Recordset = Report.DetailGrid.Recordset;
        Report.PrepareRecordset();
        for (var i = 0; i < result.length; i++) {
                Recordset.Append();
                Report.FieldByName("rptHeader").AsString = "Manifest";
                Report.FieldByName("companyName").AsString = "Linehaul Express SARL.";
                Report.FieldByName("mawb_number").AsString = result[i].mawbNumber == null ? "" : result[i].mawbNumber;
                Report.FieldByName("mainfest_number").AsString = result[i].manifestNumber == null ? "" : result[i].manifestNumber;
                Report.FieldByName("SenderInfo").AsString = result[i].senderInfo == null ? "" : result[i].senderInfo;
                Report.FieldByName("ReceiverInfo").AsString = result[i].receiverInfo == null ? "" : result[i].receiverInfo;
                Report.FieldByName("FlightRoute").AsString = result[i].flightRoute == null ? "" : result[i].flightRoute;
                Report.FieldByName("FlightInfo").AsString = result[i].flightInfo == null ? "" : result[i].flightInfo;
                Report.FieldByName("BagHawb").AsString = result[i].bagAndHawb == null ? "" : result[i].bagAndHawb;
                Report.FieldByName("declare_value").AsString = result[i].declareValue == null ? "" : result[i].declareValue;
                Report.FieldByName("scontents").AsString = result[i].contents == null ? "" : result[i].contents;
                Report.FieldByName("declare_currency").AsString = result[i].declareCurrency == null ? "" : result[i].declareCurrency;
                Report.FieldByName("total_weight").AsFloat = result[i].totalWeight;
                Report.FieldByName("weight").AsFloat = result[i].weight;
                Report.FieldByName("total_shipment").AsInteger = result[i].totalShipment;
                Recordset.Post();
        }

        if (type == 1) {
            Report.Print(false);
        } else if (type == 2) {
            Report.PrintPreview(true);
        }
    },
    printOriginLable:function(result, type) {
        var LODOP = getLodop();
        LODOP.PRINT_INIT("OriginLabel");
        LODOP.SET_PRINT_PAGESIZE(1, 800, 1200, "");
        //水平直线：左端y坐标，左端x坐标，右端x坐标，右端y坐标
        LODOP.ADD_PRINT_LINE("4cm", "0.151cm", "4cm", "7.9cm", 0, 1);
        //竖直线：上端y坐标，上端x坐标 ,下端y坐标，下端x坐标
        LODOP.ADD_PRINT_LINE("4cm", "2.699cm", "1.9cm", "2.699cm", 0, 1);
        LODOP.ADD_PRINT_LINE("4cm", "5.501cm", "1.9cm", "5.501cm", 0, 1);
        LODOP.ADD_PRINT_RECT("1.871cm", "0.101cm", "7.8cm", "9.901cm", 0, 1);
        LODOP.ADD_PRINT_LINE("7.49cm", "0.151cm", "7.49cm", "7.8cm", 0, 1);
        LODOP.ADD_PRINT_LINE("9cm", "0.151cm", "9cm", "7.8cm", 0, 1);
        LODOP.ADD_PRINT_IMAGE("-0.159cm", "1.058cm", "5.662cm", "1.588cm", "<img border='0' src='/Content/images/Logo_Linex.jpg' />");
        LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);

        LODOP.ADD_PRINT_TEXT("2.725cm", "0.503cm", "1.984cm", "1.27cm", result.agentCode);
        LODOP.ADD_PRINT_TEXT("2.672cm", "3.043cm", "1.984cm", "1.27cm", result.customerCode);
        LODOP.ADD_PRINT_TEXT("2.275cm", "5.609cm", "2.117cm", "1.27cm", result.serviceNo);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 30);
        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("4.154cm", "0.45cm", "1.217cm", "0.714cm", "From：");
        LODOP.ADD_PRINT_TEXT("4.18cm", "1.402cm", "2.99cm", "0.635cm", result.senderName);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("4.154cm", "4.683cm", "2.99cm", "0.635cm", result.senderPhone);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("5.636cm", "0.397cm", "0.926cm", "0.714cm", "To：");
        LODOP.ADD_PRINT_TEXT("4.736cm", "0.476cm", "7.144cm", "0.873cm", result.senderAddress);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("5.636cm", "1.138cm", "6.165cm", "0.741cm", result.receiverName);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("6.191cm", "0.397cm", "7.144cm", "1.005cm", result.receiverAddress1);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("7.17cm", "0.397cm", "1.905cm", "0.714cm", "PostCode：");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.ADD_PRINT_TEXT("7.17cm", "1.931cm", "2.196cm", "0.635cm", result.receiverZip);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("7.223cm", "4.842cm", "2.778cm", "0.635cm", result.receiverTel);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("7.197cm", "4.207cm", "1.005cm", "0.714cm", "Tel：");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.ADD_PRINT_TEXT("7.779cm", "0.397cm", "1.588cm", "0.714cm", "Content/");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.ADD_PRINT_TEXT("7.779cm", "1.879cm", "2.196cm", "0.635cm", "1");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("8.308cm", "0.397cm", "4.075cm", "1.138cm", result.content);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);

        LODOP.ADD_PRINT_LINE("7.5cm", "5.398cm", "9cm", "5.398cm", 0, 1);
        LODOP.ADD_PRINT_TEXT("7.858cm", "5.556cm", "1.588cm", "0.714cm", "Value");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.ADD_PRINT_TEXT("7.858cm", "6.615cm", "1.27cm", "0.635cm", result.declareCurrency);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_TEXT("8.44cm", "5.609cm", "2.064cm", "0.582cm", result.declareValue);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        LODOP.ADD_PRINT_BARCODE("9.843cm", "0.529cm", "7.064cm", "1.561cm", "Code39", result.hawbNumber);
        LODOP.ADD_PRINT_TEXT("9.208cm", "0.423cm", "2.302cm", "0.714cm", "CustomerRef：");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.ADD_PRINT_TEXT("9.208cm", "2.566cm", "4.524cm", "0.635cm", result.customerHawb);
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);



        if (type == 1) {
            LODOP.PRINT();
        } else if (type == 3) {
            LODOP.PRINT_DESIGN();
        } else if (type == 2) {
            LODOP.PREVIEW();
        }
    },
    checkIsInstall: function () {
        try {
            var LODOP = getLodop();
            if (LODOP.VERSION) {
                return true;
            };
            return false;
        } catch (err) {
            return false;
        }
    }
}