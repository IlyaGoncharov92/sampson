
myApp.controller('HeaderMenuCtrl', function ($scope, $http, $document) {
    $http.get('/Account/CheckAuthorize')
            .success(function (data) {
                $scope.loadLoginMenu = data;
            })

    $scope.clickLoadRegister = function () {
        $scope.loadLoginMenu = '/Account/Register';
    }

    $scope.popupVisible = false;

    $scope.clickLoginContent = function () {
        $scope.popupVisible = !$scope.popupVisible;
        if ($scope.loadLoginMenu != '/Account/LoginMenu') {
            $scope.loadLoginMenu = '/Account/Login';
        }
    }
    
    $(document).on('click', function (event) {
        var elem = $(event.target).closest('.popup_login_content').length,
            elem2 = $(event.target).closest('.header_login').length;

        if (elem2 || elem)
            return;
        
        $scope.popupVisible = false;
        $scope.$apply();
    })

    $(document).on("click", ".popup_office_nav a", function () {
        $scope.popupVisible = false;
        $scope.$apply();
    });

    //Если пользователь не аутентифицирован, то открыть панель входа
    if ($('#IsAuthenticated').attr('class') == 'false') {
        $(document).on("click", ".footer_content #image_liked", function () {
            window.scrollTo(0, 0);
            setTimeout(function () {
                $scope.popupVisible = true;
                $scope.$apply();
            }, 500)
        });
    }
})
