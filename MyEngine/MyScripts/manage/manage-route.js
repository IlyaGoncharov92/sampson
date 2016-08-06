myApp.controller('ManageUrlCtrl', function ($scope, $http, $routeParams, $location) {
    var manageSection = $routeParams.manageSection;

    switch (manageSection) {
        case 'general':
            $scope.manageContent = '/Manage/General';
            break;
        case 'config':
            $scope.manageContent = '/Manage/Config';
            break;
        case 'messages':
            $scope.manageContent = '/Manage/Messages';
            break;
        case 'liked':
            $scope.manageContent = '/Manage/Liked';
            break;
        default:
            break;
    }
})