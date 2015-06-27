
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
        SaveCommandExecute: function (ev, request) {
            var defaultName = $('#defaultReportName').val();
            var displayName = prompt('Please specify the report name', defaultName);
            if (displayName) {
                ev.callbackUrl = ctx.setQuery(ev.callbackUrl, {
                    DisplayName: displayName
                });
            } else {
                request.handled = true;
            }
        },
        SaveCommandExecuted: function (ev, response) {
            if (response.Result) {
                location.href = response.Result;
            }
        }
    });

})(jQuery);