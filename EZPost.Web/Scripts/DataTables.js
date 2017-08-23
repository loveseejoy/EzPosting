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
        "processing": "加载中...",
        "lengthMenu": "每页显示_MENU_ ",
        "zeroRecords": "没有数据",
        "info": "第 _START_ 到 _END_条，总共 _TOTAL_条",
        "infoEmpty": "无记录",
        "infoFiltered": "",
        "infoPostFix": "",
        "search": "搜索:",
        "url": "",
        "emptyTable": "没有记录",
        "loadingRecords": "加载中...",
        "thousands": ",",
        "paginate": {
            "first": "第一页",
            "previous": "上一页",
            "next": "下一页",
            "last": "最后一页"
        },
        "aria": {
            "sortAscending": ": ASC",
            "sortDescending": ": DESC"
        }
    }
});