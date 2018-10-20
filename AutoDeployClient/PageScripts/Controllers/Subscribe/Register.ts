appModule.controller("RegisterController", function ($scope, $http, $mdToast, $mdSidenav, $window, AutoDeployClientService) {

    $scope.isBrowserSupportPushService = false
    $scope.browserNavigator = $window.navigator

    AutoDeployClientService.printLogMessage("RegisterController", "RegisterController", "init", LOG_LEVEL_INFO)

    $scope.onInit = function () {
        AutoDeployClientService.printLogMessage("RegisterController", "onInit", "init script", LOG_LEVEL_INFO) 

        if ($scope.browserNavigator && $window) {
            $scope.isBrowserSupportPushService = true
            AutoDeployClientService.printLogMessage("RegisterController", "onInit", "browser supported", LOG_LEVEL_INFO)
        }
        else {
            AutoDeployClientService.printLogMessage("RegisterController", "onInit", "browser not supported", LOG_LEVEL_WARN)
        }
    }

    $scope.btnOnClickRegister = function () {
        $scope.browserNavigator.serviceWorker.register('/PageScripts/AppServiceWorker.js')
            .then(function (serviceWorkerRegisteration) {
                AutoDeployClientService.printLogMessage("RegisterController", "onInit", "service worker registered: " + JSON.stringify(serviceWorkerRegisteration), LOG_LEVEL_DEBUG)
            })
            .catch(function (exception) {
                AutoDeployClientService.printLogMessage("RegisterController", "onInit", "cannot register service worker: " + JSON.stringify(exception), LOG_LEVEL_ERROR)
            })
    }
})