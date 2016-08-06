
myApp.directive('popUpDialog', ['$http', function ($http) {
    return {
        restrict: 'E',
        scope: false,
        //replace: true,
        templateUrl: 'Home/PopUpDialog',
        controller: function ($scope) {
            var popImagePaths;
            $scope.showPopUpDialog = false;

            $scope.popUpDialogLoad = function (imagePath) {
                $scope.showPopUpDialog = true;

                $http.get('/Home/LoadImagePop/?id=' + imagePath.DeclarationId)
                    .success(function (data) {
                        popImagePaths = data;
                        $scope.generalImagePath = data[0].ImagePath;
                    });

                jQuery('.loupe_image img').css({
                    "opacity": "0.3",
                    'height': '20px',
                    'width': '20px'
                });
                jQuery('.loupe_image img').attr('src', '/Files/Images/loupe.png');
            }

            $scope.dropImgLoad = function () {
                $scope.popImagePaths = popImagePaths;
            }

            $scope.popUpDialogClose = function () {
                $scope.popImagePaths = '';
                $scope.showPopUpDialog = false;
                $('.general_image').attr('src', '');
            }
        }
    };
}]);

myApp.directive('popImgDropMouseenter', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.on('click', function () {
                $(".general_image").attr('src', element.attr('src'));
                element.parent().parent().find('img').attr('class', 'inactive_c');
                element.attr('class', 'pop_active');
            });
        }
    };
});

myApp.directive('loupeMouseevent', function () {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element, attrs) {
            var elem = element.children();
            new Image().src = '/Files/Images/loupe_green.png';

            element.on('mouseenter', function () {
                elem.css({
                    'height': '25px',
                    'width': '25px'
                })
                elem.attr('src', '/Files/Images/loupe_green.png');
            });

            element.on('mouseleave', function () {
                elem.css({
                    'height': '20px',
                    'width': '20px'
                })
                elem.attr('src', '/Files/Images/loupe.png');
            });
        }
    };
});

myApp.directive('imgDropMouseenter', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.on('mouseenter', function () {
                var id = scope.declaration.Id,
                    indexThis = scope.$index,
                    indexGeneral = $('#' + id + '').attr('data-index'),
                    direction = 'down';

                if (indexGeneral > indexThis)
                    direction = 'up';
                if (indexGeneral < indexThis)
                    direction = 'down';

                if (indexGeneral != indexThis) {
                    $("#" + id + "").replaceWith("<img data-index='" + indexThis + "' id='" + id + "' src='" + element.attr('src') + "'/>");
                    $('.photo #' + id + '').effect('slide', { direction: direction });

                    element.parent().parent().find('img').attr('class', 'inactive_c');
                    element.attr('class', 'active_c');
                }
            });
        }
    };
});