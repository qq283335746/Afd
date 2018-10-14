angular.module('ngTygaSoft.services.Menu', [])
.factory('$tygasoftMenu', function ($ionicHistory, $ionicSideMenuDelegate, $ionicPopup, $tygasoftLocalStorage, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Bind = function ($scope) {
        $scope.onTo = function (index) {
            ts.OnTo($scope, index);
        };
        ts.GetMenus($scope);
        ts.GetHomeMenus($scope);

        $scope.$on('$ionicView.beforeEnter', function (e) {
            $scope.LoginData.IsLogin = $tygasoftLogin.IsLogin();
            $ionicSideMenuDelegate.canDragContent($scope.LoginData.IsLogin);
            if (!$scope.LoginData.IsLogin) {
                $ionicHistory.nextViewOptions({ disableAnimate: true, disableBack: true, historyRoot: false });
                window.location = '#/app/Login';
            }
        });
    };

    ts.GetMenus = function ($scope) {
        $scope.MenuItems = [{ "Name": "设置", "icon": "ion-ios-gear-outline", "Url": "#/app/SysSet" }, { "Name": "切换账号", "icon": "ion-ios-loop", "Url": "#/app/Login" }, {"Name": "退出", "icon": "ion-power" }];
    };

    ts.GetHomeMenus = function ($scope) {
        $scope.HomeMenuItems = [{"Name": "取件", "Src": "img/icons/home-sj.png", "Url": "#/app/OrderTake" }, {"Name": "签收", "Src": "img/icons/home-jh.png", "Url": "#/app/OrderSend" }];
    };

    ts.OnTo = function ($scope,index) {
        var item = $scope.MenuItems[index];
        if (!item.Url || item.Url == '') {
            switch (item.Name) {
                case "退出":
                    ts.ExitApp();
                    break;
                default:
                    break;
            }
        }
        else {
            window.location = item.Url;
        }
        $ionicSideMenuDelegate.toggleLeft();
    };

    ts.CheckVersion = function () {
        var timespan = (new Date("2017-5-1")) - (new Date());
        var totalDays = Math.floor(timespan / (24 * 3600 * 1000));
        //if (totalDays < 1) {
        //    setInterval(function () {
        //        alert('使用期限已过期！');
        //    }, 1000);
        //}
    };

    ts.ExitApp = function () {
        $ionicPopup.confirm({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_ExitApp_Content, cancelText: $tygasoftMC.MC.Btn_CancelText, okText: $tygasoftMC.MC.Btn_OkText }).then(function (res) {
            if (res) {
                $tygasoftLogin.SignOut();
                ionic.Platform.exitApp();
            }
        })
    };

    return ts;
});