myApp.controller('DeclarationCtrl', function ($scope, $http, $routeParams, $location, $timeout) {
    var declarationId = $routeParams.declarationId,
        dataDec = [],
        dataImg = [],
        imgParentCount = 0;

    $scope.carouselDeclarationId = declarationId;

    $http.get('/Home/LoadDeclration/?id=' + declarationId)
        .success(function (data) {
            dataDec = data;

            $http.get('/Home/LoadDeclrationImages/?id=' + declarationId)
                .success(function (data) {
                    dataImg = data;
                    var imgFilter = [];

                    for (var i in data)
                        if (data[i].Type == "parent") {
                            imgFilter.push(data[i]);
                            imgParentCount++;
                        }

                    for (var i in data)
                        if (data[i].Type == "child")
                            imgFilter.push(data[i]);

                    $scope.imageChildPaths = imgFilter;
                    $scope.loadNewContent(declarationId);
                });
        });

    $scope.loadNewContent = function (id, indexThis) {
        $('.color_selection img').attr('class', 'inactive_d');
        $('.color_selection img').eq(indexThis).attr('class', 'active_d');

        for (var i in dataDec)
            if (dataDec[i].Id == id) {
                $scope.declaration = dataDec[i];
                break;
            }

        var dataImgGeneral = [];

        for (var j in dataImg)
            if (dataImg[j].DeclarationId == id)
                dataImgGeneral.push(dataImg[j]);

        $scope.imgGeneral = dataImgGeneral[0].ImagePath;
        $scope.imagePaths = dataImgGeneral;
    };

    //Кэширование фоток, после загрузки основных фото
    var imageCount = 0;
    $scope.imgLoad = function () {
        imageCount++;
        if (imageCount == imgParentCount)
            for (var i in dataImg)
                new Image().src = "/Files/" + dataImg[i].ImagePath;
    }

    $scope.loadPhoto = function (imagePath, indexThis) {
        $('.img_spis img').attr('class', 'inactive_d');
        $('.img_spis img').eq(indexThis).attr('class', 'active_d');

        var indexGeneral = jQuery('.content_photos img').attr('data-index');
        var direction;

        if (indexGeneral > indexThis)
            direction = 'up';
        if (indexGeneral < indexThis)
            direction = 'down';

        if (indexGeneral != indexThis) {
            $scope.imgGeneral = imagePath.ImagePath;
            $scope.index = indexThis;
            jQuery('.content_photos img').effect('slide', { direction: direction });
        }
    }
})


