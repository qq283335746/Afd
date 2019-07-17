angular.module('starter', ['ionic', 'ionic-datepicker', 'ngCordova', 'ngTygaSoft', 'starter.controllers'])
.run(function ($ionicPlatform, $ionicHistory, $rootScope, $state, $cordovaToast, $cordovaDevice, $tygasoftLocalStorage, $tygasoftMC, $tygasoftLogin) {
    $ionicPlatform.ready(function () {
        if (cordova.platformId === 'ios' && window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);

        }
        if (window.StatusBar) {
            StatusBar.styleDefault();
        }

        $ionicPlatform.registerBackButtonAction(function (e) {
            if ($ionicHistory.backView()) {
                $ionicHistory.goBack();
            }
            else {
                if ($rootScope.backButtonPressedOnceToExit) {
                    ionic.Platform.exitApp();
                } else {
                    $rootScope.backButtonPressedOnceToExit = true;
                    $cordovaToast.showShortCenter($tygasoftMC.MC.M_ExitApp);
                    setTimeout(function () {
                        $rootScope.backButtonPressedOnceToExit = false;
                    }, 2000);
                }
            }
            e.preventDefault();
            return false;
        }, 101);

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams, options) {
            if (!$tygasoftLogin.IsLogin()) {
                if (toState.name == 'app.Login') {
                    $ionicHistory.nextViewOptions({ disableAnimate: true, disableBack: true, historyRoot:false });
                }
                else{
                    $state.go('app.Login');
                    event.preventDefault();
                }
            }
        });

        $tygasoftLocalStorage.Set("ServiceUrl", "http://www.tygaweb.com/afd");
    });
})

.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, ionicDatePickerProvider) {
    $ionicConfigProvider.navBar.alignTitle('center');
    $ionicConfigProvider.scrolling.jsScrolling(true);
    ionicDatePickerProvider.configDatePicker({
        inputDate: new Date(),
        setLabel: '确定',
        todayLabel: '今天',
        closeLabel: '关闭',
        mondayFirst: false,
        weeksList: ["日", "一", "二", "三", "四", "五", "六"],
        monthsList: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        templateType: 'popup',
        showTodayButton: true,
        dateFormat: 'yyyy年MM月dd日',
        closeOnSelect: false,
        disableWeekdays: [6],
    });
    $stateProvider

      .state('app', {
          url: '/app',
          abstract: true,
          templateUrl: 'templates/Menu.html',
          controller: 'AppCtrl'
      })
    .state('app.Login', {
        url: '/Login',
        views: {
            'menuContent': {
                templateUrl: 'templates/Login.html',
                controller: 'LoginCtrl'
            }
        }
    })
    .state('app.Home', {
        url: '/Home',
        views: {
            'menuContent': {
                templateUrl: 'templates/Home.html',
                controller: 'AppCtrl'
            }
        }
    })
    .state('app.OrderTake', {
        url: '/OrderTake',
        views: {
            'menuContent': {
                templateUrl: 'templates/OrderTake.html',
                controller: 'OrderTakeCtrl'
            }
        }
    })
    .state('app.OrderSend', {
        url: '/OrderSend',
        views: {
            'menuContent': {
                templateUrl: 'templates/OrderSend.html',
                controller: 'OrderSendCtrl'
            }
        }
    })
    .state('app.PsOrder', {
        url: '/PsOrder/:item',
        views: {
            'menuContent': {
                templateUrl: 'templates/PsOrder.html',
                controller: 'PsOrderCtrl'
            }
        }
    })
    .state('app.SysSet', {
        url: '/SysSet',
        views: {
            'menuContent': {
                templateUrl: 'templates/SysSet.html',
                controller: 'SysCtrl'
            }
        }
    })

    $urlRouterProvider.otherwise('/app/Home');
});
