angular.module('starter.controllers', [])

.controller('AppCtrl', function ($scope, $tygasoftMenu) {
    $scope.LoginData = {};
    $tygasoftMenu.Bind($scope);

    $scope.$on('$ionicView.enter', function (e) {
        $tygasoftMenu.CheckVersion();
    });
})
.controller('DefaultCtrl', function ($scope) {
    $scope.$on('$ionicView.enter', function (e) {
    });
})
.controller('LoginCtrl', function ($scope, $tygasoftLogin) {
    $scope.LoginData = { UserName: '', Password:'' };
    
    $tygasoftLogin.Bind($scope);
})
.controller('OrderTakeCtrl', function ($scope, $interval, $tygasoftOrders) {
    $scope.ModelData = { "Barcode": "", "CustomerCode": "", "OrderStep": "已取件" };
    $scope.ScanData = [];
    $scope.ItvCommit = null;
    $scope.CanCommit = true;

    $scope.$on('$ionicView.enter', function (e) {
        $scope.ItvCommit = $interval(function () {
            if (!$scope.CanCommit) return;
            $scope.CanCommit = false;
            if ($scope.ScanData.length > 0) {
                $tygasoftOrders.SaveOrderScan($scope);
            }
            else $scope.CanCommit = true;
        }, 1000);
    });
    $scope.$on('$ionicView.leave', function (e) {
        $interval.cancel($scope.ItvCommit);
    });

    $scope.onSelectPictures = function (item) {
        $tygasoftOrders.OnSelectPictures(item);
    };
    $scope.onSure = function () {
        $tygasoftOrders.DoScan($scope, false);
    };
    $scope.onCustomerCodeChanged = function () {
        if ($scope.btnTabIndex == 1) return false;
        $tygasoftOrders.DoScan($scope, true);
    };
    $scope.onBarcodeChanged = function () {
        if ($scope.btnTabIndex == 1) return false;
        $tygasoftOrders.DoScan($scope, true);
    };
    $scope.btnTabIndex = 0;
    $scope.onTabSelected = function (index) {
        $scope.btnTabIndex = index;
    };
    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})
.controller('OrderSendCtrl', function ($scope, $interval, $tygasoftOrders) {
    $scope.ModelData = { "Barcode": "", "CustomerCode": "", "OrderStep": "已送达" };
    $scope.ScanData = [];
    $scope.ItvCommit = null;
    $scope.CanCommit = true;

    $scope.$on('$ionicView.enter', function (e) {
        $scope.ItvCommit = $interval(function () {
            if (!$scope.CanCommit) return;
            $scope.CanCommit = false;
            if ($scope.ScanData.length > 0) {
                $tygasoftOrders.SaveOrderScan($scope);
            }
            $scope.CanCommit = true;
        }, 3000);
    });
    $scope.$on('$ionicView.leave', function (e) {
        $interval.cancel($scope.ItvCommit);
    });

    $scope.onSelectPictures = function (item) {
        $tygasoftOrders.OnSelectPictures(item);
    };
    $scope.onSure = function () {
        $tygasoftOrders.DoScan($scope, false);
    };
    $scope.onCustomerCodeChanged = function () {
        if ($scope.btnTabIndex == 1) return false;
        $tygasoftOrders.DoScan($scope, true);
    };
    $scope.onBarcodeChanged = function () {
        if ($scope.btnTabIndex == 1) return false;
        $tygasoftOrders.DoScan($scope, true);
    };
    $scope.btnTabIndex = 0;
    $scope.onTabSelected = function (index) {
        $scope.btnTabIndex = index;
    };
    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})
.controller('PsOrderCtrl', function ($scope, $stateParams, $tygasoftCamera, $tygasoftPsOrder) {
    $scope.ModelData = { "BlankPic": "img/BlankPic.png" };
    $scope.OrderInfo = JSON.parse($stateParams.item);
    $scope.TotalPicture = 0;

    $tygasoftCamera.Init($scope);
    $tygasoftPsOrder.GetPictures($scope);

    $scope.OnUploading = function () {
        $tygasoftPsOrder.OnUploading($scope);
    }
    $scope.onShowCameraPicture = function () {
        $tygasoftCamera.OnShowCameraPicture($scope);
    }
    $scope.onTakePicture = function (type) {
        $tygasoftCamera.OnTakePicture($scope, type);
    }
    $scope.onFinishPicture = function () {
        $tygasoftPsOrder.OnFinishPicture($scope);
    }
})
.controller('SysCtrl', function ($scope, $tygasoftLocalStorage, $tygasoftSys, $tygasoftLogin) {
    $scope.ModelData = { "ServiceUrl": "" + $tygasoftLocalStorage.Get("ServiceUrl", "") + "", "UhfOnOff": "checked" };
    $tygasoftSys.Bind($scope);
});
