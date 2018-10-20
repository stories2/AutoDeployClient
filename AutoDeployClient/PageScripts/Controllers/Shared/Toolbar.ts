appModule.controller("ToolbarController", function ($scope, $http, $mdToast, $mdSidenav, AutoDeployClientService) {

    $scope.title = "개발을 멋지게 해보자"

    AutoDeployClientService.printLogMessage("ToolbarController", "ToolbarController", "init", LOG_LEVEL_INFO)
})