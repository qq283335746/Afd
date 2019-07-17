var exec = require('cordova/exec');

exports.GetHelloWord = function (arg0, success, error) {
    exec(success, error, "BaiduPlugin", "GetHelloWord", [arg0]);
};

exports.GetCurrentPosition = function(success, error) {
    exec(success, error, "BaiduPlugin", "GetCurrentPosition", []);
};

exports.Stop = function(success, error) {
    exec(success, error, "BaiduPlugin", "Stop", []);
};