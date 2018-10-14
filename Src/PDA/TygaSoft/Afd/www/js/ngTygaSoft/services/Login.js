angular.module('ngTygaSoft.services.Login', [])

.factory('$tygasoftLogin', function ($q, $http, $cordovaGeolocation, $cordovaDevice, $ionicModal, $ionicHistory, $ionicLoading, $ionicPopup, $tygasoftMC, $tygasoftCommon, $tygasoftLocalStorage) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.doLogin = function () {
            ts.DoLogin($scope);
        };
    };

    ts.IsLogin = function () {
        return parseInt($tygasoftLocalStorage.Get('IsLogin', 0)) == 1;
    };

    ts.GetLoginInfo = function () {
        var q = $q.defer();

        var jDeviceInfo = $tygasoftLocalStorage.GetObject('DeviceInfo');
        var loginInfo = {};
        loginInfo.LoginId = jDeviceInfo.LoginId;
        loginInfo.DeviceId = jDeviceInfo.UUID;
        loginInfo.Latlng = "";

        BaiduPlugin.GetCurrentPosition(function (result) {
            //alert('GetCurrentPosition--result--' + JSON.stringify(result));
            var lat = result.Latitude;
            var long = result.Longitude;
            loginInfo.Latlng = lat + "," + long;
            return q.resolve(loginInfo);
        }, function (err) {
            //alert('GetCurrentPosition--err--' + err);
            return q.resolve(loginInfo);
        })
        //baidu_location.getCurrentPosition(function (result) {
        //    var lat = result.latitude;
        //    var long = result.lontitude;
        //    loginInfo.Latlng = lat + "," + long;
        //    return q.resolve(loginInfo);
        //}, function (err) {
        //    //alert('baidu_location--err--' + loginInfo.Latlng);
        //    return q.resolve(loginInfo);
        //})

        //var posOptions = { timeout: 10000, enableHighAccuracy: false };
        //$cordovaGeolocation.getCurrentPosition(posOptions).then(function (position) {
        //    var lat = position.coords.latitude;
        //    var long = position.coords.longitude;
        //    loginInfo.Latlng = lat + "," + long;
        //    alert(loginInfo.Latlng);
        //    return q.resolve(loginInfo);
        //}, function (err) {
        //    alert('err--' + loginInfo.Latlng);
        //    return q.resolve(loginInfo);
        //});

        return q.promise;
    };

    ts.DoLogin = function ($scope) {
        var username = $scope.LoginData.UserName;
        var password = $scope.LoginData.Password;
        if ((!username || username == '') || (!password || password == '')) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Login_EmptyError, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        AfdPlugin.ValidateUser(username, password, function (result) {
            var jResult = JSON.parse(result);
            if (jResult.ResCode == 1000) {
                var jDeviceInfo = { "Platform": "" + $cordovaDevice.getPlatform() + "", "UUID": "" + $cordovaDevice.getUUID() + "", "Version": "" + $cordovaDevice.getVersion() + "", "Latlng": "" };
                jDeviceInfo.LoginId = username;
                $tygasoftLocalStorage.SetObject('DeviceInfo', jDeviceInfo);
                $tygasoftLocalStorage.Set('IsLogin', 1);
                ts.ToHome();
            }
            else {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Login_Error, okText: $tygasoftMC.MC.Btn_OkText });
            }
        }, function (err) {
            alert('err--' + err);
        });
    };

    ts.SignOut = function () {
        $tygasoftLocalStorage.Set('IsLogin', 0);
    };

    ts.ToHome = function () {
        window.location = '#/app/Home';
    };

    ts.SetRootView = function () {
        $ionicHistory.nextViewOptions({
            disableAnimate: true,
            disableBack: true,
            historyRoot: true
        });
        window.location = '#/app/Home';
    };

    return ts;
});