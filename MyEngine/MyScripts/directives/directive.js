
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

myApp.directive('carousel', ['$http', function ($http) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: 'Home/LoadCarousel',
        link: function ($scope, element, attrs) {
            $scope.title = attrs.carouselTitle;

            var url = attrs.carouselDataUrl,
                countElement = 0,
                count = 0;

            $http.get(url).success(function (data) {
                $scope.slideDeclarations = data;
                for (var d in data)
                    countElement++;
                countElement = countElement - 6;
            })

            $scope.slideMove = function (move) {
                if (countElement > 0) {
                    if (move == 'back') {
                        if (count > 0)
                            count--;
                    }
                    if (move == 'next') {
                        if (count < countElement)
                            count++;
                    }
                }

                $('.carousel_content').animate({
                    right: count * 194,
                    transition: ".5s linear"
                });
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

myApp.directive('ngLoad', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var fn = $parse(attrs.ngLoad);
            element.on('load', function (event) {
                scope.$apply(function () {
                    fn(scope, { $event: event });
                });
            });
        }
    };
}]);