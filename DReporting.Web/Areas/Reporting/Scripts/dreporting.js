
(function ($) {

    'use strict';

    // namespace
    var ctx = window.dreporting = { designer: {}, viewer: {} };

    // query kits
    $.extend(ctx, {
        queryAllowTypes: {
            'boolean': true,
            'number': true,
            'string': true
        },
        getQuery: function (query, name) {
            if (name === undefined) {
                name = query;
                query = location.search;
            }
            var result = query.match(new RegExp('[\?\&]' + name + '=([^\&]+)', 'i'));
            if (!result || result.length < 1) {
                return '';
            }
            return decodeURIComponent(result[1]);
        },
        getQueryHash: function (loc) {
            var href = ($.type(loc) === 'string') ? loc : (loc || location).href;
            var matches = href.match(/^[^#]*(#.+)$/);
            return matches ? matches[1] : '';
        },
        getQueryParams: function (url, type) {
            var params = {}, pairs = [], search = location.search, hash = ctx.getQueryHash(location);
            var splits = function (str) { return str.substr(1).replace(/\+/gi, ' ').split('&'); };
            if ($.type(url) === 'string') {
                search = ''; hash = '';
                var hashIndex = url.indexOf('#');
                if (hashIndex > -1) {
                    hash = url.substr(hashIndex);
                    url = url.substr(0, hashIndex);
                }
                var searchIndex = url.indexOf('?');
                if (searchIndex > -1) {
                    search = url.substr(searchIndex);
                }
            }
            if (type === 'hash') {
                if (hash) { pairs = splits(hash); }
            } else if (type === 'search') {
                if (search) { pairs = splits(search); }
            } else if (type === 'auto') {
                if (hash) {
                    pairs = splits(hash);
                } else if (search) {
                    pairs = splits(search);
                }
            } else {
                if (search) { pairs = splits(search); }
                if (hash) { pairs = pairs.concat(splits(hash)); }
            }
            for (var i = 0, len = pairs.length; i < len; i++) {
                var parts = (pairs[i] || '').split('=');
                if (parts.length === 2) {
                    params[parts[0]] = decodeURIComponent(parts[1]);
                }
            }
            return params;
        },
        appendQuery: function (query, name, value) {
            if (query === null || query === undefined || (!name && name !== 0)) { return query; }
            if ($.type(name) === 'object' || $.type(name) === 'array') {
                $.each(name, function (key, val) {
                    query = ctx.appendQuery(query, key, val);
                });
            } else if (ctx.queryAllowTypes[$.type(value)]) {
                query += ((query + '').indexOf('?') > -1) ? '' : '?';
                query += (/\?$/.test(query)) ? '' : '&';
                query += name + '=' + encodeURIComponent(String(value));
            }
            return query;
        },
        setQuery: function (query, name, value) {
            if (query === null || query === undefined || (!name && name !== 0)) { return query; }
            var queryParams = ctx.getQueryParams(query), lowerParams = {}, keyMap = {}, params = {}, lower;
            params = 'object,array'.indexOf($.type(name)) > -1 ? name : (params[name] = value, params);
            $.each(queryParams, function (key, val) {
                lowerParams[key.toLowerCase()] = val;
                keyMap[key] = key.toLowerCase();
            });
            $.each(params, function (key, val) {
                lower = (key + '').toLowerCase();
                if (lower in lowerParams) {
                    lowerParams[lower] = val;
                } else {
                    queryParams[key] = val;
                }
            });
            $.each(keyMap, function (f, t) {
                queryParams[f] = lowerParams[t];
            });
            // ret
            var sIndex = query.indexOf('?');
            sIndex = (sIndex === -1) ? query.length : sIndex;
            return ctx.appendQuery(query.substr(0, sIndex), queryParams);
        }
    });

    // designer extents
    $.extend(ctx.designer, {
        saveAndClose: null,
        SaveCommandExecute: function (designer, request) {
            //designer.callbackCustomArgs = {};
            var reportName = $('#txtReportName').val();
            if (!reportName) {
                reportName = prompt('Please specify the report name', 'Unnamed Report');
            }
            if (reportName) {
                designer.callbackUrl = ctx.setQuery(designer.callbackUrl, {
                    ReportName: reportName
                });
            } else {
                request.handled = true;
            }
        },
        SaveCommandExecuted: function (designer, response) {
            var returnUrl = ctx.getQuery('ReturnUrl');
            if (returnUrl && ctx.designer.saveAndClose) {
                location.href = returnUrl;
            } else {
                location.href = response.Result;
            }
        },
        CustomizeMenuActions: function (designer, context) {
            var actions = context.Actions;
            var returnUrl = ctx.getQuery('ReturnUrl');
            if (returnUrl) {
                actions.push({
                    text: 'Save & Close',
                    imageClassName: 'dxrd-image-save',
                    container: 'menu',
                    disabled: ko.observable(false),
                    visible: true,
                    clickAction: function (report, event) {
                        ctx.designer.saveAndClose = true;
                        report.save();
                    }
                });
            }
        }
    });

    // page actions
    $(function () {
        $('[data-action="deletereport"]').click(function () {
            var name = $(this).data('name');
            if (!confirm('Are you sure to delete report "' + name + '" ?')) {
                return false;
            }
        });
        $('[data-action="deletecategory"]').click(function () {
            var name = $(this).data('name');
            if (!confirm('Are you sure to delete category "' + name + '" ?')) {
                return false;
            }
        });
        $('[data-action="cancel"]').click(function () {
            var href = $(this).data('href');
            if (href) { location.href = href; }
        });
    });

})(jQuery);
