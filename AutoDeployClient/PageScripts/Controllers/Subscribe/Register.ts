appModule.controller("RegisterController", function ($scope, $http, $mdToast, $mdSidenav, $window, AutoDeployClientService) {

    $scope.isBrowserSupportPushService = false
    $scope.isAlreadySubscribed = true
    $scope.browserNavigator = $window.navigator
    $scope.serviceWorkerRegistration = {}

    AutoDeployClientService.printLogMessage("RegisterController", "RegisterController", "init", LOG_LEVEL_INFO)

    $scope.onInit = function () {
        AutoDeployClientService.printLogMessage("RegisterController", "onInit", "init script", LOG_LEVEL_INFO) 

        if ($scope.browserNavigator && $window) {
            $scope.isBrowserSupportPushService = true
            AutoDeployClientService.printLogMessage("RegisterController", "onInit", "browser supported", LOG_LEVEL_INFO)

            getServiceWorkerRegisterationInfo($scope.browserNavigator)
        }
        else {
            AutoDeployClientService.printLogMessage("RegisterController", "onInit", "browser not supported", LOG_LEVEL_WARN)
        }
    }

    function getServiceWorkerRegisterationInfo(navigator) {
        navigator.serviceWorker.register('/PageScripts/AppServiceWorker.js')
            .then(function (serviceWorkerRegisteration) {
                AutoDeployClientService.printLogMessage("RegisterController", "getServiceWorkerRegisterationInfo", "service worker registered info: " + JSON.stringify(serviceWorkerRegisteration), LOG_LEVEL_DEBUG)
                $scope.serviceWorkerRegistration = serviceWorkerRegisteration

                checkIsAlreadySubscribed($scope.serviceWorkerRegistration)
            })
            .catch(function (exception) {
                AutoDeployClientService.printLogMessage("RegisterController", "getServiceWorkerRegisterationInfo", "cannot register service worker: " + JSON.stringify(exception), LOG_LEVEL_ERROR)
            })
    }

    function checkIsAlreadySubscribed(serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.getSubscription()
            .then(function (subscription) {
                var isSubscribed = !(subscription === null)
                AutoDeployClientService.printLogMessage("RegisterController", "checkIsAlreadySubscribed", "status: " + isSubscribed, LOG_LEVEL_DEBUG)
                if (isSubscribed) {
                    $scope.isAlreadySubscribed = true
                }
                else {
                    $scope.isAlreadySubscribed = false
                }
            })
    }

    $scope.btnOnClickRegister = function () {
        
    }
})