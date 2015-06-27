
(function ($) {

    ''

    // namespace
    var dreporting = window.dreporting = { designer: {}, viewer: {} };

    dreporting.appendQuery = function (url, obj) {
        
    };

    $.extend(dreporting.designer, {
        SaveCommandExecute: function (ev, request) {
            ev.callbackUrl = dreporting.appendQuery(ev.callbackUrl, {
                ReportName: $('#txtReportName').val()
            });
        },
        SaveCommandExecuted: function (ev, response) {
            if (response.Result) {
                location.replace(response.Result);
            }
        }
    });

})(jQuery);