var ToastTypeEnum = { Default: "info", Info: "info", Warning: "warning", Success: "success", Error: "error" };

site = function () {
    var page = {}
    var common = {}
    var ajax = {}
    var notifications = {}

    return {
        page: page,
        common: common,
        ajax: ajax,
        notifications: notifications
    }
}();

site.notifications = function() {
    var toast = (function () {
        var settingsApplied = false;

        var applySettings = function () {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
            settingsApplied = true;
        };

        var show = function (message, toastTypeEnum) {
            if (!settingsApplied) {
                applySettings();
            }

            toastRef = toastr[toastTypeEnum](message, null);

        };

        var clear = function () {
            toastr.clear();
        };

        return {
            show: show,
            clear: clear
        };
    })();


    return {
        toast: toast
    }
}();

site.ajax = function () {

    var generateSuccessHandler = function (successCallback, url) {
        var target = url;
        var callback = function (serverData) {
            console.info("Ajax request sent. Location = " + target);

            successCallback(serverData);
        }

        return callback;
    };

    var generateErrorHandler = function (failureCallback, url) {
        var defaultErrorHandler = function (serverData) {
            var readyState = (serverData != undefined && serverData.readyState) ? serverData.readyState : "Not Available";
            var status = (serverData != undefined && serverData.status) ? serverData.status : "Not Available";
            var message = (serverData != undefined && serverData.responseText) ? serverData.responseText : undefined;

            if (message !== undefined && message !== null && message !== "") {

                try {
                    var resultObject = $.parseJSON(message);

                    // Do custom server error handling here

                } catch (err) { }
            }

            console.info("AJAX Error Details: Ready State [" + readyState + "] Status [" + status + "] Msg [" + message + "]");
        }

        failureCallback = (failureCallback == undefined) ? defaultErrorHandler : failureCallback;

        var callback = function (serverData) {
            console.error("Ajax request failed. Location = " + ((url != undefined && url != null) ? url : "Unknown"));

            failureCallback(serverData);
        }

        return callback;
    };





    var ajaxGet = function (url, successHandler, errorHandler) {
        var successCallback = generateSuccessHandler(successHandler, url);
        var errorCallback = generateErrorHandler(errorHandler, url);

        $.ajax(
            {
                type: "GET",
                url: url,
                success: successCallback,
                error: errorCallback,
                dataType: "json",
                contentType: "application/json' charset=utf-8"
            }
        );
    };

    var ajaxPost = function (data, url, successHandler, errorHandler) {
        var successCallback = generateSuccessHandler(successHandler, url);
        var errorCallback = generateErrorHandler(errorHandler, url);

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback,
            dataType: "json",
            contentType: "application/json' charset=utf-8"
        });
    };

    var ajaxPostHtml = function (data, url, successHandler, errorHandler) {
        var successCallback = generateSuccessHandler(successHandler, url);
        var errorCallback = generateErrorHandler(errorHandler, url);

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback,
            dataType: "text",
            contentType: "application/json' charset=utf-8"
        });
    };

    return {
        ajaxGet: ajaxGet,
        ajaxPost: ajaxPost,
        ajaxPostHtml: ajaxPostHtml
    }
}();