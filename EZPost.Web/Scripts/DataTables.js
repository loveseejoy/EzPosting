//DataTables.js  这是Datatables的相关知识，具体作用请求官网查看
$.extend($.fn.dataTable.defaults, {
    "dom": "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 text-center'i>>" +
            "<'row'<'col-sm-5'l><'col-sm-7'p>>",//默认是lfrtip
    "processing": true,//加载中
    "serverSide": true,//服务器模式（★★★★★重要，本文主要介绍服务器模式）
    "searching": false,//datatables自带的搜索
    "scrollX": true,//X滑动条
    "pagingType": "full_numbers",//分页模式
    "ajax": {
        "type": "POST",//（★★★★★重要）
        "contentType": "application/json; charset=utf-8"
    },
    "language": {
        "processing": "Loading...",
        "lengthMenu": "Every Page Show _MENU_ Itmes",
        "zeroRecords": "No Record",
        "info": "Show _START_ To _END_ Result，Total _TOTAL_ Items",
        "infoEmpty": "Show From 0 To 0 Result，Total 0 Item",
        "infoFiltered": "",
        "infoPostFix": "",
        "search": "Search:",
        "url": "",
        "emptyTable": "No Record",
        "loadingRecords": "Loading...",
        "thousands": ",",
        "paginate": {
            "first": "First",
            "previous": "PrePage",
            "next": "NextPage",
            "last": "Last"
        },
        "aria": {
            "sortAscending": ": ASC",
            "sortDescending": ": DESC"
        }
    }
});