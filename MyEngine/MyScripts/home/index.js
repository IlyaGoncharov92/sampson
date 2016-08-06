myApp.controller('IndexCtrl', function ($scope, $http) {
    var countElement = 0,
        count = 0;

    $http.get('/Home/LoadCarouselIndex').success(function (data) {
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
})