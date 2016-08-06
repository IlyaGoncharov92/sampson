
myApp.controller('CatalogCtrl', function ($scope, $http, $routeParams) {
    var sectionId = $routeParams.sectionId,
        categoryId = $routeParams.categoryId;

    $http.get('/Home/LoadCatalogImages/?sectionId=' + sectionId + "&categoryId=" + categoryId)
        .success(function (data) {
            $scope.imagePaths = data;

            $http.get('/Home/LoadCatalog/?sectionId=' + sectionId + "&categoryId=" + categoryId)
                .success(function (data) {
                    $scope.declarations = data;
                });
        });

    var likedKey = true;
    $scope.likedClick = function (declaration, indexThis) {
        if (likedKey == true) {
            likedKey = false;

            var liked;
            var thisClass = $('.content #image_liked').eq(indexThis).attr('class');
            if (thisClass == 'image_liked_true')
                liked = true;
            if (thisClass == 'image_liked_false')
                liked = false;

            $http.get('/Home/LickedClick/?idDeclaration=' + declaration.Id + '&declarationLiked=' + liked)
                .success(function (data) {
                    if (thisClass == 'image_liked_true') {
                        $('.content #image_liked').eq(indexThis).attr('class', 'image_liked_false');
                    }

                    if (thisClass == 'image_liked_false') {
                        $('.content #image_liked').eq(indexThis).attr('class', 'image_liked_true');
                    }
                    likedKey = true;
                })
        }
    }
})