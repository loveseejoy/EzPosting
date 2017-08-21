(function ($) {

    //Notification handler
    //注释，已经在index.html注册,因为我们的信息接受是在页面顶部
    //abp.event.on('abp.notifications.received', function (userNotification) {
    //    abp.notifications.showUiNotifyForUserNotification(userNotification);
    //});

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }

})(jQuery);