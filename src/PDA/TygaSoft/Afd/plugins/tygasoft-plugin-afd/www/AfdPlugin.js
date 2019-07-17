var exec = require('cordova/exec');

exports.GetHelloWord = function (arg0, success, error) {
    exec(success, error, "AfdPlugin", "GetHelloWord", [arg0]);
};

exports.ValidateUser = function (arg0, arg1, success, error) {
    exec(success, error, "AfdPlugin", "ValidateUser", [arg0, arg1]);
};

exports.GetOrderProcessInfo = function (arg0, success, error) {
    exec(success, error, "AfdPlugin", "GetOrderProcessInfo", [arg0]);
};

exports.SaveOrderScan = function (arg0, success, error) {
    exec(success, error, "AfdPlugin", "SaveOrderScan", [arg0]);
};