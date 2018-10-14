angular.module('ngTygaSoft.services.Orders', [])

.factory('$tygasoftOrders', function ($ionicPopup,$tygasoftLocalStorage, $tygasoftMC, $tygasoftCommon, $tygasoftLogin) {

    var ts = {};

    ts.OnSelectPictures = function (item) {
        if (!item.OrderProcessId || item.OrderProcessId == '') {
            var dlgShow = $ionicPopup.show({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Waiting });
            setTimeout(function () {
                dlgShow.close();
            }, 1000);
            return false;
        }
        window.location = '#/app/PsOrder/' + JSON.stringify(item);
    };

    ts.DoScan = function ($scope, isAuto) {
        var sBarcode = $scope.ModelData.Barcode;
        var sCustomerCode = $scope.ModelData.CustomerCode;
        if (!sBarcode || sBarcode == '') return false;
        if ($scope.ModelData.OrderStep == '已取件') {
            if (!sCustomerCode || sCustomerCode == "") return false;
        }

        try {
            var item = { Barcode: sBarcode, CustomerCode: sCustomerCode, Status: '待处理', OrderStep: $scope.ModelData.OrderStep };
            if ($scope.ScanData.length == 0) $scope.ScanData.push(item);
            else {
                var isExist = false;
                for (var i = 0; i < $scope.ScanData.length; i++) {
                    var oldItem = $scope.ScanData[i];
                    if (oldItem.Barcode == sBarcode && oldItem.CustomerCode == sCustomerCode) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    return false;
                }
                $scope.ScanData.push(item);
            }
        }
        finally {
            $scope.ModelData.Barcode = "";
            $scope.ModelData.CustomerCode = "";
        }
    };

    ts.SaveOrderScan = function ($scope) {
        var len = $scope.ScanData.length;
        if (len > 0) {
            for (var i = 0; i < len; i++) {
                var item = $scope.ScanData[i];
                if (item.Status == '待处理') {
                    $tygasoftLogin.GetLoginInfo().then(function (res) {
                        var postData = {};
                        postData.OrderCode = item.Barcode;
                        postData.CustomerCode = item.CustomerCode;
                        postData.OrderStep = item.OrderStep;
                        postData.LoginId = res.LoginId;
                        postData.DeviceId = res.DeviceId;
                        postData.Latlng = res.Latlng;
                        AfdPlugin.SaveOrderScan(JSON.stringify(postData), function (result) {
                            var jResult = JSON.parse(result);
                            if (jResult.ResCode == 1000) {
                                item.OrderProcessId = jResult.Data;
                                item.Status = '已处理';
                            }
                            else {
                                item.Status = '异常';
                            }
                            $scope.CanCommit = true;
                        })
                    }, function (err) {
                        $scope.CanCommit = true;
                    })
                }
                else if (i == (len - 1)) $scope.CanCommit = true;
            }
        }
        else $scope.CanCommit = true;
    };

    return ts;
});