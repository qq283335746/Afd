angular.module('ngTygaSoft.services.Camera', [])

.factory('$tygasoftCamera', function ($timeout,$cordovaCamera, $cordovaFileTransfer, $cordovaToast, $ionicModal, $ionicActionSheet, $ionicLoading, $tygasoftMC, $tygasoftCommon) {

    var ts = {};

    ts.Init = function ($scope) {
        $scope.CameraModel = {};
        $ionicModal.fromTemplateUrl('templates/ShowCameraPicture.html', {
            scope: $scope
        }).then(function (modal) {
            $scope.CameraModal = modal;
        });
    };

    ts.OnToggleCamera = function ($scope) {

        var btnMenus = [
            { text: '<i class="icon ion-ios-camera-outline positive"></i> 拍照' },
            { text: '<i class="icon ion-image positive"></i> 相册' },
            { text: '<i class="icon ion-ios-minus-outline assertive"></i> 取消' }
        ];
        $ionicActionSheet.show({
            buttons: btnMenus,
            cancelText: '取消',
            buttonClicked: function (index) {
                ts.OnTakePicture($scope, index);
                return true;
            }
        });
    };

    ts.OnTakePicture = function ($scope, pictureSourceType) {

        var options = {
            quality: 50,
            destinationType: Camera.DestinationType.FILE_URI
        };
        if (pictureSourceType == 1) {
            options.sourceType = Camera.PictureSourceType.PHOTOLIBRARY;
        }
        else {
            options.sourceType = Camera.PictureSourceType.CAMERA;
        }
        $cordovaCamera.getPicture(options).then(function (imageURI) {
            //$scope.CameraImage = "data:image/jpeg;base64," + imageData;
            $scope.CameraModel.PictureUrl = imageURI;
            ts.OnShowCameraPicture($scope);
        });
    };

    ts.OnShowCameraPicture = function ($scope) {
        if ($scope.CameraModel.PictureUrl && !$scope.CameraModel.PictureUrl.IsNullOrEmpty()) {
            $scope.CameraModal.show();
        }
        else {
            $cordovaToast.showShortCenter($tygasoftMC.MC.M_Camera_Error);
        }
    };

    ts.OnFinishPicture = function ($scope, parms, callback) {
        $ionicLoading.show();
        $timeout(function () {
            var options = new FileUploadOptions();
            options.params = parms;
            var serverUri = $tygasoftCommon.UploadUrl();
            var filePath = $scope.CameraModel.PictureUrl;
            $cordovaFileTransfer.upload(serverUri, filePath, options).then(function (result) {
                $ionicLoading.hide();
                callback(JSON.parse(result.response));
            }, function (err) {
                $ionicLoading.hide();
            }, function (progress) {
                //$ionicLoading.show();
            });
        }, 1000);
    };

    ts.CloseCameraModal = function ($scope) {
        $scope.CameraModal.hide();
    };

    return ts;
});