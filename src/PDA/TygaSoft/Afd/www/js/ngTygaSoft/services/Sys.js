angular.module('ngTygaSoft.services.Sys', [])

.factory('$tygasoftSys', function ($http, $ionicLoading, $ionicPopup, $ionicModal, ionicDatePicker, $tygasoftLocalStorage, $tygasoftMC, $tygasoftCommon) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.onSave = function () {
            if (!$scope.ModelData.ServiceUrl || $scope.ModelData.ServiceUrl == '') {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_EmptyError, okText: $tygasoftMC.MC.Btn_OkText });
                return false;
            }
            $ionicPopup.confirm({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_SaveConfirm, cancelText: $tygasoftMC.MC.Btn_CancelText, okText: $tygasoftMC.MC.Btn_OkText }).then(function (r) {
                if (r) {
                    $tygasoftLocalStorage.Set("ServiceUrl", $scope.ModelData.ServiceUrl);
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok, okText: $tygasoftMC.MC.Btn_OkText });
                }
            })
        }
    };

    return ts;
});