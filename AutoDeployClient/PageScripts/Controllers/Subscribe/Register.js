appModule.controller("RegisterController", function ($scope, $http, $mdToast, $mdSidenav, $window, AutoDeployClientService) {
    $scope.isBrowserSupportPushService = false;
    $scope.isAlreadySubscribed = true;
    $scope.browserNavigator = $window.navigator;
    $scope.serviceWorkerRegistration = {};
    $scope.subscribeInfo = "";
    AutoDeployClientService.printLogMessage("RegisterController", "RegisterController", "init", LOG_LEVEL_INFO);
    $scope.onInit = function () {
        AutoDeployClientService.printLogMessage("RegisterController", "onInit", "init script", LOG_LEVEL_INFO);
        if ($scope.browserNavigator && $window) {
            $scope.isBrowserSupportPushService = true;
            AutoDeployClientService.printLogMessage("RegisterController", "onInit", "browser supported", LOG_LEVEL_INFO);
            getServiceWorkerRegisterationInfo($scope.browserNavigator);
        }
        else {
            AutoDeployClientService.printLogMessage("RegisterController", "onInit", "browser not supported", LOG_LEVEL_WARN);
        }
    };
    $scope.btnOnClickRegister = function () {
        subscribeThisBrowser();
    };
    function subscribeThisBrowser() {
        var applicationServerKey = urlB64ToUint8Array(APPLICATION_SERVER_PUBLIC_KEY);
        var payload = {
            userVisibleOnly: true,
            applicationServerKey: applicationServerKey
        };
        AutoDeployClientService.printLogMessage("RegisterController", "subscribeThisBrowser", "subscribe payload: " + JSON.stringify(payload), LOG_LEVEL_DEBUG);
        $scope.serviceWorkerRegistration.pushManager.subscribe(payload)
            .then(function (subscription) {
            AutoDeployClientService.printLogMessage("RegisterController", "subscribeThisBrowser", "subscribed: " + JSON.stringify(subscription), LOG_LEVEL_DEBUG);
            $scope.$apply(function () {
                $scope.isAlreadySubscribed = true;
                $scope.subscribeInfo = JSON.stringify(subscription);
                AutoDeployClientService.printLogMessage("RegisterController", "subscribeThisBrowser", "status: " + $scope.isAlreadySubscribed, LOG_LEVEL_DEBUG);
            });
        })
            .catch(function (err) {
            AutoDeployClientService.printLogMessage("RegisterController", "subscribeThisBrowser", "cannot subscribe: " + JSON.stringify(err), LOG_LEVEL_ERROR);
            $scope.$apply(function () {
                $scope.isAlreadySubscribed = false;
                AutoDeployClientService.printLogMessage("RegisterController", "subscribeThisBrowser", "status: " + $scope.isAlreadySubscribed, LOG_LEVEL_DEBUG);
            });
        });
    }
    function urlB64ToUint8Array(base64String) {
        var padding = repeat('=', (4 - base64String.length % 4) % 4);
        console.log("padding", padding);
        var base64 = (base64String + padding)
            .replace(/\-/g, '+')
            .replace(/_/g, '/');
        var rawData = window.atob(base64);
        var outputArray = new Uint8Array(rawData.length);
        for (var i = 0; i < rawData.length; ++i) {
            outputArray[i] = rawData.charCodeAt(i);
        }
        return outputArray;
    }
    function repeat(character, repeatRange) {
        var result = "";
        for (var i = 0; i < repeatRange; i += 1) {
            result = result + character;
        }
        return result;
    }
    function getServiceWorkerRegisterationInfo(navigator) {
        navigator.serviceWorker.register('/PageScripts/AppServiceWorker.js')
            .then(function (serviceWorkerRegisteration) {
            AutoDeployClientService.printLogMessage("RegisterController", "getServiceWorkerRegisterationInfo", "service worker registered info: " + JSON.stringify(serviceWorkerRegisteration), LOG_LEVEL_DEBUG);
            $scope.serviceWorkerRegistration = serviceWorkerRegisteration;
            checkIsAlreadySubscribed($scope.serviceWorkerRegistration);
        })
            .catch(function (exception) {
            AutoDeployClientService.printLogMessage("RegisterController", "getServiceWorkerRegisterationInfo", "cannot register service worker: " + JSON.stringify(exception), LOG_LEVEL_ERROR);
        });
    }
    function updateSubscriptionInfo(subscription) {
        $scope.$apply(function () {
            $scope.subscribeInfo = JSON.stringify(subscription.pushManager.getSubscription());
        });
    }
    function checkIsAlreadySubscribed(serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.getSubscription()
            .then(function (subscription) {
            var isSubscribed = !(subscription === null);
            $scope.$apply(function () {
                $scope.isAlreadySubscribed = isSubscribed;
                AutoDeployClientService.printLogMessage("RegisterController", "checkIsAlreadySubscribed", "status: " + $scope.isAlreadySubscribed, LOG_LEVEL_DEBUG);
                if ($scope.isAlreadySubscribed) {
                    $scope.subscribeInfo = JSON.stringify(subscription);
                }
            });
        });
    }
});
//# sourceMappingURL=Register.js.map