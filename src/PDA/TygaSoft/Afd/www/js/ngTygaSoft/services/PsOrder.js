angular.module('ngTygaSoft.services.PsOrder', [])

.factory('$tygasoftPsOrder', function ($ionicPopup, $tygasoftLocalStorage, $tygasoftMC, $tygasoftCommon, $tygasoftLogin, $tygasoftCamera) {

    var ts = {};

    ts.OnUploading = function ($scope) {
        if ($scope.TotalPicture >= 5) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_MaxUploadPictures, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        $tygasoftCamera.OnToggleCamera($scope);
    };

    ts.GetPictures = function ($scope) {
        AfdPlugin.GetOrderProcessInfo($scope.OrderInfo.OrderProcessId, function (result) {
            var jResult = JSON.parse(result);
            if (jResult.ResCode != 1000) {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: jResult.Msg, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }
            ts.SetPictures($scope, JSON.parse(jResult.Data).Pictures);
        })
    };

    ts.SetPictures = function ($scope, sPictures) {
        if (!$tygasoftCommon.String.IsNullOrWhiteSpace(sPictures)) {
            $scope.Pictures = [];
            var picItems = sPictures.split('|');
            $scope.TotalPicture = picItems.length;
            for (var i = 0; i < $scope.TotalPicture; i++) {
                if (i % 3 == 0) {
                    var item = {};
                    item.Src = picItems[i] == undefined ? undefined : $tygasoftCommon.ServerUrl() + picItems[i];
                    item.Src1 = picItems[i + 1] == undefined ? undefined : $tygasoftCommon.ServerUrl() + picItems[i + 1];
                    item.Src2 = picItems[i + 2] == undefined ? undefined : $tygasoftCommon.ServerUrl() + picItems[i + 2];
                    $scope.Pictures.push(item);
                }
            }
        }
    };

    ts.OnFinishPicture = function ($scope) {
        $tygasoftLogin.GetLoginInfo().then(function (res) {
            var parms = {};
            parms['ReqName'] = "UploadPdaOrderProcess";
            parms['Id'] = $scope.OrderInfo.OrderProcessId;
            parms['OrderStep'] = $scope.OrderInfo.OrderStep;
            parms['LoginId'] = res.LoginId;
            parms['DeviceId'] = res.DeviceId;
            parms['Latlng'] = res.Latlng;

            $tygasoftCamera.OnFinishPicture($scope, parms, function (result) {
                if (result.ResCode != 1000) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: result.Msg, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                ts.SetPictures($scope, result.Data);
                var dlgShow = $ionicPopup.show({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok });
                setTimeout(function () {
                    dlgShow.close();
                }, 1000);
                $tygasoftCamera.CloseCameraModal($scope);
            });
        })
    };

    return ts;
});